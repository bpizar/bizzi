/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_clients_incident`;
CREATE TABLE IF NOT EXISTS `h_clients_incident` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfClient` bigint(10) NOT NULL,
  `IdfIncident` bigint(10) NOT NULL,
  `State` varchar(2) DEFAULT 'C',
  PRIMARY KEY (`Id`),
  KEY `h_clients_incident_client_idx` (`IdfClient`),
  KEY `h_clients_incident_incident_idx` (`IdfIncident`),
  CONSTRAINT `h_clients_incident_client` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_clients_incident_incident` FOREIGN KEY (`IdfIncident`) REFERENCES `h_incidents` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
