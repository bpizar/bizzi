/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `client_form_reminder_users`;
CREATE TABLE IF NOT EXISTS `client_form_reminder_users` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfClientFormReminder` bigint(20) DEFAULT NULL,
  `IdfUser` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_client_form_reminder_users_client_form_reminders` (`IdfClientFormReminder`),
  KEY `FK_client_form_reminder_users_identity_users` (`IdfUser`),
  CONSTRAINT `FK_client_form_reminder_users_client_form_reminders` FOREIGN KEY (`IdfClientFormReminder`) REFERENCES `client_form_reminders` (`Id`),
  CONSTRAINT `FK_client_form_reminder_users_identity_users` FOREIGN KEY (`IdfUser`) REFERENCES `identity_users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
