/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_form_reminders`;
CREATE TABLE IF NOT EXISTS `project_form_reminders` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfProjectForm` bigint(20) DEFAULT NULL,
  `IdfReminderLevel` bigint(20) DEFAULT NULL,
  `IdfPeriodType` bigint(20) DEFAULT NULL,
  `IdfPeriodValue` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_project_form_reminders_project_forms` (`IdfProjectForm`),
  CONSTRAINT `FK_project_form_reminders_project_forms` FOREIGN KEY (`IdfProjectForm`) REFERENCES `project_forms` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
