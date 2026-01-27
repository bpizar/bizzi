/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_injuries`;
CREATE TABLE IF NOT EXISTS `h_injuries` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfPeriod` bigint(10) NOT NULL,
  `IdfClient` bigint(10) NOT NULL,
  `IdfIncident` bigint(10) DEFAULT NULL,
  `IdfDegreeOfInjury` int(11) NOT NULL,
  `DescName` varchar(45) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  `DateOfInjury` datetime NOT NULL DEFAULT current_timestamp(),
  `DateReportedSupervisor` datetime DEFAULT current_timestamp(),
  `IdfSupervisor` bigint(10) DEFAULT NULL,
  `BodySerialized` longtext DEFAULT NULL,
  `ProjectId` bigint(10) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `h_injuries_period_idx` (`IdfPeriod`),
  KEY `h_injuries_client_idx` (`IdfClient`),
  KEY `h_injuries_incident_idx` (`IdfIncident`),
  KEY `h_injuries_degree_idx` (`IdfDegreeOfInjury`),
  KEY `h_injuries_projects_idx` (`ProjectId`),
  CONSTRAINT `h_injuries_client` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_injuries_degree` FOREIGN KEY (`IdfDegreeOfInjury`) REFERENCES `h_degree_of_injury` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_injuries_incident` FOREIGN KEY (`IdfIncident`) REFERENCES `h_incidents` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_injuries_period` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `h_injuries_projects` FOREIGN KEY (`ProjectId`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
