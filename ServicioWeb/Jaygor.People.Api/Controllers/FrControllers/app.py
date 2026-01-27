import json
import os
import requests
import uuid
from scipy.spatial import distance as dist
from flask import Flask, request, jsonify
from werkzeug.utils import secure_filename
import face_recognition
import base64
import cv2
import numpy as np
import shutil
from imutils import paths
import imutils
import ffmpeg
import uuid
from PIL import Image, ExifTags
import dlib
from imutils import face_utils
from imutils.video import FileVideoStream
from classes import EnrollResult, IdentifyResult, LocationResult
from jwt import JWT
from shutil import copyfile

application = Flask(__name__)

ALLOWED_EXTENSIONS = set(['mp4', 'jpg'])

UPLOAD_FOLDER = './uploads'

application.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER
application.config['MAX_CONTENT_LENGTH'] = 10 * 1024 * 1024


def allowed_file(file_name):
    """
        Check if file name is allowed
        Response object for enroll result
        Parameters:
        file_name (string): User id
        Returns:
        bool: Indicates if the extesion file is allowed
    """
    return '.' in file_name and file_name.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS


@application.route('/face-reco/')
def index():
    """
    Rest test
    """
    return 'Face detection works!!!'



@application.route('/face-reco/identificationfromphoto', methods=['POST'])
def identify_from_photo():
    # first, call Login API to Get the userid
    user = request.form['userid']
    ruta = application.root_path
    datasetPath = os.path.join(ruta, "dataset-" + user, user)
    # check if dataset folder exists
    if not os.path.exists(datasetPath):
        resp = jsonify({'message': 'No dataset folder exists'})
        resp.status_code = 400
        return resp

    # check if the post request has the file part
    if 'file' not in request.files:
        resp = jsonify({'message': 'No file part in the request'})
        resp.status_code = 400
        return resp
    file = request.files['file']
    if file.filename == '':
        resp = jsonify({'message': 'No file selected for uploading'})
        resp.status_code = 400
        return resp
    if file and allowed_file(file.filename):
        filename = secure_filename(file.filename)
        file.save(os.path.join(application.config['UPLOAD_FOLDER'], filename))

        result = IdentifyResult()
        result.userid = user
        result.photo = os.path.join(application.config['UPLOAD_FOLDER'], filename)
        result = recognize(result, datasetPath, user)

        response = json.loads(result.toJSON())
        resp = jsonify({'success': result.success, 'IdentifyResult': response})
        resp.status_code = 200
        return resp
    else:
        resp = jsonify({'message': 'Allowed file types are: jpg, jpeg, mp4'})
        resp.status_code = 400
        return resp




@application.route('/face-reco/identification', methods=['POST'])
def identify_from_video():
    # first, call Login API to Get the userid
    user = request.form['userid']
    ruta = application.root_path
    datasetPath = os.path.join(ruta, "dataset-" + user, user)
    # check if dataset folder exists
    if not os.path.exists(datasetPath):
        resp = jsonify({'message': 'No dataset folder exists'})
        resp.status_code = 400
        return resp

    # check if the post request has the file part
    if 'file' not in request.files:
        resp = jsonify({'message': 'No file part in the request'})
        resp.status_code = 400
        return resp
    file = request.files['file']
    if file.filename == '':
        resp = jsonify({'message': 'No file selected for uploading'})
        resp.status_code = 400
        return resp
    if file and allowed_file(file.filename):
        filename = secure_filename(file.filename)
        file.save(os.path.join(application.config['UPLOAD_FOLDER'], filename))

        result = IdentifyResult()
        result.userid = user
        result = extract_blink_from_video(os.path.join(application.config['UPLOAD_FOLDER'], filename), result, user)

        if result.blinks <= 0:
            result.success = False
            result.message = "Error. No blink detected: #" + str(result.blinks)
        else:
            result = recognize(result, datasetPath, user)

        response = json.loads(result.toJSON())
        resp = jsonify({'success': result.success, 'identify_result': response})
        resp.status_code = 200
        return resp
    else:
        resp = jsonify({'message': 'Allowed file types are: jpg, jpeg, mp4'})
        resp.status_code = 400
        return resp


