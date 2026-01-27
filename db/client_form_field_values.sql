/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `client_form_field_values`;
CREATE TABLE IF NOT EXISTS `client_form_field_values` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfClientFormValue` bigint(20) NOT NULL DEFAULT 0,
  `IdfFormField` bigint(20) NOT NULL DEFAULT 0,
  `Value` longtext NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_client_form_field_values_client_form_values` (`IdfClientFormValue`),
  KEY `FK_client_form_field_values_form_fields` (`IdfFormField`),
  CONSTRAINT `FK_client_form_field_values_client_form_values` FOREIGN KEY (`IdfClientFormValue`) REFERENCES `client_form_values` (`Id`),
  CONSTRAINT `FK_client_form_field_values_form_fields` FOREIGN KEY (`IdfFormField`) REFERENCES `form_fields` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
