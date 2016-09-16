/*
Navicat MySQL Data Transfer

Source Server         : MySQL
Source Server Version : 50523
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50523
File Encoding         : 65001

Date: 2015-04-14 21:58:25
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `players_messages`
-- ----------------------------
DROP TABLE IF EXISTS `players_messages`;
CREATE TABLE `players_messages` (
  `PlayerId` int(11) NOT NULL,
  `OwnerId` int(11) NOT NULL,
  `RecipientName` text NOT NULL,
  `Text` text NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of players_messages
-- ----------------------------
INSERT INTO `players_messages` VALUES ('1', '2', 'Admin', 'Tested message');
