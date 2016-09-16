/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-06-07 19:40:47
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `players_stats`
-- ----------------------------
DROP TABLE IF EXISTS `players_stats`;
CREATE TABLE `players_stats` (
  `PlayerID` int(11) NOT NULL,
  `Fights` int(11) NOT NULL,
  `Wins` int(11) NOT NULL,
  `Losts` int(11) NOT NULL,
  `Kills` int(11) NOT NULL,
  `Headshots` int(11) NOT NULL,
  `Deaths` int(11) NOT NULL,
  `Escapes` int(11) NOT NULL,
  `SeasonFights` int(11) NOT NULL,
  `SeasonWins` int(11) NOT NULL,
  `SeasonLosts` int(11) NOT NULL,
  `SeasonKills` int(11) NOT NULL,
  `SeasonHeadshots` int(11) NOT NULL,
  `SeasonDeaths` int(11) NOT NULL,
  `SeasonEscapes` int(11) NOT NULL,
  UNIQUE KEY `PlayerID` (`PlayerID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of players_stats
-- ----------------------------
