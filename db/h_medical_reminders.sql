/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_medical_reminders`;
CREATE TABLE IF NOT EXISTS `h_medical_reminders` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfClient` bigint(10) NOT NULL,
  `IdfAssignedTo` bigint(10) NOT NULL,
  `Description` varchar(128) NOT NULL,
  `Datetime` datetime NOT NULL,
  `From` datetime NOT NULL,
  `To` datetime NOT NULL,
  `Reminder` tinyint(4) NOT NULL DEFAULT 1,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`),
  KEY `fk_h_medical_reminders_client_idx` (`IdfClient`),
  KEY `fk_h_medical_reminders_spp_idx` (`IdfAssignedTo`),
  CONSTRAINT `fk_h_medical_reminders_client` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_h_medical_reminders_spp` FOREIGN KEY (`IdfAssignedTo`) REFERENCES `staff_project_position` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
