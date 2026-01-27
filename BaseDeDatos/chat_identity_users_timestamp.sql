/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `chat_identity_users_timestamp`;
CREATE TABLE IF NOT EXISTS `chat_identity_users_timestamp` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfIdentityUser` bigint(10) NOT NULL,
  `ClientRoomVersion` bigint(10) NOT NULL DEFAULT 0,
  `ClientParticipantsVersion` bigint(10) NOT NULL DEFAULT 0,
  `ClientMessagesVersion` bigint(10) NOT NULL DEFAULT 0,
  `ServerRoomVersion` bigint(10) NOT NULL DEFAULT 0,
  `ServerParticipantsVersion` bigint(10) NOT NULL DEFAULT 0,
  `ServerMessagesVersion` bigint(10) NOT NULL DEFAULT 0,
  `DatePushSent` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `fk_chatIdentuserstime_Identity_Users_idx` (`IdfIdentityUser`),
  CONSTRAINT `fk_chatIdentuserstime_Identity_Users` FOREIGN KEY (`IdfIdentityUser`) REFERENCES `identity_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
