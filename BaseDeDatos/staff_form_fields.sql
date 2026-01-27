/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_form_fields`;
CREATE TABLE IF NOT EXISTS `staff_form_fields` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfStaffForm` bigint(20) DEFAULT 0,
  `IdfFormField` bigint(20) DEFAULT 0,
  `Position` int(11) NOT NULL DEFAULT 1,
  `IsEnabled` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`Id`),
  KEY `FK_staff_form_fields_staff_form` (`IdfStaffForm`),
  KEY `FK_staff_form_fields_form_field` (`IdfFormField`),
  CONSTRAINT `FK_staff_form_fields_form_field` FOREIGN KEY (`IdfFormField`) REFERENCES `form_fields` (`Id`),
  CONSTRAINT `FK_staff_form_fields_staff_form` FOREIGN KEY (`IdfStaffForm`) REFERENCES `staff_forms` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
