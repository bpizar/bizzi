/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_period_settings`;
CREATE TABLE IF NOT EXISTS `staff_period_settings` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfStaff` bigint(20) NOT NULL,
  `IdfPeriod` bigint(20) NOT NULL,
  `WorkingHours` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `fk_staff_period_sett_unique` (`IdfPeriod`,`IdfStaff`),
  KEY `fk_staff_period_sett_staff_idx` (`IdfStaff`),
  KEY `fk_staff_period_sett_period_idx` (`IdfPeriod`),
  CONSTRAINT `fk_staff_period_sett_period` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_staff_period_sett_staff` FOREIGN KEY (`IdfStaff`) REFERENCES `staff` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
