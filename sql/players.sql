/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-06-07 19:40:20
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `players`
-- ----------------------------
DROP TABLE IF EXISTS `players`;
CREATE TABLE `players` (
  `PlayerID` int(32) NOT NULL AUTO_INCREMENT,
  `AccountID` int(32) NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Rank` int(32) NOT NULL,
  `PC_Cafe` int(32) NOT NULL,
  `Emblem` int(32) NOT NULL,
  `Exp` int(32) NOT NULL,
  `GP` int(32) NOT NULL,
  `Money` int(32) NOT NULL,
  `ClanID` int(32) NOT NULL,
  `Effect1` int(32) NOT NULL, //Coupon1
  `Effect2` int(32) NOT NULL, //Coupon2
  `Effect3` int(32) NOT NULL, //Coupon3
   `Effect4` int(32) NOT NULL, //Coupon4 
   `Effect5` int(32) NOT NULL, //Coupon5
  PRIMARY KEY (`PlayerID`),
  KEY `accountid` (`AccountID`),
  CONSTRAINT `accountid` FOREIGN KEY (`AccountID`) REFERENCES `accounts` (`AccountID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of players
-- ----------------------------
