-- Bizzi dummy users seed (for local dev)
USE `bizzi`;

-- Roles (create/update)
INSERT INTO identity_roles (Id, Rol, State, DisplayShortName, RolDescription)
VALUES
  (1, 'user',  'A', 'user',  'Standard user'),
  (2, 'admin', 'A', 'admin', 'Administrator'),
  (3, 'faceRecorder', 'A', 'faceRecorder', 'Face recorder')
ON DUPLICATE KEY UPDATE
  Rol=VALUES(Rol),
  State=VALUES(State),
  DisplayShortName=VALUES(DisplayShortName),
  RolDescription=VALUES(RolDescription);

-- ===== Admin user =====
INSERT INTO identity_users
  (Email, Password, State, LastName, FirstName, IdfImg, GeoTrackingEvery, FaceStamp, TFASecret)
VALUES
  ('admin@local', 'admin123', 'A', 'Local', 'Admin', 0, 0, '-', '-');

SET @admin_id := LAST_INSERT_ID();

INSERT INTO identity_users_rol (IdfUser, IdfRol, State)
VALUES
  (@admin_id, 1, 'A'),
  (@admin_id, 2, 'A');

INSERT INTO identity_images (Name, IdfIdentity_user)
VALUES ('-', @admin_id);

SET @admin_img_id := LAST_INSERT_ID();
UPDATE identity_users SET IdfImg = @admin_img_id WHERE Id = @admin_id;

-- ===== Normal user =====
INSERT INTO identity_users
  (Email, Password, State, LastName, FirstName, IdfImg, GeoTrackingEvery, FaceStamp, TFASecret)
VALUES
  ('user@local', 'user123', 'A', 'Local', 'User', 0, 0, '-', '-');

SET @user_id := LAST_INSERT_ID();

INSERT INTO identity_users_rol (IdfUser, IdfRol, State)
VALUES
  (@user_id, 1, 'A');

INSERT INTO identity_images (Name, IdfIdentity_user)
VALUES ('-', @user_id);

SET @user_img_id := LAST_INSERT_ID();
UPDATE identity_users SET IdfImg = @user_img_id WHERE Id = @user_id;

-- If login fails because Password is hashed, run ONE of these after inserts:
-- UPDATE identity_users SET Password = MD5('admin123') WHERE Email='admin@local';
-- UPDATE identity_users SET Password = MD5('user123')  WHERE Email='user@local';
-- or:
-- UPDATE identity_users SET Password = SHA1('admin123') WHERE Email='admin@local';
-- UPDATE identity_users SET Password = SHA1('user123')  WHERE Email='user@local';