@application.route('/face-reco/location', methods=['POST'])
def location():
    content = request.json
    latitude = content['latitude']
    longitude = content['longitude']
    one_signal_user_id = content['onesignaluserid']
    one_signal_push_token = content['onesignalpushtoken']
    result = LocationResult()
    result.latitude = content['latitude']
    result.longitude = content['longitude']
    result.onesignaluserid = content['onesignaluserid']
    result.onesignalpushtoken = content['onesignalpushtoken']
    result.date = content['date']
    result.success = True

    response = json.loads(result.toJSON())

    print("[INFO] {} ".format(response))
    resp = jsonify({'success': result.success, 'location_result': response})
    resp.status_code = 200
    return resp


@application.route('/face-reco/enroll', methods=['POST'])
def enroll_from_video():
    """
    Enroll the face for user id from video
    Response object for enroll result
    Parameters:
    user_id (string): User id
    file (file): Video file
    Returns:
    EnrollResult: Enroll result
    """
    # first, call Login API to Get the userid
    url = "http://52.60.139.58:8010/qa/jaygorapi/api/"

    if request.headers['Authorization'] is None:
        resp = jsonify({'message': 'No Authorization token found'})
        resp.status_code = 400
        return resp
    email = request.form['email']
    userid = request.form['userid']
    auth_token = request.headers['Authorization'].split(" ")[1]
    jwt = JWT()
    decode_token = jwt.decode(auth_token, do_verify=False)
    # check if idx is equal to id
    if decode_token['roles'] is None:
        resp = jsonify({'message': 'No roles found in Token'})
        resp.status_code = 400
        return resp

    if 'admin' not in decode_token['roles']:
        resp = jsonify({'message': 'Admin role not in Token'})
        resp.status_code = 400
        return resp

    # check if the post request has the file part
    if 'file' not in request.files:
        resp = jsonify({'message': 'No file part in the request'})
        resp.status_code = 400
        return resp
    file = request.files['file']
    if file.filename == '':
        resp = jsonify({'message': 'No file selected for uploading'})
        resp.status_code = 400
        return resp
    if file and allowed_file(file.filename):
        filename = secure_filename(str(uuid.uuid4()) + "_" + file.filename)
        file.save(os.path.join(application.config['UPLOAD_FOLDER'], filename))
        user_id = userid
        result = enroll_face_from_video(os.path.join(application.config['UPLOAD_FOLDER'], filename), user_id)
        result.success = True

        if result.faces <= 0:
            result.success = False
            result.message = "Error. No faces detected: " + str(result.faces)

        response = json.loads(result.toJSON())
        resp = jsonify({'success': result.success, 'enroll_result': response})
        resp.status_code = 201
        return resp
    else:
        resp = jsonify({'message': 'Allowed file types are: mp4'})
        resp.status_code = 400
        return resp


