/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_incident_values`;
CREATE TABLE IF NOT EXISTS `h_incident_values` (
  `id` bigint(10) NOT NULL AUTO_INCREMENT,
  `idfIncident` bigint(10) NOT NULL,
  `idfCatalog` varchar(10) NOT NULL,
  `Value` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `h_incident_values_catalog_idx` (`idfCatalog`),
  KEY `h_incident_values_incident_idx` (`idfIncident`),
  CONSTRAINT `h_incident_values_catalog` FOREIGN KEY (`idfCatalog`) REFERENCES `h_catalog` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_incident_values_incident` FOREIGN KEY (`idfIncident`) REFERENCES `h_incidents` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
