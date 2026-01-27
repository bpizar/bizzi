/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_form_image_values`;
CREATE TABLE IF NOT EXISTS `staff_form_image_values` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfStaff` bigint(20) NOT NULL DEFAULT 0,
  `IdfStaffForm` bigint(20) NOT NULL DEFAULT 0,
  `Image` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `FormDateTime` datetime DEFAULT current_timestamp(),
  `DateTime` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `FK_staff_form_value_staff` (`IdfStaff`),
  KEY `FK_staff_form_value_staff_form` (`IdfStaffForm`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