@application.route('/face-reco/enrollfromphoto', methods=['POST'])
def enroll_from_photo():
    """
    Enroll the face for user id from photo
    Response object for enroll result
    Parameters:
    user_id (string): User id
    file (file): Photo file
    Returns:
    EnrollResult: Enroll result
    """
    # first, call Login API to Get the userid
    #url = "http://52.60.139.58:8010/qa/jaygorapi/api/"

    if request.headers['Authorization'] is None:
        resp = jsonify({'message': 'No Authorization token found'})
        resp.status_code = 400
        return resp
    email = request.form['email']
    userid = request.form['userid']
    auth_token = request.headers['Authorization'].split(" ")[1]
    jwt = JWT()
    decode_token = jwt.decode(auth_token, do_verify=False)
    # check if idx is equal to id
    if decode_token['roles'] is None:
        resp = jsonify({'message': 'No roles found in Token'})
        resp.status_code = 400
        return resp

    if 'admin' not in decode_token['roles']:
        resp = jsonify({'message': 'Admin role not in Token'})
        resp.status_code = 400
        return resp

    # check if the post request has the file part
    if 'file' not in request.files:
        resp = jsonify({'message': 'No file part in the request'})
        resp.status_code = 400
        return resp
    file = request.files['file']
    if file.filename == '':
        resp = jsonify({'message': 'No file selected for uploading'})
        resp.status_code = 400
        return resp
    if file and allowed_file(file.filename):
        filename = secure_filename(str(uuid.uuid4()) + "_" + file.filename)
        file.save(os.path.join(application.config['UPLOAD_FOLDER'], filename))
        user_id = userid
        result = EnrollResult()
        result = enroll_face_from_photo(os.path.join(application.config['UPLOAD_FOLDER'], filename), user_id)
        if result.faces <= 0:
            result.success = False
            result.message = "Error. No faces detected: " + str(result.faces)

        response = json.loads(result.toJSON())
        resp = jsonify({'Success': result.success, 'EnrollResult': response})
        resp.status_code = 201
        return resp
    else:
        resp = jsonify({'message': 'Allowed file types are: mp4'})
        resp.status_code = 400
        return resp


def eye_aspect_ratio(eye):
    """
    compute the euclidean distances between the two sets of vertical eye landmarks.
    Parameters:
    eye (?): eye
    Returns:
    bool: Indicates if the extesion file is allowed
    """
    # compute the euclidean distances between the two sets of
    # vertical eye landmarks (x, y)-coordinates
    A = dist.euclidean(eye[1], eye[5])
    B = dist.euclidean(eye[2], eye[4])

    # compute the euclidean distance between the horizontal
    # eye landmark (x, y)-coordinates
    C = dist.euclidean(eye[0], eye[3])

    # compute the eye aspect ratio
    ear = (A + B) / (2.0 * C)

    # return the eye aspect ratio
    return ear


def recognize(result, datase_path, user_id):
    """
    Do the recognition of user id from video
    Parameters:
    result (IdentifyResult): Identify result
    dataset_path (string): Dataset path
    user_id (string): User id.
    Returns:
    IdentifyResult: Indicates if the extesion file is allowed
    """
    ruta = application.root_path
    carpetaOutput = os.path.join(ruta, "images")
    img = Image.open(result.photo)
    getexif = img._getexif()
    if getexif is not None:
        exif = {ExifTags.TAGS[k]: v for k, v in getexif.items() if k in ExifTags.TAGS}
        if 'Orientation' in exif:
            result.rotate = exif['Orientation']

    if result.rotate == 6:
        rotedImage = cv2.imread(result.photo)
        p = result.photo
        cv2.imwrite(p, rotedImage)

    # Identificamos si hay caras primeramente
    face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')
    gray = cv2.imread(result.photo)
    facesDetected = face_cascade.detectMultiScale(
        gray,
        scaleFactor=1.3,
        minNeighbors=3,
        minSize=(100, 100)
    )

    print("[INFO] Found {0} Faces!".format(len(facesDetected)))

    if len(facesDetected) == 0:
        result.success = False
        result.message = "No faces detected"
        return result

    if len(facesDetected) > 1:
        result.success = False
        result.message = "More than one face detected"
        return result

    for (x, y, w, h) in facesDetected:
        # cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
        p = os.path.sep.join([carpetaOutput, "{}.png".format(user_id)])
        roi_color = gray[y:y + h, x:x + w]
        print("[INFO] Object found. Saving locally.")
        cv2.imwrite(p, roi_color)

    unknown_image = face_recognition.load_image_file(p)

    # Get the face encodings for each face in each image file Since there could be more than one face in each image,
    # it returns a list of encodings. But since I know each image only has one face, I only care about the first
    # encoding in each image, so I grab index 0.

    # Bucle para cada foto del dataset

    known_faces = []
    imagePaths = list(paths.list_images(datase_path))
    LIMIT = 5

    for (i, imagePath) in enumerate(imagePaths):
        # Load the jpg files into numpy arrays
        known_image = face_recognition.load_image_file(imagePath)
        # A list of 128-dimensional face encodings (one for each face in the image)
        list_known_face_encoding = face_recognition.face_encodings(known_image)
        # if len(list_known_face_encoding) == 0:
        #    result.success = False
        #    result.message = "No faces detected in know folder: " + datase_path
        #    return result

        if len(list_known_face_encoding) > 1:
            result.success = False
            result.message = "More than one face detected in know folder: " + datase_path
            return result

        if len(list_known_face_encoding) == 1:
            known_faces.append(list_known_face_encoding[0])

        if i >= LIMIT:
            break

    list_unknown_face_encoding = face_recognition.face_encodings(unknown_image)

    if len(list_unknown_face_encoding) == 0:
        result.success = False
        result.message = "No faces detected in unknow"
        return result

    if len(list_unknown_face_encoding) > 1:
        result.success = False
        result.message = "More than one face detected in unknow"
        return result

    if len(list_unknown_face_encoding) == 1:
        unknown_face_encoding = list_unknown_face_encoding[0]

    # results is an array of True/False telling if the unknown face matched anyone in the known_faces array
    results = face_recognition.compare_faces(known_faces, unknown_face_encoding)
    countTrue = sum(1 for f in results if bool(f) is True)
    result.probability = countTrue / len(results)

    if result.probability > 0.8:
        result.success = True
    else:
        result.success = False

    return result


