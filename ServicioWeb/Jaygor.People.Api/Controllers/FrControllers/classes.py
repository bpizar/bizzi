import json


class EnrollResult:
    """
        Enroll result class
        Response object for enroll result
        Parameters:
        user_id (string): User id
        faces (int): number of faces detected
        rotate (int): degrees rotate of the video
        error (string): error text
        message (string): message
        width (int): width of the video
        height (int): height of the video
        frames (int): number of frames of the video
    """
    def __init__(self, userid=None, faces=0, rotate=0, error=None, message=None, width=0, height=0, frames=0, success=False):
        self.userid = userid
        self.faces = faces
        self.rotate = rotate
        self.error = error
        self.message = message
        self.width = width
        self.height = height
        self.frames = frames
        self.success = success

    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__,
                          sort_keys=True, indent=4)


class IdentifyResult:
    """
        Enroll result class
        Response object for enroll result
        Parameters:
        user_id (string): User id
        error (string): error text
        message (string): message
        success (bool): identify if the identification is succes or not
        rotate (int): degrees rotate of the video
        blinks (int): number of blinks detected
        photo (string): photo path for identification
        probability (float): probability of recognization
    """
    def __init__(self, user_id=None, error=None, message=None, success=False, rotate=0, blinks=0, photo=None,
                 probability=0):
        self.userid = user_id
        self.error = error
        self.message = message
        self.success = success
        self.rotate = rotate
        self.blinks = blinks
        self.photo = photo
        self.probability = 0

    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__,
                          sort_keys=True, indent=4)

class LocationResult:
    """
        Location result class
        Response object for enroll result
        Parameters:
        user_id (string): User id
        faces (int): number of faces detected
        rotate (int): degrees rotate of the video
        error (string): error text
        message (string): message
        width (int): width of the video
        height (int): height of the video
        frames (int): number of frames of the video
    """
    def __init__(self, user_id=None, onesignaluserid=None, onesignalpushtoken=None, latitude=0, longitude=0, date=None, success=False):
        self.userid = user_id
        self.onesignaluserid = onesignaluserid
        self.onesignalpushtoken = onesignalpushtoken
        self.latitude = latitude
        self.longitude = longitude
        self.date = date
        self.success = success

    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__,
                          sort_keys=True, indent=4)
