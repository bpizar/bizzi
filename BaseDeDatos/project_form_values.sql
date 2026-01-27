/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_form_values`;
CREATE TABLE IF NOT EXISTS `project_form_values` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfProject` bigint(20) NOT NULL DEFAULT 0,
  `IdfProjectForm` bigint(20) NOT NULL DEFAULT 0,
  `FormDateTime` datetime DEFAULT NULL,
  `DateTime` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK__projects` (`IdfProject`),
  KEY `FK__project_forms` (`IdfProjectForm`),
  CONSTRAINT `FK__project_forms` FOREIGN KEY (`IdfProjectForm`) REFERENCES `project_forms` (`Id`),
  CONSTRAINT `FK__projects` FOREIGN KEY (`IdfProject`) REFERENCES `projects` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
