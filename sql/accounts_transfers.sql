SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `accounts_transfers`
-- ----------------------------
DROP TABLE IF EXISTS `accounts_transfers`;
CREATE TABLE `accounts_transfers` (
  `TransferID` int(32) NOT NULL AUTO_INCREMENT,
  `AccountID` int(11) NOT NULL,
  `TransferMoney` int(32) NOT NULL,
  `TransferStatus` int(32) NOT NULL,
  `Hash` varchar(250) NOT NULL,
  UNIQUE KEY `TransferID` (`TransferID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;