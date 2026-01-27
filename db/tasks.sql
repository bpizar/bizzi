/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `tasks`;
CREATE TABLE IF NOT EXISTS `tasks` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Subject` varchar(512) NOT NULL DEFAULT '',
  `IdfStatus` bigint(20) DEFAULT NULL,
  `State` varchar(2) DEFAULT NULL,
  `Description` varchar(256) DEFAULT NULL,
  `Deadline` datetime DEFAULT NULL,
  `IdfAssignedTo` bigint(10) DEFAULT NULL,
  `RecurrencePattern` varchar(256) DEFAULT NULL,
  `RecurrenceException` varchar(256) DEFAULT NULL,
  `AllDay` int(11) DEFAULT NULL,
  `IdfCreatedBy` bigint(10) DEFAULT 0,
  `CreationDate` datetime NOT NULL DEFAULT current_timestamp(),
  `Lat` varchar(60) DEFAULT NULL,
  `Lon` varchar(60) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `IdDuplicate` bigint(10) DEFAULT NULL,
  `Hours` bigint(10) NOT NULL DEFAULT 0,
  `IdfPeriod` bigint(10) DEFAULT NULL,
  `IdfProject` bigint(10) DEFAULT NULL,
  `IdfAssignableRol` bigint(10) DEFAULT NULL,
  `Type` varchar(2) NOT NULL DEFAULT 'RQ',
  `IdfTaskParent` bigint(10) DEFAULT 0,
  `Notes` varchar(15000) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `TaskSPP_idx` (`IdfAssignedTo`),
  KEY `TaskProject_idx` (`IdfProject`),
  KEY `TaskPeriod_idx` (`IdfPeriod`),
  KEY `TaskStatus` (`IdfStatus`),
  KEY `fk_tasks_positions_idx` (`IdfAssignableRol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
