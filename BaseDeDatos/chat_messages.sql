/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `chat_messages`;
CREATE TABLE IF NOT EXISTS `chat_messages` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Messages` varchar(256) NOT NULL,
  `Date` datetime NOT NULL,
  `IdfIdentityUserSender` bigint(10) NOT NULL,
  `IdfChatRoom` bigint(10) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_chatmessages_chatroom_idx` (`IdfChatRoom`),
  KEY `fk_chat_messages_identityusers_idx` (`IdfIdentityUserSender`),
  CONSTRAINT `fk_chat_messages_identityusers` FOREIGN KEY (`IdfIdentityUserSender`) REFERENCES `identity_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_chatmessages_chatroom` FOREIGN KEY (`IdfChatRoom`) REFERENCES `chat_rooms` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
