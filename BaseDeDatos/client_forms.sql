/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `client_forms`;
CREATE TABLE IF NOT EXISTS `client_forms` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Description` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Information` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `Template` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `TemplateFile` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `State` varchar(2) COLLATE utf8_spanish_ci DEFAULT NULL,
  `IdfRecurrence` bigint(20) DEFAULT NULL,
  `IdfRecurrenceDetail` bigint(20) unsigned DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
