/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-06-07 19:40:29
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `players_equip`
-- ----------------------------
DROP TABLE IF EXISTS `players_equip`;
CREATE TABLE `players_equip` (
  `PlayerID` int(11) NOT NULL,
  `WeaponPrimary` int(11) NOT NULL,
  `WeaponSecondary` int(11) NOT NULL,
  `WeaponMelee` int(11) NOT NULL,
  `WeaponThrownNormal` int(11) NOT NULL,
  `WeaponThrownSpecial` int(11) NOT NULL,
  `CharRed` int(10) NOT NULL,
  `CharBlue` int(10) NOT NULL,
  `CharHelmet` int(10) NOT NULL,
  `CharDino` int(10) NOT NULL,
  `CharBeret` int(10) NOT NULL,
  UNIQUE KEY `PlayerID` (`PlayerID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of players_equip
-- ----------------------------
