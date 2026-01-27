-- Bizzi database schema (generated from BaseDeDatos/*.sql)

CREATE DATABASE IF NOT EXISTS `bizzi` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE `bizzi`;



-- ===================== form_fields =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `form_fields`;
CREATE TABLE IF NOT EXISTS `form_fields` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Description` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `Placeholder` varchar(100) COLLATE utf8_spanish_ci DEFAULT NULL,
  `DataType` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Constraints` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== staff_form_image_values =====================

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


-- ===================== identity_roles =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `identity_roles`;
CREATE TABLE IF NOT EXISTS `identity_roles` (
  `Id` bigint(10) NOT NULL,
  `Rol` varchar(50) NOT NULL,
  `State` varchar(1) NOT NULL,
  `DisplayShortName` varchar(45) NOT NULL DEFAULT '-',
  `RolDescription` varchar(100) NOT NULL DEFAULT '-',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== projects =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `projects`;
CREATE TABLE IF NOT EXISTS `projects` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `ProjectName` varchar(500) NOT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `BeginDate` datetime(6) DEFAULT NULL,
  `EndDate` datetime(6) DEFAULT NULL,
  `Color` varchar(20) NOT NULL DEFAULT '#5bc0de',
  `State` varchar(2) NOT NULL DEFAULT 'C',
  `Visible` int(11) NOT NULL DEFAULT 1,
  `CreationDate` datetime DEFAULT current_timestamp(),
  `Address` varchar(256) DEFAULT NULL,
  `City` varchar(125) DEFAULT NULL,
  `Phone1` varchar(45) DEFAULT NULL,
  `Phone2` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_type_serious_occurrence =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_type_serious_occurrence`;
CREATE TABLE IF NOT EXISTS `h_type_serious_occurrence` (
  `Id` int(11) NOT NULL,
  `Description` varchar(256) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_pettycash_categories =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_pettycash_categories`;
CREATE TABLE IF NOT EXISTS `project_pettycash_categories` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Description` varchar(100) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== duplicate_tasks =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `duplicate_tasks`;
CREATE TABLE IF NOT EXISTS `duplicate_tasks` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `DuplicateValue` varchar(1) NOT NULL,
  `RepeatEvery` int(11) NOT NULL DEFAULT 0,
  `Weekly_Su` int(11) DEFAULT NULL,
  `Weekly_Mo` int(11) DEFAULT NULL,
  `Weekly_Tu` int(11) DEFAULT NULL,
  `Weekly_We` int(11) DEFAULT NULL,
  `Weekly_Th` int(11) DEFAULT NULL,
  `Weekly_Fr` int(11) DEFAULT NULL,
  `Weekly_Sa` int(11) DEFAULT NULL,
  `Monthly_Day` int(11) DEFAULT NULL,
  `Yearly_Month` int(11) DEFAULT NULL,
  `Yearly_MonthDay` int(11) DEFAULT NULL,
  `EndAfter` int(11) NOT NULL DEFAULT 0,
  `EndOn` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== common_errors =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `common_errors`;
CREATE TABLE IF NOT EXISTS `common_errors` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Description` varchar(500) NOT NULL,
  `RegistrationDate` datetime NOT NULL DEFAULT current_timestamp(),
  `IdfUser` bigint(10) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== time_tracking_auto =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `time_tracking_auto`;
CREATE TABLE IF NOT EXISTS `time_tracking_auto` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfUser` bigint(10) NOT NULL,
  `start` datetime NOT NULL,
  `Longitude` float DEFAULT NULL,
  `Latitude` float DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_time_tracking_auto_user_idx` (`IdfUser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== time_tracking_review =====================

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


-- ===================== statuses =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `statuses`;
CREATE TABLE IF NOT EXISTS `statuses` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `status` varchar(50) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== duplicate_scheduling =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `duplicate_scheduling`;
CREATE TABLE IF NOT EXISTS `duplicate_scheduling` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `DuplicateValue` varchar(1) NOT NULL,
  `RepeatEvery` int(11) NOT NULL DEFAULT 0,
  `Weekly_Su` int(11) DEFAULT NULL,
  `Weekly_Mo` int(11) DEFAULT NULL,
  `Weekly_Tu` int(11) DEFAULT NULL,
  `Weekly_We` int(11) DEFAULT NULL,
  `Weekly_Th` int(11) DEFAULT NULL,
  `Weekly_Fr` int(11) DEFAULT NULL,
  `Weekly_Sa` int(11) DEFAULT NULL,
  `Monthly_Day` int(11) DEFAULT NULL,
  `Yearly_Month` int(11) DEFAULT NULL,
  `Yearly_MonthDay` int(11) DEFAULT NULL,
  `EndAfter` int(11) NOT NULL DEFAULT 0,
  `EndOn` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== chat_rooms =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `chat_rooms`;
CREATE TABLE IF NOT EXISTS `chat_rooms` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== clients =====================

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


-- ===================== periods =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `periods`;
CREATE TABLE IF NOT EXISTS `periods` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `State` varchar(2) DEFAULT NULL,
  `Description` varchar(256) DEFAULT NULL,
  `From` datetime NOT NULL,
  `To` datetime NOT NULL,
  `IdfCreatedBy` bigint(10) DEFAULT 0,
  `CreationDate` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== time_tracking =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `time_tracking`;
CREATE TABLE IF NOT EXISTS `time_tracking` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfStaffProjectPosition` bigint(10) NOT NULL,
  `start` datetime NOT NULL,
  `end` datetime DEFAULT NULL,
  `status` int(11) NOT NULL,
  `startNote` varchar(1000) DEFAULT NULL,
  `endNote` varchar(500) DEFAULT NULL,
  `Longitude` float DEFAULT NULL,
  `Latitude` float DEFAULT NULL,
  `endLong` float DEFAULT NULL,
  `endLat` float DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_time_tracking_staffprojectposition_idx` (`IdfStaffProjectPosition`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_ministeries =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_ministeries`;
CREATE TABLE IF NOT EXISTS `h_ministeries` (
  `Id` int(11) NOT NULL,
  `Description` varchar(45) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== reminders =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `reminders`;
CREATE TABLE IF NOT EXISTS `reminders` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserId` bigint(20) NOT NULL,
  `Name` varchar(1000) NOT NULL,
  `Date` datetime NOT NULL,
  `DateRemind` datetime NOT NULL,
  `Type` varchar(250) NOT NULL,
  `Active` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_forms =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_forms`;
CREATE TABLE IF NOT EXISTS `project_forms` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Description` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Information` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `Template` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `TemplateFile` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `State` varchar(2) COLLATE utf8_spanish_ci DEFAULT NULL,
  `IdfRecurrence` bigint(20) DEFAULT NULL,
  `IdfRecurrenceDetail` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== settings_reminder_time =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `settings_reminder_time`;
CREATE TABLE IF NOT EXISTS `settings_reminder_time` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `MinutesBefore` bigint(10) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== tasks_state_history =====================

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


-- ===================== staff_forms =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_forms`;
CREATE TABLE IF NOT EXISTS `staff_forms` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Description` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Information` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `Template` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `TemplateFile` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `State` varchar(2) COLLATE utf8_spanish_ci DEFAULT NULL,
  `IdfRecurrence` bigint(20) DEFAULT NULL,
  `IdfRecurrenceDetail` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== todo_items =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `todo_items`;
CREATE TABLE IF NOT EXISTS `todo_items` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserId` bigint(20) NOT NULL,
  `Note` varchar(1000) NOT NULL,
  `Date` datetime NOT NULL,
  `DueDate` datetime NOT NULL,
  `Remind` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== identity_users =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `identity_users`;
CREATE TABLE IF NOT EXISTS `identity_users` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Email` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `State` varchar(1) NOT NULL,
  `RegistrationDate` datetime NOT NULL DEFAULT current_timestamp(),
  `LastName` varchar(45) DEFAULT NULL,
  `FirstName` varchar(45) DEFAULT NULL,
  `Face` longtext DEFAULT NULL,
  `IdfImg` bigint(10) NOT NULL,
  `IdOneSignal` varchar(256) DEFAULT NULL,
  `IdOneSignalWeb` varchar(256) DEFAULT NULL,
  `GeoTrackingEvery` int(11) NOT NULL DEFAULT 0,
  `FaceStamp` varchar(125) NOT NULL DEFAULT '-',
  `TFASecret` varchar(125) DEFAULT '-',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserName_UNIQUE` (`Email`),
  KEY `EmailIndex` (`Email`),
  KEY `Id` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== meetings =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `meetings`;
CREATE TABLE IF NOT EXISTS `meetings` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` bigint(20) NOT NULL,
  `RequiredAtt` varchar(1000) NOT NULL,
  `OptionalAtt` varchar(1000) NOT NULL,
  `Organizers` varchar(1000) NOT NULL,
  `Name` varchar(250) NOT NULL,
  `Description` varchar(1000) NOT NULL,
  `Date` datetime NOT NULL,
  `DateStart` datetime NOT NULL,
  `DateEnd` datetime NOT NULL,
  `Remind` int(11) NOT NULL,
  `Active` int(11) NOT NULL,
  `Type` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== positions =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `positions`;
CREATE TABLE IF NOT EXISTS `positions` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `State` varchar(2) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== tasks =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `tasks`;
CREATE TABLE IF NOT EXISTS `tasks` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Subject` varchar(512) NOT NULL DEFAULT '',
  `IdfStatus` bigint(20) DEFAULT NULL,
  `State` varchar(2) DEFAULT NULL,
  `Description` varchar(256) DEFAULT NULL,
  `Deadline` datetime DEFAULT NULL,
  `IdfAssignedTo` bigint(10) DEFAULT NULL,
  `RecurrencePattern` varchar(256) DEFAULT NULL,
  `RecurrenceException` varchar(256) DEFAULT NULL,
  `AllDay` int(11) DEFAULT NULL,
  `IdfCreatedBy` bigint(10) DEFAULT 0,
  `CreationDate` datetime NOT NULL DEFAULT current_timestamp(),
  `Lat` varchar(60) DEFAULT NULL,
  `Lon` varchar(60) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `IdDuplicate` bigint(10) DEFAULT NULL,
  `Hours` bigint(10) NOT NULL DEFAULT 0,
  `IdfPeriod` bigint(10) DEFAULT NULL,
  `IdfProject` bigint(10) DEFAULT NULL,
  `IdfAssignableRol` bigint(10) DEFAULT NULL,
  `Type` varchar(2) NOT NULL DEFAULT 'RQ',
  `IdfTaskParent` bigint(10) DEFAULT 0,
  `Notes` varchar(15000) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `TaskSPP_idx` (`IdfAssignedTo`),
  KEY `TaskProject_idx` (`IdfProject`),
  KEY `TaskPeriod_idx` (`IdfPeriod`),
  KEY `TaskStatus` (`IdfStatus`),
  KEY `fk_tasks_positions_idx` (`IdfAssignableRol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_assets =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_assets`;
CREATE TABLE IF NOT EXISTS `project_assets` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Amount` float NOT NULL,
  `TaxFed` int(11) DEFAULT NULL,
  `TaxProb` int(11) DEFAULT NULL,
  `Type` varchar(250) NOT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `Date` datetime NOT NULL,
  `ProjectId` bigint(20) NOT NULL,
  `Category` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== staff_project_position =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_project_position`;
CREATE TABLE IF NOT EXISTS `staff_project_position` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfStaff` bigint(20) NOT NULL,
  `IdfProject` bigint(20) NOT NULL,
  `IdfPosition` bigint(20) NOT NULL,
  `IdfPeriod` bigint(10) NOT NULL,
  `State` varchar(2) DEFAULT NULL,
  `Hours` bigint(20) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `SPPStaff_idx` (`IdfStaff`),
  KEY `SPPProject_idx` (`IdfProject`),
  KEY `SPPPosition_idx` (`IdfPosition`),
  KEY `fk_staff_project_position_periods_idx` (`IdfPeriod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== tasks_reminders =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `tasks_reminders`;
CREATE TABLE IF NOT EXISTS `tasks_reminders` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfTask` bigint(10) NOT NULL,
  `IdfSettingReminderTime` bigint(10) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  `IdfPeriod` bigint(10) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `fk_tasks_reminders_tasks_idx` (`IdfTask`),
  KEY `fk_tasks_reminders_settingReminder_idx` (`IdfSettingReminderTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== client_forms =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `client_forms`;
CREATE TABLE IF NOT EXISTS `client_forms` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Description` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `Information` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `Template` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `TemplateFile` longtext COLLATE utf8_spanish_ci DEFAULT NULL,
  `State` varchar(2) COLLATE utf8_spanish_ci DEFAULT NULL,
  `IdfRecurrence` bigint(20) DEFAULT NULL,
  `IdfRecurrenceDetail` bigint(20) unsigned DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_umab_intervention =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_umab_intervention`;
CREATE TABLE IF NOT EXISTS `h_umab_intervention` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(45) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_region =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_region`;
CREATE TABLE IF NOT EXISTS `h_region` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(45) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_degree_of_injury =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_degree_of_injury`;
CREATE TABLE IF NOT EXISTS `h_degree_of_injury` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(45) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_catalog =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_catalog`;
CREATE TABLE IF NOT EXISTS `h_catalog` (
  `id` varchar(10) NOT NULL,
  `IdentifierGroup` varchar(5) NOT NULL,
  `Description` varchar(125) NOT NULL,
  `Type` varchar(2) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== clients_images =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `clients_images`;
CREATE TABLE IF NOT EXISTS `clients_images` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) CHARACTER SET latin1 NOT NULL,
  `IdfClient` bigint(10) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_clients_images_clients_idx` (`IdfClient`),
  CONSTRAINT `fk_clients_images_clients` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_dailylogs =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_dailylogs`;
CREATE TABLE IF NOT EXISTS `h_dailylogs` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfPeriod` bigint(10) NOT NULL,
  `ProjectId` bigint(10) NOT NULL,
  `ClientId` bigint(10) NOT NULL,
  `Date` datetime NOT NULL,
  `UserId` bigint(10) NOT NULL,
  `Placement` varchar(1000) NOT NULL,
  `StaffOnShift` varchar(1000) NOT NULL COMMENT 'Must be deleted….. because this field can be many, so h_dailylog_involved_people table will be create.\nTake very care with mobile app after delete this field',
  `GeneralMood` varchar(1000) DEFAULT NULL,
  `InteractionStaff` varchar(1000) DEFAULT NULL,
  `InteractionPeers` varchar(1000) DEFAULT NULL,
  `School` varchar(1000) DEFAULT NULL,
  `Attended` varchar(1000) DEFAULT NULL,
  `InHouseProg` varchar(1000) DEFAULT NULL COMMENT 'This??????\nis project ??',
  `Comments` varchar(1000) DEFAULT NULL,
  `Health` varchar(1000) DEFAULT NULL,
  `ContactFamily` varchar(1000) DEFAULT NULL,
  `SeriousOccurrence` varchar(1000) DEFAULT ' ' COMMENT 'Mmmm ?????',
  `Other` varchar(1000) DEFAULT ' ',
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`),
  KEY `fk_h_dailylogs_projects_idx` (`ProjectId`),
  KEY `fk_h_dailylogs_clients_idx` (`ClientId`),
  KEY `fk_h_dailylogs_periods_idx` (`IdfPeriod`),
  CONSTRAINT `fk_h_dailylogs_clients` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_h_dailylogs_periods` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_h_dailylogs_projects` FOREIGN KEY (`ProjectId`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_petty_cash =====================

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


-- ===================== projects_clients =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `projects_clients`;
CREATE TABLE IF NOT EXISTS `projects_clients` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfProject` bigint(10) NOT NULL,
  `IdfClient` bigint(10) NOT NULL,
  `IdfPeriod` bigint(10) NOT NULL DEFAULT 113,
  `State` varchar(2) CHARACTER SET latin1 DEFAULT NULL,
  `IdfSPP` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_projects_clients_projects_idx` (`IdfProject`),
  KEY `fk_projects_clients_clients_idx` (`IdfClient`),
  KEY `fk_projects_clients_periods_idx` (`IdfPeriod`),
  CONSTRAINT `fk_projects_clients_clients` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_projects_clients_periods` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_projects_clients_projects` FOREIGN KEY (`IdfProject`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_form_fields =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_form_fields`;
CREATE TABLE IF NOT EXISTS `project_form_fields` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfProjectForm` bigint(20) DEFAULT 0,
  `IdfFormField` bigint(20) DEFAULT 0,
  `Position` int(11) NOT NULL DEFAULT 1,
  `IsEnabled` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`Id`),
  KEY `FK_project_form_fields_project_forms` (`IdfProjectForm`),
  KEY `FK_project_form_fields_form_fields` (`IdfFormField`),
  CONSTRAINT `FK_project_form_fields_form_fields` FOREIGN KEY (`IdfFormField`) REFERENCES `form_fields` (`Id`),
  CONSTRAINT `FK_project_form_fields_project_forms` FOREIGN KEY (`IdfProjectForm`) REFERENCES `project_forms` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_form_image_values =====================

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


-- ===================== project_form_reminders =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_form_reminders`;
CREATE TABLE IF NOT EXISTS `project_form_reminders` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfProjectForm` bigint(20) DEFAULT NULL,
  `IdfReminderLevel` bigint(20) DEFAULT NULL,
  `IdfPeriodType` bigint(20) DEFAULT NULL,
  `IdfPeriodValue` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_project_form_reminders_project_forms` (`IdfProjectForm`),
  CONSTRAINT `FK_project_form_reminders_project_forms` FOREIGN KEY (`IdfProjectForm`) REFERENCES `project_forms` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_form_values =====================

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


-- ===================== staff_form_fields =====================

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


-- ===================== staff_form_reminders =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_form_reminders`;
CREATE TABLE IF NOT EXISTS `staff_form_reminders` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfStaffForm` bigint(20) DEFAULT NULL,
  `IdfReminderLevel` bigint(20) DEFAULT NULL,
  `IdfPeriodType` bigint(20) DEFAULT NULL,
  `IdfPeriodValue` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_staff_form_reminders_staff_forms` (`IdfStaffForm`),
  CONSTRAINT `FK_staff_form_reminders_staff_forms` FOREIGN KEY (`IdfStaffForm`) REFERENCES `staff_forms` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== chat_identity_users_timestamp =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `chat_identity_users_timestamp`;
CREATE TABLE IF NOT EXISTS `chat_identity_users_timestamp` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfIdentityUser` bigint(10) NOT NULL,
  `ClientRoomVersion` bigint(10) NOT NULL DEFAULT 0,
  `ClientParticipantsVersion` bigint(10) NOT NULL DEFAULT 0,
  `ClientMessagesVersion` bigint(10) NOT NULL DEFAULT 0,
  `ServerRoomVersion` bigint(10) NOT NULL DEFAULT 0,
  `ServerParticipantsVersion` bigint(10) NOT NULL DEFAULT 0,
  `ServerMessagesVersion` bigint(10) NOT NULL DEFAULT 0,
  `DatePushSent` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `fk_chatIdentuserstime_Identity_Users_idx` (`IdfIdentityUser`),
  CONSTRAINT `fk_chatIdentuserstime_Identity_Users` FOREIGN KEY (`IdfIdentityUser`) REFERENCES `identity_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== chat_messages =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `chat_messages`;
CREATE TABLE IF NOT EXISTS `chat_messages` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Messages` varchar(256) NOT NULL,
  `Date` datetime NOT NULL,
  `IdfIdentityUserSender` bigint(10) NOT NULL,
  `IdfChatRoom` bigint(10) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_chatmessages_chatroom_idx` (`IdfChatRoom`),
  KEY `fk_chat_messages_identityusers_idx` (`IdfIdentityUserSender`),
  CONSTRAINT `fk_chat_messages_identityusers` FOREIGN KEY (`IdfIdentityUserSender`) REFERENCES `identity_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_chatmessages_chatroom` FOREIGN KEY (`IdfChatRoom`) REFERENCES `chat_rooms` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== chat_room_participants =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `chat_room_participants`;
CREATE TABLE IF NOT EXISTS `chat_room_participants` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfIdentityUser` bigint(10) NOT NULL,
  `IdfChatRoom` bigint(10) NOT NULL,
  `DateFrom` datetime NOT NULL,
  `DateTo` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_chatroompart_chat_room_idx` (`IdfChatRoom`),
  KEY `fk_chatroompart_identityuser_idx` (`IdfIdentityUser`),
  CONSTRAINT `fk_chatroompart_chat_room` FOREIGN KEY (`IdfChatRoom`) REFERENCES `chat_rooms` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_chatroompart_identityuser` FOREIGN KEY (`IdfIdentityUser`) REFERENCES `identity_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== identity_images =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `identity_images`;
CREATE TABLE IF NOT EXISTS `identity_images` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `IdfIdentity_user` bigint(10) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_identityImages_identityUsers_idx` (`IdfIdentity_user`),
  CONSTRAINT `fk_identityImages_identityUsers` FOREIGN KEY (`IdfIdentity_user`) REFERENCES `identity_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== identity_users_rol =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `identity_users_rol`;
CREATE TABLE IF NOT EXISTS `identity_users_rol` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfUser` bigint(10) NOT NULL,
  `IdfRol` bigint(10) NOT NULL,
  `State` varchar(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `_idx` (`IdfUser`),
  KEY `IdentityUsersRolIdfRol_Identity_Roles_Id_idx` (`IdfRol`),
  CONSTRAINT `IdentityUsersRolIdfRol_Identity_Roles_Id` FOREIGN KEY (`IdfRol`) REFERENCES `identity_roles` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `IdentityUsersRolIdfUser_Identity_Users_Id` FOREIGN KEY (`IdfUser`) REFERENCES `identity_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== staff =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff`;
CREATE TABLE IF NOT EXISTS `staff` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfUser` bigint(10) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  `Color` varchar(20) NOT NULL DEFAULT '#5bc0de',
  `WorkStartDate` datetime DEFAULT NULL,
  `SocialInsuranceNumber` varchar(128) DEFAULT NULL,
  `HealthInsuranceNumber` varchar(128) DEFAULT NULL,
  `HomeAddress` varchar(256) DEFAULT NULL,
  `City` varchar(125) DEFAULT NULL,
  `HomePhone` varchar(128) DEFAULT NULL,
  `CellNumber` varchar(45) DEFAULT NULL,
  `SpouceName` varchar(128) DEFAULT NULL,
  `EmergencyPerson` varchar(128) DEFAULT NULL,
  `EmergencyPersonInfo` varchar(128) DEFAULT NULL,
  `AvailableForManyPrograms` int(11) DEFAULT 1,
  `tmpAccreditations` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `StaffUser_idx` (`IdfUser`),
  CONSTRAINT `fk_StaffUser` FOREIGN KEY (`IdfUser`) REFERENCES `identity_users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_medical_reminders =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_medical_reminders`;
CREATE TABLE IF NOT EXISTS `h_medical_reminders` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfClient` bigint(10) NOT NULL,
  `IdfAssignedTo` bigint(10) NOT NULL,
  `Description` varchar(128) NOT NULL,
  `Datetime` datetime NOT NULL,
  `From` datetime NOT NULL,
  `To` datetime NOT NULL,
  `Reminder` tinyint(4) NOT NULL DEFAULT 1,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`),
  KEY `fk_h_medical_reminders_client_idx` (`IdfClient`),
  KEY `fk_h_medical_reminders_spp_idx` (`IdfAssignedTo`),
  CONSTRAINT `fk_h_medical_reminders_client` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_h_medical_reminders_spp` FOREIGN KEY (`IdfAssignedTo`) REFERENCES `staff_project_position` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== scheduling =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `scheduling`;
CREATE TABLE IF NOT EXISTS `scheduling` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `State` varchar(2) DEFAULT NULL,
  `From` datetime DEFAULT NULL,
  `To` datetime DEFAULT NULL,
  `IdfAssignedTo` bigint(10) DEFAULT 0,
  `AllDay` int(11) DEFAULT NULL,
  `IdfCreatedBy` bigint(10) DEFAULT 0,
  `CreationDate` datetime NOT NULL DEFAULT current_timestamp(),
  `IdDuplicate` bigint(10) DEFAULT NULL,
  `IdfProject` bigint(10) DEFAULT NULL,
  `IdfPeriod` bigint(10) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `SchedulingStaffProjectPosition_idx` (`IdfAssignedTo`),
  KEY `SchedulingProject_idx` (`IdfProject`),
  KEY `SchedulingPeriod_idx` (`IdfPeriod`),
  KEY `fk_scheduling_duplicate_scheduling_idx` (`IdDuplicate`),
  CONSTRAINT `SchedulingPeriod` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `SchedulingProject` FOREIGN KEY (`IdfProject`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `SchedulingStaffProjectPosition` FOREIGN KEY (`IdfAssignedTo`) REFERENCES `staff_project_position` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_scheduling_duplicate_scheduling` FOREIGN KEY (`IdDuplicate`) REFERENCES `duplicate_scheduling` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== client_form_fields =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `client_form_fields`;
CREATE TABLE IF NOT EXISTS `client_form_fields` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfClientForm` bigint(20) DEFAULT 0,
  `IdfFormField` bigint(20) DEFAULT 0,
  `Position` int(11) NOT NULL DEFAULT 1,
  `IsEnabled` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`Id`),
  KEY `FK__client_forms` (`IdfClientForm`),
  KEY `FK__form_fields` (`IdfFormField`),
  CONSTRAINT `FK__client_forms` FOREIGN KEY (`IdfClientForm`) REFERENCES `client_forms` (`Id`),
  CONSTRAINT `FK__form_fields` FOREIGN KEY (`IdfFormField`) REFERENCES `form_fields` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== client_form_image_values =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `client_form_image_values`;
CREATE TABLE IF NOT EXISTS `client_form_image_values` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfClient` bigint(20) NOT NULL DEFAULT 0,
  `IdfClientForm` bigint(20) NOT NULL DEFAULT 0,
  `Image` varchar(50) COLLATE utf8_spanish_ci DEFAULT NULL,
  `FormDateTime` datetime DEFAULT NULL,
  `DateTime` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_client_form_image_values_clients` (`IdfClient`),
  KEY `FK_client_form_image_values_client_forms` (`IdfClientForm`),
  CONSTRAINT `FK_client_form_image_values_client_forms` FOREIGN KEY (`IdfClientForm`) REFERENCES `client_forms` (`Id`),
  CONSTRAINT `FK_client_form_image_values_clients` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== client_form_reminders =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `client_form_reminders`;
CREATE TABLE IF NOT EXISTS `client_form_reminders` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfClientForm` bigint(20) DEFAULT NULL,
  `IdfReminderLevel` bigint(20) DEFAULT NULL,
  `IdfPeriodType` bigint(20) DEFAULT NULL,
  `IdfPeriodValue` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_client_form_reminders_client_forms` (`IdfClientForm`),
  CONSTRAINT `FK_client_form_reminders_client_forms` FOREIGN KEY (`IdfClientForm`) REFERENCES `client_forms` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== client_form_values =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `client_form_values`;
CREATE TABLE IF NOT EXISTS `client_form_values` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfClient` bigint(20) NOT NULL DEFAULT 0,
  `IdfClientForm` bigint(20) NOT NULL DEFAULT 0,
  `FormDateTime` datetime DEFAULT NULL,
  `DateTime` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_client_form_values_clients` (`IdfClient`),
  KEY `FK_client_form_values_client_forms` (`IdfClientForm`),
  CONSTRAINT `FK_client_form_values_client_forms` FOREIGN KEY (`IdfClientForm`) REFERENCES `client_forms` (`Id`),
  CONSTRAINT `FK_client_form_values_clients` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_incidents =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_incidents`;
CREATE TABLE IF NOT EXISTS `h_incidents` (
  `id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfPeriod` bigint(10) NOT NULL,
  `DateIncident` datetime NOT NULL,
  `TimeIncident` time DEFAULT NULL,
  `IsSeriousOcurrence` int(1) NOT NULL DEFAULT 0,
  `IdfTypeOfSeriousOccurrence` int(11) NOT NULL DEFAULT 0,
  `IdfRegion` int(11) DEFAULT NULL,
  `DateTimeWhenSeriousOccurrence` datetime DEFAULT NULL,
  `SentToMinistry` int(11) NOT NULL DEFAULT 0,
  `IdfMinistry` int(11) DEFAULT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  `DescName` varchar(256) NOT NULL,
  `IdfUmabIntervention` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `h_incidents_region_idx` (`IdfRegion`),
  KEY `h_incidents_ministry_idx` (`IdfMinistry`),
  KEY `h_incidents_typeofso_idx` (`IdfTypeOfSeriousOccurrence`),
  KEY `h_incidents_periods_idx` (`IdfPeriod`),
  CONSTRAINT `h_incidents_ministry` FOREIGN KEY (`IdfMinistry`) REFERENCES `h_ministeries` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_incidents_periods` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_incidents_region` FOREIGN KEY (`IdfRegion`) REFERENCES `h_region` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `h_incidents_typeofso` FOREIGN KEY (`IdfTypeOfSeriousOccurrence`) REFERENCES `h_type_serious_occurrence` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_dailylog_involved_people =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_dailylog_involved_people`;
CREATE TABLE IF NOT EXISTS `h_dailylog_involved_people` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfDailyLog` bigint(10) NOT NULL,
  `IdfSPP` bigint(10) NOT NULL,
  `IdentifierGroup` varchar(2) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`Id`),
  KEY `h_dailylog_involved_people_dailyLog_idx` (`IdfDailyLog`),
  KEY `h_dailylog_involved_people_spp_idx` (`IdfSPP`),
  CONSTRAINT `h_dailylog_involved_people_dailyLog` FOREIGN KEY (`IdfDailyLog`) REFERENCES `h_dailylogs` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_dailylog_involved_people_spp` FOREIGN KEY (`IdfSPP`) REFERENCES `staff_project_position` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_form_reminder_users =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_form_reminder_users`;
CREATE TABLE IF NOT EXISTS `project_form_reminder_users` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfProjectFormReminder` bigint(20) DEFAULT NULL,
  `IdfUser` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_project_form_reminder_users_project_form_reminders` (`IdfProjectFormReminder`),
  KEY `FK_project_form_reminder_users_identity_users` (`IdfUser`),
  CONSTRAINT `FK_project_form_reminder_users_identity_users` FOREIGN KEY (`IdfUser`) REFERENCES `identity_users` (`Id`),
  CONSTRAINT `FK_project_form_reminder_users_project_form_reminders` FOREIGN KEY (`IdfProjectFormReminder`) REFERENCES `project_form_reminders` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_form_field_values =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_form_field_values`;
CREATE TABLE IF NOT EXISTS `project_form_field_values` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfProjectFormValue` bigint(20) NOT NULL DEFAULT 0,
  `IdfFormField` bigint(20) NOT NULL DEFAULT 0,
  `Value` longtext NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_project_form_field_values_project_form_values` (`IdfProjectFormValue`),
  KEY `FK_project_form_field_values_form_fields` (`IdfFormField`),
  CONSTRAINT `FK_project_form_field_values_form_fields` FOREIGN KEY (`IdfFormField`) REFERENCES `form_fields` (`Id`),
  CONSTRAINT `FK_project_form_field_values_project_form_values` FOREIGN KEY (`IdfProjectFormValue`) REFERENCES `project_form_values` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== staff_form_reminder_users =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_form_reminder_users`;
CREATE TABLE IF NOT EXISTS `staff_form_reminder_users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdfStaffFormReminder` bigint(20) DEFAULT NULL,
  `IdfUser` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_staff_form_reminder_users_identity_users` (`IdfUser`),
  KEY `FK_staff_form_reminder_users_staff_form_reminders` (`IdfStaffFormReminder`),
  CONSTRAINT `FK_staff_form_reminder_users_identity_users` FOREIGN KEY (`IdfUser`) REFERENCES `identity_users` (`Id`),
  CONSTRAINT `FK_staff_form_reminder_users_staff_form_reminders` FOREIGN KEY (`IdfStaffFormReminder`) REFERENCES `staff_form_reminders` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== chat_message_participant_state =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `chat_message_participant_state`;
CREATE TABLE IF NOT EXISTS `chat_message_participant_state` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfMessage` bigint(10) NOT NULL,
  `IdfParticipant` bigint(10) NOT NULL,
  `Delivered` varchar(2) NOT NULL DEFAULT '0',
  `Read` varchar(2) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `fk_chatmsgpartstate_part_idx` (`IdfParticipant`),
  KEY `fk_chatmsgpartstate_chatmessages_idx` (`IdfMessage`),
  CONSTRAINT `fk_chatmsgpartstate_chatmessages` FOREIGN KEY (`IdfMessage`) REFERENCES `chat_messages` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_chatmsgpartstate_part` FOREIGN KEY (`IdfParticipant`) REFERENCES `chat_room_participants` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== project_owners =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `project_owners`;
CREATE TABLE IF NOT EXISTS `project_owners` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfProject` bigint(10) NOT NULL,
  `IdfOwner` bigint(10) NOT NULL,
  `State` varchar(2) NOT NULL,
  `IdfPeriod` bigint(10) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `ProjectOwnerProject_idx` (`IdfProject`),
  KEY `ProjectOwnerStaff_idx` (`IdfOwner`),
  KEY `fk_project:owners_periods_idx` (`IdfPeriod`),
  CONSTRAINT `ProjectOwnerProject` FOREIGN KEY (`IdfProject`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `ProjectOwnerStaff` FOREIGN KEY (`IdfOwner`) REFERENCES `staff` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_project:owners_periods` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== staff_form_values =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_form_values`;
CREATE TABLE IF NOT EXISTS `staff_form_values` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfStaff` bigint(20) NOT NULL DEFAULT 0,
  `IdfStaffForm` bigint(20) NOT NULL DEFAULT 0,
  `FormDateTime` datetime DEFAULT current_timestamp(),
  `DateTime` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `FK_staff_form_value_staff` (`IdfStaff`),
  KEY `FK_staff_form_value_staff_form` (`IdfStaffForm`),
  CONSTRAINT `FK_staff_form_values_staff` FOREIGN KEY (`IdfStaff`) REFERENCES `staff` (`Id`),
  CONSTRAINT `FK_staff_form_values_staff_forms` FOREIGN KEY (`IdfStaffForm`) REFERENCES `staff_forms` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== staff_period_settings =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `staff_period_settings`;
CREATE TABLE IF NOT EXISTS `staff_period_settings` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfStaff` bigint(20) NOT NULL,
  `IdfPeriod` bigint(20) NOT NULL,
  `WorkingHours` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `fk_staff_period_sett_unique` (`IdfPeriod`,`IdfStaff`),
  KEY `fk_staff_period_sett_staff_idx` (`IdfStaff`),
  KEY `fk_staff_period_sett_period_idx` (`IdfPeriod`),
  CONSTRAINT `fk_staff_period_sett_period` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_staff_period_sett_staff` FOREIGN KEY (`IdfStaff`) REFERENCES `staff` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== client_form_reminder_users =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `client_form_reminder_users`;
CREATE TABLE IF NOT EXISTS `client_form_reminder_users` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `IdfClientFormReminder` bigint(20) DEFAULT NULL,
  `IdfUser` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_client_form_reminder_users_client_form_reminders` (`IdfClientFormReminder`),
  KEY `FK_client_form_reminder_users_identity_users` (`IdfUser`),
  CONSTRAINT `FK_client_form_reminder_users_client_form_reminders` FOREIGN KEY (`IdfClientFormReminder`) REFERENCES `client_form_reminders` (`Id`),
  CONSTRAINT `FK_client_form_reminder_users_identity_users` FOREIGN KEY (`IdfUser`) REFERENCES `identity_users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== client_form_field_values =====================

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


-- ===================== h_clients_incident =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_clients_incident`;
CREATE TABLE IF NOT EXISTS `h_clients_incident` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfClient` bigint(10) NOT NULL,
  `IdfIncident` bigint(10) NOT NULL,
  `State` varchar(2) DEFAULT 'C',
  PRIMARY KEY (`Id`),
  KEY `h_clients_incident_client_idx` (`IdfClient`),
  KEY `h_clients_incident_incident_idx` (`IdfIncident`),
  CONSTRAINT `h_clients_incident_client` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_clients_incident_incident` FOREIGN KEY (`IdfIncident`) REFERENCES `h_incidents` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_incident_involved_people =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_incident_involved_people`;
CREATE TABLE IF NOT EXISTS `h_incident_involved_people` (
  `id` bigint(19) NOT NULL AUTO_INCREMENT,
  `idfIncident` bigint(10) NOT NULL,
  `IdfSPP` bigint(10) NOT NULL,
  `IdentifierGroup` varchar(2) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  PRIMARY KEY (`id`),
  KEY `h_incident_involved_people_incident_idx` (`idfIncident`),
  KEY `h_incident_involved_people_spp_idx` (`IdfSPP`),
  CONSTRAINT `h_incident_involved_people_incident` FOREIGN KEY (`idfIncident`) REFERENCES `h_incidents` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_incident_involved_people_spp` FOREIGN KEY (`IdfSPP`) REFERENCES `staff_project_position` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_incident_values =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_incident_values`;
CREATE TABLE IF NOT EXISTS `h_incident_values` (
  `id` bigint(10) NOT NULL AUTO_INCREMENT,
  `idfIncident` bigint(10) NOT NULL,
  `idfCatalog` varchar(10) NOT NULL,
  `Value` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `h_incident_values_catalog_idx` (`idfCatalog`),
  KEY `h_incident_values_incident_idx` (`idfIncident`),
  CONSTRAINT `h_incident_values_catalog` FOREIGN KEY (`idfCatalog`) REFERENCES `h_catalog` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_incident_values_incident` FOREIGN KEY (`idfIncident`) REFERENCES `h_incidents` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== h_injuries =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_injuries`;
CREATE TABLE IF NOT EXISTS `h_injuries` (
  `Id` bigint(10) NOT NULL AUTO_INCREMENT,
  `IdfPeriod` bigint(10) NOT NULL,
  `IdfClient` bigint(10) NOT NULL,
  `IdfIncident` bigint(10) DEFAULT NULL,
  `IdfDegreeOfInjury` int(11) NOT NULL,
  `DescName` varchar(45) NOT NULL,
  `State` varchar(2) NOT NULL DEFAULT 'C',
  `DateOfInjury` datetime NOT NULL DEFAULT current_timestamp(),
  `DateReportedSupervisor` datetime DEFAULT current_timestamp(),
  `IdfSupervisor` bigint(10) DEFAULT NULL,
  `BodySerialized` longtext DEFAULT NULL,
  `ProjectId` bigint(10) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `h_injuries_period_idx` (`IdfPeriod`),
  KEY `h_injuries_client_idx` (`IdfClient`),
  KEY `h_injuries_incident_idx` (`IdfIncident`),
  KEY `h_injuries_degree_idx` (`IdfDegreeOfInjury`),
  KEY `h_injuries_projects_idx` (`ProjectId`),
  CONSTRAINT `h_injuries_client` FOREIGN KEY (`IdfClient`) REFERENCES `clients` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_injuries_degree` FOREIGN KEY (`IdfDegreeOfInjury`) REFERENCES `h_degree_of_injury` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_injuries_incident` FOREIGN KEY (`IdfIncident`) REFERENCES `h_incidents` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_injuries_period` FOREIGN KEY (`IdfPeriod`) REFERENCES `periods` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `h_injuries_projects` FOREIGN KEY (`ProjectId`) REFERENCES `projects` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


-- ===================== staff_form_field_values =====================

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


-- ===================== h_injury_values =====================

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `h_injury_values`;
CREATE TABLE IF NOT EXISTS `h_injury_values` (
  `id` bigint(10) NOT NULL AUTO_INCREMENT,
  `idfInjury` bigint(10) NOT NULL,
  `idfCatalog` varchar(7) NOT NULL,
  `Value` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `h_incident_values_catalog_idx` (`idfCatalog`),
  KEY `h_injury_values_idx` (`idfInjury`),
  CONSTRAINT `h_injury_values_cat` FOREIGN KEY (`idfCatalog`) REFERENCES `h_catalog` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `h_injury_values_injuries` FOREIGN KEY (`idfInjury`) REFERENCES `h_injuries` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
