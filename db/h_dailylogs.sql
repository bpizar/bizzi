/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_dailylogs`;
CREATE TABLE IF NOT EXISTS `h_dailylogs` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfPeriod` bigint(10) NOT NULL,
  `ProjectId` bigint(10) NOT NULL,
  `ClientId` bigint(10) NOT NULL,
  `Date` datetime NOT NULL,
  `UserId` bigint(10) NOT NULL,
  `Placement` varchar(1000) NOT NULL,
  `StaffOnShift` varchar(1000) NOT NULL COMMENT 'Must be deleted….. because this field can be many, so h_dailylog_involved_people table will be create.\nTake very care with mobile app after delete this field',
  `GeneralMood` varchar(1000) DEFAULT NULL,
  `InteractionStaff` varchar(1000) DEFAULT NULL,
  `InteractionPeers` varchar(1000) DEFAULT NULL,
  `School` varchar(1000) DEFAULT NULL,
  `Attended` varchar(1000) DEFAULT NULL,
  `InHouseProg` varchar(1000) DEFAULT NULL COMMENT 'This??????\nis project ??',
  `Comments` varchar(1000) DEFAULT NULL,
  `Health` varchar(1000) DEFAULT NULL,
  `ContactFamily` varchar(1000) DEFAULT NULL,
  `SeriousOccurrence` varchar(1000) DEFAULT ' ' COMMENT 'Mmmm ?????',
  `Other` varchar(1000) DEFAULT ' ',
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`),
  KEY `fk_h_dailylogs_projects_idx` (`ProjectId`),
  KEY `fk_h_dailylogs_clients_idx` (`ClientId`),
  KEY `fk_h_dailylogs_periods_idx` (`IdfPeriod`),
  CONSTRAINT `fk_h_dailylogs_clients` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_h_dailylogs_periods` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_h_dailylogs_projects` FOREIGN KEY (`ProjectId`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
