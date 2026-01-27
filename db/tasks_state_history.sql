/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `tasks_state_history`;
CREATE TABLE IF NOT EXISTS `tasks_state_history` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfTask` bigint(10) NOT NULL,
  `IdfUser` bigint(10) NOT NULL,
  `CurrentDate` datetime NOT NULL,
  `IdfState` bigint(20) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_tasks_sta_hist_tasks_idx` (`IdfTask`),
  KEY `fk_tasks_sta_hist_Identity_users_idx` (`IdfUser`),
  KEY `fk_tasks_sta_hist_Statuses_idx` (`IdfState`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
