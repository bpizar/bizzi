/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `clients`;
CREATE TABLE IF NOT EXISTS `clients` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(250) NOT NULL,
  `LastName` varchar(250) NOT NULL,
  `BirthDate` datetime DEFAULT NULL,
  `PhoneNumber` varchar(50) DEFAULT NULL,
  `Email` varchar(150) DEFAULT NULL,
  `Notes` varchar(500) DEFAULT NULL,
  `Active` int(11) NOT NULL,
  `State` varchar(250) NOT NULL,
  `IdfImg` bigint(10) DEFAULT NULL,
  `SafetyPlan` text DEFAULT NULL,
  `tmpMotherName` varchar(256) DEFAULT NULL,
  `tmpMotherInfo` varchar(256) DEFAULT NULL,
  `tmpFatherName` varchar(256) DEFAULT NULL,
  `tmpFatherInfo` varchar(256) DEFAULT NULL,
  `tmpAgencyWorker` varchar(256) DEFAULT NULL,
  `tmpAgencyWorkerInfo` varchar(256) DEFAULT NULL,
  `tmpAgency` varchar(256) DEFAULT NULL,
  `tmpAgencyInfo` varchar(256) DEFAULT NULL,
  `tmpPlacement` varchar(256) DEFAULT NULL,
  `tmpSupervisor` varchar(256) DEFAULT NULL,
  `tmpSpecialProgram` varchar(256) DEFAULT NULL,
  `tmpSchool` varchar(256) DEFAULT NULL,
  `tmpSchoolInfo` varchar(256) DEFAULT NULL,
  `tmpTeacher` varchar(256) DEFAULT NULL,
  `tmpTeacherInfo` varchar(256) DEFAULT NULL,
  `tmpDoctorName` varchar(256) DEFAULT NULL,
  `tmpDoctorInfo` varchar(256) DEFAULT NULL,
  `tmpMedicationNotes` longtext DEFAULT NULL,
  `tmpAdditionalInformation` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
