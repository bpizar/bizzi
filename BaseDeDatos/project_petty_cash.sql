/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_petty_cash`;
CREATE TABLE IF NOT EXISTS `project_petty_cash` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfProject` bigint(10) NOT NULL,
  `Amount` decimal(10,2) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `Date` datetime NOT NULL,
  `RegistrationDate` datetime NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  `IdfPeriod` bigint(10) NOT NULL,
  `IdfCategories` bigint(10) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_ project_petty_cash_projects_idx` (`IdfProject`),
  KEY `fk_project_petty_cash_periods_idx` (`IdfPeriod`),
  KEY `fk_ppc_ppcc_idx` (`IdfCategories`),
  CONSTRAINT `fk_ project_petty_cash_projects` FOREIGN KEY (`IdfProject`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_ppc_ppcc` FOREIGN KEY (`IdfCategories`) REFERENCES `project_pettycash_categories` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_project_petty_cash_periods` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
