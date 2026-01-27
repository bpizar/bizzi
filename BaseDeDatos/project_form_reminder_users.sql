/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_form_reminder_users`;
CREATE TABLE IF NOT EXISTS `project_form_reminder_users` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfProjectFormReminder` bigint(20) DEFAULT NULL,
  `IdfUser` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_project_form_reminder_users_project_form_reminders` (`IdfProjectFormReminder`),
  KEY `FK_project_form_reminder_users_identity_users` (`IdfUser`),
  CONSTRAINT `FK_project_form_reminder_users_identity_users` FOREIGN KEY (`IdfUser`) REFERENCES `identity_users` (`Id`),
  CONSTRAINT `FK_project_form_reminder_users_project_form_reminders` FOREIGN KEY (`IdfProjectFormReminder`) REFERENCES `project_form_reminders` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
