/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_incident_involved_people`;
CREATE TABLE IF NOT EXISTS `h_incident_involved_people` (
  `id` bigint(19) NOT NULL AUTO_INCREMENT,
  `idfIncident` bigint(10) NOT NULL,
  `IdfSPP` bigint(10) NOT NULL,
  `IdentifierGroup` varchar(2) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`id`),
  KEY `h_incident_involved_people_incident_idx` (`idfIncident`),
  KEY `h_incident_involved_people_spp_idx` (`IdfSPP`),
  CONSTRAINT `h_incident_involved_people_incident` FOREIGN KEY (`idfIncident`) REFERENCES `h_incidents` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_incident_involved_people_spp` FOREIGN KEY (`IdfSPP`) REFERENCES `staff_project_position` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
