/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_form_image_values`;
CREATE TABLE IF NOT EXISTS `project_form_image_values` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfProject` bigint(20) NOT NULL DEFAULT 0,
  `IdfProjectForm` bigint(20) NOT NULL DEFAULT 0,
  `Image` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `FormDateTime` datetime DEFAULT current_timestamp(),
  `DateTime` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `FK_project_form_image_values_projects` (`IdfProject`),
  KEY `FK_project_form_image_values_project_forms` (`IdfProjectForm`),
  CONSTRAINT `FK_project_form_image_values_project_forms` FOREIGN KEY (`IdfProjectForm`) REFERENCES `project_forms` (`Id`),
  CONSTRAINT `FK_project_form_image_values_projects` FOREIGN KEY (`IdfProject`) REFERENCES `projects` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
