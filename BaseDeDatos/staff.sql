/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff`;
CREATE TABLE IF NOT EXISTS `staff` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfUser` bigint(10) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  `Color` varchar(20) NOT NULL DEFAULT '#5bc0de',
  `WorkStartDate` datetime DEFAULT NULL,
  `SocialInsuranceNumber` varchar(128) DEFAULT NULL,
  `HealthInsuranceNumber` varchar(128) DEFAULT NULL,
  `HomeAddress` varchar(256) DEFAULT NULL,
  `City` varchar(125) DEFAULT NULL,
  `HomePhone` varchar(128) DEFAULT NULL,
  `CellNumber` varchar(45) DEFAULT NULL,
  `SpouceName` varchar(128) DEFAULT NULL,
  `EmergencyPerson` varchar(128) DEFAULT NULL,
  `EmergencyPersonInfo` varchar(128) DEFAULT NULL,
  `AvailableForManyPrograms` int(11) DEFAULT 1,
  `tmpAccreditations` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `StaffUser_idx` (`IdfUser`),
  CONSTRAINT `fk_StaffUser` FOREIGN KEY (`IdfUser`) REFERENCES `identity_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
