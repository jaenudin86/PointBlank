/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-06-07 19:40:37
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `players_quests`
-- ----------------------------
DROP TABLE IF EXISTS `players_quests`;
CREATE TABLE `players_quests` (
  `PlayerID` int(32) NOT NULL,
  `MissionID` int(32) NOT NULL,
  `CardID` int(32) NOT NULL,
  `Card1_1` int(32) NOT NULL,
  `Card1_2` int(32) NOT NULL,
  `Card1_3` int(32) NOT NULL,
  `Card1_4` int(32) NOT NULL,
  `Card2_1` int(32) NOT NULL,
  `Card2_2` int(32) NOT NULL,
  `Card2_3` int(32) NOT NULL,
  `Card2_4` int(32) NOT NULL,
  `Card3_1` int(32) NOT NULL,
  `Card3_2` int(32) NOT NULL,
  `Card3_3` int(32) NOT NULL,
  `Card3_4` int(32) NOT NULL,
  `Card4_1` int(32) NOT NULL,
  `Card4_2` int(32) NOT NULL,
  `Card4_3` int(32) NOT NULL,
  `Card4_4` int(32) NOT NULL,
  `Card5_1` int(32) NOT NULL,
  `Card5_2` int(32) NOT NULL,
  `Card5_3` int(32) NOT NULL,
  `Card5_4` int(32) NOT NULL,
  `Card6_1` int(32) NOT NULL,
  `Card6_2` int(32) NOT NULL,
  `Card6_3` int(32) NOT NULL,
  `Card6_4` int(32) NOT NULL,
  `Card7_1` int(32) NOT NULL,
  `Card7_2` int(32) NOT NULL,
  `Card7_3` int(32) NOT NULL,
  `Card7_4` int(32) NOT NULL,
  `Card8_1` int(32) NOT NULL,
  `Card8_2` int(32) NOT NULL,
  `Card8_3` int(32) NOT NULL,
  `Card8_4` int(32) NOT NULL,
  `Card9_1` int(32) NOT NULL,
  `Card9_2` int(32) NOT NULL,
  `Card9_3` int(32) NOT NULL,
  `Card9_4` int(32) NOT NULL,
  `Card10_1` int(32) NOT NULL,
  `Card10_2` int(32) NOT NULL,
  `Card10_3` int(32) NOT NULL,
  `Card10_4` int(32) NOT NULL,
  `LastRewardEXP` int(32) NOT NULL,
  `LastRewardCredits` int(32) NOT NULL,
  `RepeatCount` int(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of players_quests
-- ----------------------------
