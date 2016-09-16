/*
Navicat MySQL Data Transfer

Source Server         : MySQL
Source Server Version : 50523
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50523
File Encoding         : 65001

Date: 2015-05-18 01:56:48
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `players_titles`
-- ----------------------------
DROP TABLE IF EXISTS `players_titles`;
CREATE TABLE `players_titles` (
  `PlayerID` int(11) NOT NULL AUTO_INCREMENT,
  `SlotCount` int(11) NOT NULL,
  `TitleEquiped1` int(11) NOT NULL,
  `TitleEquiped2` int(11) NOT NULL,
  `TitleEquiped3` int(11) NOT NULL,
  `titlePos1` int(11) NOT NULL,
  `titlePos2` int(11) NOT NULL,
  `titlePos3` int(11) NOT NULL,
  `titlePos4` int(11) NOT NULL,
  `titlePos5` int(11) NOT NULL,
  `titlePos6` int(11) NOT NULL,
  `title1` int(11) NOT NULL,
  `title2` int(11) NOT NULL,
  `title3` int(11) NOT NULL,
  `title4` int(11) NOT NULL,
  `title5` int(11) NOT NULL,
  `title6` int(11) NOT NULL,
  `title7` int(11) NOT NULL,
  `title8` int(11) NOT NULL,
  `title9` int(11) NOT NULL,
  `title10` int(11) NOT NULL,
  `title11` int(11) NOT NULL,
  `title12` int(11) NOT NULL,
  `title13` int(11) NOT NULL,
  `title14` int(11) NOT NULL,
  `title15` int(11) NOT NULL,
  `title16` int(11) NOT NULL,
  `title17` int(11) NOT NULL,
  `title18` int(11) NOT NULL,
  `title19` int(11) NOT NULL,
  `title20` int(11) NOT NULL,
  `title21` int(11) NOT NULL,
  `title22` int(11) NOT NULL,
  `title23` int(11) NOT NULL,
  `title24` int(11) NOT NULL,
  `title25` int(11) NOT NULL,
  `title26` int(11) NOT NULL,
  `title27` int(11) NOT NULL,
  `title28` int(11) NOT NULL,
  `title29` int(11) NOT NULL,
  `title30` int(11) NOT NULL,
  `title31` int(11) NOT NULL,
  `title32` int(11) NOT NULL,
  `title33` int(11) NOT NULL,
  `title34` int(11) NOT NULL,
  `title35` int(11) NOT NULL,
  `title36` int(11) NOT NULL,
  `title37` int(11) NOT NULL,
  `title38` int(11) NOT NULL,
  `title39` int(11) NOT NULL,
  `title40` int(11) NOT NULL,
  `title41` int(11) NOT NULL,
  `title42` int(11) NOT NULL,
  `title43` int(11) NOT NULL,
  `title44` int(11) NOT NULL,
  PRIMARY KEY (`PlayerID`),
  UNIQUE KEY `PlayerID` (`PlayerID`)
) ENGINE=MyISAM AUTO_INCREMENT=4457 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of players_titles
-- ----------------------------
INSERT INTO `players_titles` VALUES ('1', '3', '1', '2', '3', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1');