def extract_blink_from_video(videoPath, result, userid):
    # al verificar el video de validacion hay que ver si esta vivo
    # define two constants, one for the eye aspect ratio to indicate
    # blink and then a second constant for the number of consecutive
    # frames the eye must be below the threshold
    EYE_AR_THRESH = 0.3
    EYE_AR_CONSEC_FRAMES = 3

    # initialize the frame counters and the total number of blinks
    COUNTER = 0
    TOTAL = 0
    LIMIT = 2

    ruta = application.root_path
    pathDetector = os.path.join(ruta, "shape_predictor_68_face_landmarks.dat")
    photoPath = os.path.join(ruta, "images/" + userid + ".png")

    # initialize dlib's face detector (HOG-based) and then create
    # the facial landmark predictor
    print("[INFO] loading facial landmark predictor...")
    detector = dlib.get_frontal_face_detector()
    predictor = dlib.shape_predictor(pathDetector)

    # grab the indexes of the facial landmarks for the left and
    # right eye, respectively
    (lStart, lEnd) = face_utils.FACIAL_LANDMARKS_IDXS["left_eye"]
    (rStart, rEnd) = face_utils.FACIAL_LANDMARKS_IDXS["right_eye"]

    # obtenemos la orientacion del video
    try:
        probe = ffmpeg.probe(videoPath)
    except ffmpeg.Error as e:
        print(e.stderr)

    video_stream = next((stream for stream in probe['streams'] if stream['codec_type'] == 'video'), None)
    if video_stream is None:
        print('No video stream found')
        return -1

    frames = int(video_stream['nb_frames'])
    tags = dict(video_stream['tags'])
    rotate = 0
    if 'rotate' in tags:
        rotate = int(str(tags['rotate']))

    # start the video stream thread
    print("[INFO] starting video stream thread...")
    vs = FileVideoStream(videoPath).start()
    fileStream = True
    # loop over frames from the video stream
    counter = 0
    while True:
        # if this is a file video stream, then we need to check if
        # there any more frames left in the buffer to process
        if fileStream and not vs.more():
            break

        counter += 1
        # grab the frame from the threaded video file stream, resize
        # it, and convert it to grayscale
        # channels)
        frame = vs.read()
        if frame is None:
            continue
        frame = imutils.resize(frame, width=450)

        if rotate == 90:
            frame = rot90(frame, 1)
        if rotate == 270:
            frame = rot90(frame, 2)

        gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

        # detect faces in the grayscale frame
        rects = detector(gray, 0)

        # loop over the face detections
        for rect in rects:
            # determine the facial landmarks for the face region, then
            # convert the facial landmark (x, y)-coordinates to a NumPy
            # array
            shape = predictor(gray, rect)
            shape = face_utils.shape_to_np(shape)

            # extract the left and right eye coordinates, then use the
            # coordinates to compute the eye aspect ratio for both eyes
            leftEye = shape[lStart:lEnd]
            rightEye = shape[rStart:rEnd]
            leftEAR = eye_aspect_ratio(leftEye)
            rightEAR = eye_aspect_ratio(rightEye)

            # average the eye aspect ratio together for both eyes
            ear = (leftEAR + rightEAR) / 2.0

            # compute the convex hull for the left and right eye, then
            # visualize each of the eyes
            ##leftEyeHull = cv2.convexHull(leftEye)
            ##rightEyeHull = cv2.convexHull(rightEye)
            ##cv2.drawContours(frame, [leftEyeHull], -1, (0, 255, 0), 1)
            ##cv2.drawContours(frame, [rightEyeHull], -1, (0, 255, 0), 1)

            # check to see if the eye aspect ratio is below the blink
            # threshold, and if so, increment the blink frame counter
            if ear < EYE_AR_THRESH:
                COUNTER += 1

            # otherwise, the eye aspect ratio is not below the blink
            # threshold
            else:
                # if the eyes were closed for a sufficient number of
                # then increment the total number of blinks
                if COUNTER >= EYE_AR_CONSEC_FRAMES:
                    TOTAL += 1
                    # grabamos la photo
                    cv2.imwrite(photoPath, frame)
                    know_image = face_recognition.load_image_file(photoPath)
                    list_known_face_encoding = face_recognition.face_encodings(know_image)
                    # if len(list_known_face_encoding) == 0:
                    if result.photo is None and len(list_known_face_encoding) == 1:
                        result.photo = photoPath

                # reset the eye frame counter
                COUNTER = 0

        if TOTAL >= LIMIT:
            break
    # do a bit of cleanup
    cv2.destroyAllWindows()
    vs.stop()
    result.blinks = TOTAL
    return result


