/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-06-07 19:39:40
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `items`
-- ----------------------------
DROP TABLE IF EXISTS `items`;
CREATE TABLE `items` (
  `ObjectID` int(11) NOT NULL AUTO_INCREMENT,
  `OwnerID` int(32) NOT NULL,
  `ItemID` int(32) NOT NULL,
  `Slot` int(32) NOT NULL,
  `Type` int(32) NOT NULL,
  `Count` int(32) NOT NULL,
  PRIMARY KEY (`ObjectID`),
  KEY `ownerid` (`OwnerID`),
  CONSTRAINT `ownerid` FOREIGN KEY (`OwnerID`) REFERENCES `accounts` (`AccountID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of items
-- ----------------------------
