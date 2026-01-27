/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `identity_users`;
CREATE TABLE IF NOT EXISTS `identity_users` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Email` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `State` varchar(1) NOT NULL,
  `RegistrationDate` datetime NOT NULL DEFAULT current_timestamp(),
  `LastName` varchar(45) DEFAULT NULL,
  `FirstName` varchar(45) DEFAULT NULL,
  `Face` longtext DEFAULT NULL,
  `IdfImg` bigint(10) NOT NULL,
  `IdOneSignal` varchar(256) DEFAULT NULL,
  `IdOneSignalWeb` varchar(256) DEFAULT NULL,
  `GeoTrackingEvery` int(11) NOT NULL DEFAULT 0,
  `FaceStamp` varchar(125) NOT NULL DEFAULT '-',
  `TFASecret` varchar(125) DEFAULT '-',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserName_UNIQUE` (`Email`),
  KEY `EmailIndex` (`Email`),
  KEY `Id` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
