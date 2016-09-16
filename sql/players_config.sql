/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-06-07 19:40:24
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `players_config`
-- ----------------------------
DROP TABLE IF EXISTS `players_config`;
CREATE TABLE `players_config` (
  `PlayerID` int(11) NOT NULL,
  `Config` int(11) NOT NULL,
  `Blood` int(11) NOT NULL,
  `Visibility` int(11) NOT NULL,
  `Mao` int(11) NOT NULL,
  `Audio1` int(11) NOT NULL,
  `Audio2` int(11) NOT NULL,
  `AudioEnable` int(11) NOT NULL,
  `MouseSensitivity` int(11) NOT NULL,
  `Vision` int(11) NOT NULL,
  UNIQUE KEY `PlayerID` (`PlayerID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of players_config
-- ----------------------------
