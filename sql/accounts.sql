/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-06-07 19:39:29
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `accounts`
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `AccountID` int(32) NOT NULL AUTO_INCREMENT,
  `Login` varchar(32) NOT NULL,
  `Password` varchar(32) NOT NULL,
  `Status` int(11) NOT NULL,
  `Referals` int(11) NOT NULL,
  `Mail` varchar(50) NOT NULL,
  `Hash` varchar(32) NOT NULL,
  PRIMARY KEY (`AccountID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of accounts
-- ----------------------------
