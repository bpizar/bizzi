/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `time_tracking_review`;
CREATE TABLE IF NOT EXISTS `time_tracking_review` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfStaffProjectPosition` bigint(10) NOT NULL,
  `IdfPeriod` bigint(10) NOT NULL,
  `SecondsScheduledTime` bigint(10) NOT NULL,
  `SecondsUserTracking` bigint(10) NOT NULL,
  `SecondsModifiedTracking` bigint(10) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`),
  KEY `fk_time_tracking_review_staffprojectposition_idx` (`IdfStaffProjectPosition`),
  KEY `fk_time_tracking_review_periods_idx` (`IdfPeriod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
