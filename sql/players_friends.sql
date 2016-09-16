/*
Navicat MySQL Data Transfer

Source Server         : MySQL
Source Server Version : 50523
Source Host           : localhost:3306
Source Database       : pb

Target Server Type    : MYSQL
Target Server Version : 50523
File Encoding         : 65001

Date: 2015-04-14 21:58:20
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `players_friends`
-- ----------------------------
DROP TABLE IF EXISTS `players_friends`;
CREATE TABLE `players_friends` (
  `OwnerID` int(32) NOT NULL,
  `FriendID` int(32) NOT NULL,
  `Status` int(32) NOT NULL,
  KEY `owner` (`OwnerID`),
  KEY `friend` (`FriendID`),
  CONSTRAINT `friend` FOREIGN KEY (`FriendID`) REFERENCES `players` (`PlayerID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `owner` FOREIGN KEY (`OwnerID`) REFERENCES `players` (`PlayerID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of players_friends
-- ----------------------------
INSERT INTO `players_friends` VALUES ('1', '2', '2');
