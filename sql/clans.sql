/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-06-07 19:39:35
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `clans`
-- ----------------------------
DROP TABLE IF EXISTS `clans`;
CREATE TABLE `clans` (
  `ClanID` int(32) NOT NULL,
  `OwnerID` int(32) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Rank` int(32) NOT NULL,
  `Exp` int(32) NOT NULL,
  `Logo1` int(32) NOT NULL,
  `Logo2` int(32) NOT NULL,
  `Logo3` int(32) NOT NULL,
  `Logo4` int(32) NOT NULL,
  `Color` int(32) NOT NULL,
  `MaxPlayersCount` int(32) NOT NULL,
  `PlayersCount` int(32) NOT NULL,
  `Info` varchar(120) NOT NULL,
  `Notice` varchar(120) NOT NULL,
  KEY `clanownerid` (`OwnerID`),
  CONSTRAINT `clanownerid` FOREIGN KEY (`OwnerID`) REFERENCES `players` (`PlayerID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of clans
-- ----------------------------
