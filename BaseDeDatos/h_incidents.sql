/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_incidents`;
CREATE TABLE IF NOT EXISTS `h_incidents` (
  `id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfPeriod` bigint(10) NOT NULL,
  `DateIncident` datetime NOT NULL,
  `TimeIncident` time DEFAULT NULL,
  `IsSeriousOcurrence` int(1) NOT NULL DEFAULT 0,
  `IdfTypeOfSeriousOccurrence` int(11) NOT NULL DEFAULT 0,
  `IdfRegion` int(11) DEFAULT NULL,
  `DateTimeWhenSeriousOccurrence` datetime DEFAULT NULL,
  `SentToMinistry` int(11) NOT NULL DEFAULT 0,
  `IdfMinistry` int(11) DEFAULT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  `DescName` varchar(256) NOT NULL,
  `IdfUmabIntervention` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `h_incidents_region_idx` (`IdfRegion`),
  KEY `h_incidents_ministry_idx` (`IdfMinistry`),
  KEY `h_incidents_typeofso_idx` (`IdfTypeOfSeriousOccurrence`),
  KEY `h_incidents_periods_idx` (`IdfPeriod`),
  CONSTRAINT `h_incidents_ministry` FOREIGN KEY (`IdfMinistry`) REFERENCES `h_ministeries` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_incidents_periods` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_incidents_region` FOREIGN KEY (`IdfRegion`) REFERENCES `h_region` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `h_incidents_typeofso` FOREIGN KEY (`IdfTypeOfSeriousOccurrence`) REFERENCES `h_type_serious_occurrence` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