def rot90(img, rotflag):
    """ rotFlag 1=CW, 2=CCW, 3=180"""
    if rotflag == 1:
        img = cv2.transpose(img)
        img = cv2.flip(img, 1)  # transpose+flip(1)=CW
    elif rotflag == 2:
        img = cv2.transpose(img)
        img = cv2.flip(img, 0)  # transpose+flip(0)=CCW
    elif rotflag == 3:
        img = cv2.flip(img, -1)  # transpose+flip(-1)=180
    elif rotflag != 0:  # if not 0,1,2,3
        raise Exception("Unknown rotation flag({})".format(rotflag))
    return img

def enroll_face_from_photo(photo_path, user_id):
    """
    Enroll the face for user id from photo
    Parameters:
    photo_path (string): path of the photo
    user_id (string): user id
    Returns:
    EnrollResult: Enroll result
    """
    result = EnrollResult()
    result.userid = user_id
    # hay que verificar que no haya mas de 2 caras en el upload
    # Load the cascade
    face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')

    app_path = application.root_path
    detector_path = os.path.join(app_path, "face_detection_model")
    dataset_path = os.path.join(app_path, "dataset-" + user_id)
    # delete folders
    if os.path.exists(dataset_path):
        shutil.rmtree(dataset_path)
    # create folder
    if not os.path.exists(dataset_path):
        os.mkdir(dataset_path)

    # create sub folder
    output_path = os.path.join(dataset_path, user_id)

    if os.path.exists(output_path):
        shutil.rmtree(output_path)
    if not os.path.exists(output_path):
        os.mkdir(output_path)

    faces = 0
    confidence1 = 0.9

    # get photo orientation
    img = Image.open(photo_path)
    getexif = img._getexif()
    if getexif is not None:
        exif = {ExifTags.TAGS[k]: v for k, v in getexif.items() if k in ExifTags.TAGS}
        if 'Orientation' in exif:
            result.rotate = exif['Orientation']
        if 'ImageWidth' in exif:
            result.width = exif['ImageWidth']
        if 'ImageLength' in exif:
            result.height = exif['ImageLength']

    if result.rotate == 6:
        rotedImage = cv2.imread(photo_path)
        p = photo_path
        cv2.imwrite(p, rotedImage)

    if result.rotate == 8:
        rotedImage = cv2.imread(photo_path)
        rotedImage = rot90(rotedImage, 3)
        rotedImage = rot90(rotedImage, 3)
        gray = cv2.cvtColor(rotedImage, cv2.COLOR_BGR2GRAY)
        p = photo_path
        cv2.imwrite(p, gray)

    know_image = face_recognition.load_image_file(photo_path)
    list_known_face_encoding = face_recognition.face_encodings(know_image)
    if len(list_known_face_encoding) == 0:
        result.faces = 0
        result.success = False
        result.message = "No face detected in know folder: " + photo_path

    if len(list_known_face_encoding) == 1:
        p = os.path.sep.join([output_path, "{}.png".format(user_id + "_gray")])
        copyfile(photo_path, p)
        result.faces = 1
        result.success = True
        result.message = "Face enroll correctly for userId: " + user_id

    if len(list_known_face_encoding) > 1:
        result.faces = len(list_known_face_encoding)
        result.success = False
        result.message = "More than one face detected in know folder: " + photo_path
        # delete folder
        if os.path.exists(dataset_path):
            shutil.rmtree(dataset_path)
        return result

    if result.faces == 0:
        # delete folder
        if os.path.exists(dataset_path):
            shutil.rmtree(dataset_path)
    return result


