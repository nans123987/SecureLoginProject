-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: securelogindb
-- ------------------------------------------------------
-- Server version	5.5.49

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_roles`
--

DROP TABLE IF EXISTS `tbl_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_roles` (
  `Role_id` varchar(5) NOT NULL,
  `Role_desc` varchar(10) NOT NULL,
  PRIMARY KEY (`Role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_roles`
--

LOCK TABLES `tbl_roles` WRITE;
/*!40000 ALTER TABLE `tbl_roles` DISABLE KEYS */;
INSERT INTO `tbl_roles` VALUES ('1000','ROLE_USER'),('1001','ROLE_ADMIN');
/*!40000 ALTER TABLE `tbl_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_userinfo`
--

DROP TABLE IF EXISTS `tbl_userinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_userinfo` (
  `UserId` varchar(36) NOT NULL,
  `Username` varchar(100) NOT NULL,
  `Password` varchar(100) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `DOB` varchar(30) NOT NULL,
  `LockedStatus` tinyint(1) NOT NULL,
  `Role_id` varchar(5) NOT NULL,
  `PwdHashSalt` varchar(100) NOT NULL,
  PRIMARY KEY (`UserId`),
  KEY `roles_foreignKey_idx` (`Role_id`),
  CONSTRAINT `roles_foreignKey` FOREIGN KEY (`Role_id`) REFERENCES `tbl_roles` (`Role_id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_userinfo`
--

LOCK TABLES `tbl_userinfo` WRITE;
/*!40000 ALTER TABLE `tbl_userinfo` DISABLE KEYS */;
INSERT INTO `tbl_userinfo` VALUES ('04aa8c5c-19ab-4107-a8fe-7e36ec581819','abhimanyu.sarin','$2a$10$5AnlpVB5Q.V0/n32lFim.uCm/GZvbN14qhewG/EWaOG2mJUXq8fpS','aby.sarin@gmail.com','11/20/1988',0,'1000','$2a$10$5AnlpVB5Q.V0/n32lFim.u'),('29e0d68d-a2e5-4d61-9b6c-e03708d33d65','james2016','$2a$10$tb1d7WBAbcHWzOv8wYC7/OGkIPF1N6p8vH3ZajDwonoEDRVTFcf6C','james@mavs.uta.edu','11/20/1988',0,'1000','$2a$10$tb1d7WBAbcHWzOv8wYC7/O'),('584203c9-bf66-454e-9df8-ce223aafb47e','nandan301190','$2a$10$NRGX78WAI03Yy4BE.ME7MuAbOmM2HD6nG4I7ExsRZ.jVd3ht.Fi.a','nandanthareja@gmail.com','11/20/1990',1,'1000','$2a$10$NRGX78WAI03Yy4BE.ME7Mu'),('b01f4f4e-ef20-4785-8930-13d3f20f3c5c','nandan2003','$2a$10$WGHY4SmLEYx/x8zrBygc6uD0qgcSJ/iWK/OCKlWfnPmCorObU6MlS','nandanthareja301190@gmail.com','11/20/1988',0,'1000','$2a$10$WGHY4SmLEYx/x8zrBygc6u');
/*!40000 ALTER TABLE `tbl_userinfo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-05-12 19:51:40
