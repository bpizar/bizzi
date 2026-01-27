/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `projects_clients`;
CREATE TABLE IF NOT EXISTS `projects_clients` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfProject` bigint(10) NOT NULL,
  `IdfClient` bigint(10) NOT NULL,
  `IdfPeriod` bigint(10) NOT NULL DEFAULT 113,
  `State` varchar(2) CHARACTER SET latin1 DEFAULT NULL,
  `IdfSPP` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_projects_clients_projects_idx` (`IdfProject`),
  KEY `fk_projects_clients_clients_idx` (`IdfClient`),
  KEY `fk_projects_clients_periods_idx` (`IdfPeriod`),
  CONSTRAINT `fk_projects_clients_clients` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_projects_clients_periods` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_projects_clients_projects` FOREIGN KEY (`IdfProject`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