def enroll_face_from_video(video_path, user_id):
    """
    Enroll the face for user id from video
    Parameters:
    video_path (string): path of the video
    user_id (string): user id
    Returns:
    EnrollResult: Enroll result
    """
    result = EnrollResult()
    result.user_id = user_id
    # hay que verificar que no haya mas de 2 caras en el upload
    # Load the cascade
    face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')

    app_path = application.root_path
    detector_path = os.path.join(app_path, "face_detection_model")
    dataset_path = os.path.join(app_path, "dataset-" + user_id)
    # delete folders
    if os.path.exists(dataset_path):
        shutil.rmtree(dataset_path)
    # create folder
    if not os.path.exists(dataset_path):
        os.mkdir(dataset_path)

    # create sub folder
    output_path = os.path.join(dataset_path, user_id)

    if os.path.exists(output_path):
        shutil.rmtree(output_path)
    if not os.path.exists(output_path):
        os.mkdir(output_path)

    faces = 0
    confidence1 = 0.5

    # get video orientation
    try:
        probe = ffmpeg.probe(video_path)
    except ffmpeg.Error as e:
        result.error = -4
        result.message = e.stderr
        result.success = False
        print(e.stderr)
        return result

    video_stream = next((stream for stream in probe['streams'] if stream['codec_type'] == 'video'), None)
    if video_stream is None:
        result.error = -5
        result.message = 'No video stream found'
        result.success = False
        print('No video stream found')
        return result

    result.width = int(video_stream['width'])
    result.height = int(video_stream['height'])
    result.frames = int(video_stream['nb_frames'])
    tags = dict(video_stream['tags'])
    if 'rotate' in tags:
        result.rotate = int(str(tags['rotate']))

    # hay que calcular el skip en base al numero de frames
    skip = int(result.frames / 10)  # para tener 10 fotos

    # load our serialized face detector from disk
    print("[INFO] loading face detector...")
    protoPath = os.path.sep.join([detector_path, "deploy.prototxt"])
    modelPath = os.path.sep.join([detector_path, "res10_300x300_ssd_iter_140000.caffemodel"])
    net = cv2.dnn.readNetFromCaffe(protoPath, modelPath)

    # open a pointer to the video file stream and initialize the total
    # number of frames read and saved thus far
    vs = cv2.VideoCapture(video_path)
    read = 0
    saved = 0

    # loop over frames from the video file stream
    while True:
        # grab the frame from the file
        (grabbed, frame) = vs.read()

        # if the frame was not grabbed, then we have reached the end
        # of the stream
        if not grabbed:
            break

        # increment the total number of frames read thus far
        read += 1

        # check to see if we should process this frame
        if read % skip != 0:
            continue

        # grab the frame dimensions and construct a blob from the frame
        (h, w) = frame.shape[:2]
        blob = cv2.dnn.blobFromImage(cv2.resize(frame, (300, 300)), 1.0,
                                     (300, 300), (104.0, 177.0, 123.0))

        # pass the blob through the network and obtain the detections and
        # predictions
        net.setInput(blob)
        detections = net.forward()

        # ensure at least one face was found
        if len(detections) > 0:
            if result.rotate == 90:
                # p = os.path.sep.join([output_path, "{}.png".format(user_id + "_before")])
                # cv2.imwrite(p, frame)
                frame = rot90(frame, 1)
                # p = os.path.sep.join([output_path, "{}.png".format(user_id + "_after")])
                # cv2.imwrite(p, frame)
            elif result.rotate == 270:
                # p = os.path.sep.join([output_path, "{}.png".format(user_id + "_before")])
                # cv2.imwrite(p, frame)
                frame = rot90(frame, 2)
                # p = os.path.sep.join([output_path, "{}.png".format(user_id + "_after")])
                # cv2.imwrite(p, frame)

            # Convert into grayscale
            gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
            # p = os.path.sep.join([output_path, "{}.png".format(user_id + "_gray")])
            # cv2.imwrite(p, gray)
            # Detect faces

            # facesCount = face_cascade.detectMultiScale(gray, 1.1, 4)
            # detect faces in the grayscale frame
            # rects = face_cascade.detectMultiScale(
            #    cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY), scaleFactor=1.1,
            #    minNeighbors=5, minSize=(30, 30))
            facesCount = len(detections)

            if facesCount > 1:
                p = os.path.sep.join([output_path, "{}.png".format(user_id)])
                cv2.imwrite(p, frame)
                result.error = -2
                result.message = "More than one face detected"
                result.success = False
                return result

            # we're making the assumption that each image has only ONE
            # face, so find the bounding box with the largest probability
            i = np.argmax(detections[0, 0, :, 2])
            confidence = detections[0, 0, i, 2]

            # ensure that the detection with the largest probability also
            # means our minimum probability test (thus helping filter out
            # weak detections)
            if confidence > confidence1:
                # compute the (x, y)-coordinates of the bounding box for
                # the face and extract the face ROI
                # box = detections[0, 0, i, 3:7] * np.array([w, h, w, h])
                # (startX, startY, endX, endY) = box.astype("int")
                # face = frame[startY:endY, startX:endX]

                # write the face to disk
                # p = os.path.sep.join([output_path, "{}.png".format(user_id)])
                # cv2.imwrite(p, face)
                faces += 1

                facesDetected = face_cascade.detectMultiScale(
                    gray,
                    scaleFactor=1.3,
                    minNeighbors=3,
                    minSize=(100, 100)
                )

                print("[INFO] Found {0} Faces!".format(len(facesDetected)))

                for (x, y, w, h) in facesDetected:
                    # cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
                    p = os.path.sep.join([output_path, "{}-{}.png".format(user_id, faces)])
                    roi_color = gray[y:y + h, x:x + w]
                    print("[INFO] Object found. Saving locally.")
                    cv2.imwrite(p, roi_color)
    # do a bit of cleanup
    vs.release()
    cv2.destroyAllWindows()
    result.faces = faces

    if faces == 0:
        result.success = False
        # delete folder
        if os.path.exists(dataset_path):
            shutil.rmtree(dataset_path)
    return result


if __name__ == '__main__':
    application.run(host='0.0.0.0', port=80)
