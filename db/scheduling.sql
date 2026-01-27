/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `scheduling`;
CREATE TABLE IF NOT EXISTS `scheduling` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `State` varchar(2) DEFAULT NULL,
  `From` datetime DEFAULT NULL,
  `To` datetime DEFAULT NULL,
  `IdfAssignedTo` bigint(10) DEFAULT 0,
  `AllDay` int(11) DEFAULT NULL,
  `IdfCreatedBy` bigint(10) DEFAULT 0,
  `CreationDate` datetime NOT NULL DEFAULT current_timestamp(),
  `IdDuplicate` bigint(10) DEFAULT NULL,
  `IdfProject` bigint(10) DEFAULT NULL,
  `IdfPeriod` bigint(10) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `SchedulingStaffProjectPosition_idx` (`IdfAssignedTo`),
  KEY `SchedulingProject_idx` (`IdfProject`),
  KEY `SchedulingPeriod_idx` (`IdfPeriod`),
  KEY `fk_scheduling_duplicate_scheduling_idx` (`IdDuplicate`),
  CONSTRAINT `SchedulingPeriod` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `SchedulingProject` FOREIGN KEY (`IdfProject`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `SchedulingStaffProjectPosition` FOREIGN KEY (`IdfAssignedTo`) REFERENCES `staff_project_position` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_scheduling_duplicate_scheduling` FOREIGN KEY (`IdDuplicate`) REFERENCES `duplicate_scheduling` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
