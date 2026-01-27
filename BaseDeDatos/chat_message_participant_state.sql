/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `chat_message_participant_state`;
CREATE TABLE IF NOT EXISTS `chat_message_participant_state` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfMessage` bigint(10) NOT NULL,
  `IdfParticipant` bigint(10) NOT NULL,
  `Delivered` varchar(2) NOT NULL DEFAULT '0',
  `Read` varchar(2) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `fk_chatmsgpartstate_part_idx` (`IdfParticipant`),
  KEY `fk_chatmsgpartstate_chatmessages_idx` (`IdfMessage`),
  CONSTRAINT `fk_chatmsgpartstate_chatmessages` FOREIGN KEY (`IdfMessage`) REFERENCES `chat_messages` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_chatmsgpartstate_part` FOREIGN KEY (`IdfParticipant`) REFERENCES `chat_room_participants` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
