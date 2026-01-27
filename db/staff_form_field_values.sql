/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_form_field_values`;
CREATE TABLE IF NOT EXISTS `staff_form_field_values` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfStaffFormValue` bigint(20) NOT NULL DEFAULT 0,
  `IdfFormField` bigint(20) NOT NULL DEFAULT 0,
  `Value` longtext COLLATE utf8_spanish_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_staff_form_field_values_staff_form_values` (`IdfStaffFormValue`),
  KEY `FK_staff_form_field_values_form_fields` (`IdfFormField`),
  CONSTRAINT `FK_staff_form_field_values_form_fields` FOREIGN KEY (`IdfFormField`) REFERENCES `form_fields` (`Id`),
  CONSTRAINT `FK_staff_form_field_values_staff_form_values` FOREIGN KEY (`IdfStaffFormValue`) REFERENCES `staff_form_values` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
