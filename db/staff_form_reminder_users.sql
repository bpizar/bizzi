/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_form_reminder_users`;
CREATE TABLE IF NOT EXISTS `staff_form_reminder_users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdfStaffFormReminder` bigint(20) DEFAULT NULL,
  `IdfUser` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_staff_form_reminder_users_identity_users` (`IdfUser`),
  KEY `FK_staff_form_reminder_users_staff_form_reminders` (`IdfStaffFormReminder`),
  CONSTRAINT `FK_staff_form_reminder_users_identity_users` FOREIGN KEY (`IdfUser`) REFERENCES `identity_users` (`Id`),
  CONSTRAINT `FK_staff_form_reminder_users_staff_form_reminders` FOREIGN KEY (`IdfStaffFormReminder`) REFERENCES `staff_form_reminders` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
