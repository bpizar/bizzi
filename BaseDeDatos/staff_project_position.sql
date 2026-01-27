/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_project_position`;
CREATE TABLE IF NOT EXISTS `staff_project_position` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfStaff` bigint(20) NOT NULL,
  `IdfProject` bigint(20) NOT NULL,
  `IdfPosition` bigint(20) NOT NULL,
  `IdfPeriod` bigint(10) NOT NULL,
  `State` varchar(2) DEFAULT NULL,
  `Hours` bigint(20) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `SPPStaff_idx` (`IdfStaff`),
  KEY `SPPProject_idx` (`IdfProject`),
  KEY `SPPPosition_idx` (`IdfPosition`),
  KEY `fk_staff_project_position_periods_idx` (`IdfPeriod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
