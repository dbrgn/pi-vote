SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci ;
CREATE SCHEMA IF NOT EXISTS `pivote` DEFAULT CHARACTER SET latin1 ;

-- -----------------------------------------------------
-- Table `pivote`.`authority`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`authority` (
  `VotingId` VARBINARY(16) NOT NULL ,
  `AuthorityIndex` INT(11) NOT NULL ,
  `AuthorityId` VARBINARY(16) NOT NULL ,
  `Certificate` BLOB NOT NULL ,
  PRIMARY KEY (`VotingId`, `AuthorityIndex`) ,
  UNIQUE INDEX `Authoirtyid` (`VotingId` ASC, `AuthorityId` ASC) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`certificate`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`certificate` (
  `Id` VARBINARY(16) NOT NULL ,
  `Value` BLOB NOT NULL ,
  `Root` INT(11) NOT NULL ,
  PRIMARY KEY (`Id`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`deciphers`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`deciphers` (
  `VotingId` VARBINARY(16) NOT NULL ,
  `AuthorityIndex` INT(11) NOT NULL ,
  `Value` BLOB NOT NULL ,
  PRIMARY KEY (`VotingId`, `AuthorityIndex`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`envelope`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`envelope` (
  `VotingId` VARBINARY(16) NOT NULL ,
  `EnvelopeIndex` INT(11) NOT NULL ,
  `VoterId` VARBINARY(16) NOT NULL ,
  `Value` LONGBLOB NOT NULL ,
  PRIMARY KEY (`VotingId`, `EnvelopeIndex`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`revocationlist`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`revocationlist` (
  `IssuerId` VARBINARY(16) NOT NULL,
  `ValidFrom` DATETIME NOT NULL ,
  `ValidUntil` DATETIME NOT NULL ,
  `Value` BLOB NULL DEFAULT NULL ,
  PRIMARY KEY (`IssuerId`, `ValidFrom`, `ValidUntil`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`sharepart`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`sharepart` (
  `VotingId` VARBINARY(16) NOT NULL ,
  `AuthorityIndex` INT(11) NOT NULL ,
  `Value` BLOB NOT NULL ,
  PRIMARY KEY (`VotingId`, `AuthorityIndex`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`shareresponse`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`shareresponse` (
  `VotingId` VARBINARY(16) NOT NULL ,
  `AuthorityIndex` INT(11) NOT NULL ,
  `Value` BLOB NOT NULL ,
  PRIMARY KEY (`VotingId`, `AuthorityIndex`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`signaturerequest`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`signaturerequest` (
  `Id` VARBINARY(16) NOT NULL ,
  `Value` BLOB NOT NULL ,
  `Info` BLOB NOT NULL ,
  PRIMARY KEY (`Id`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`signatureresponse`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`signatureresponse` (
  `Id` VARBINARY(16) NOT NULL ,
  `Value` BLOB NOT NULL ,
  PRIMARY KEY (`Id`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`voting`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`voting` (
  `Id` VARBINARY(16) NOT NULL ,
  `Parameters` BLOB NOT NULL ,
  `Status` INT(11) NOT NULL ,
  PRIMARY KEY (`Id`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`votinggroup`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`votinggroup` (
  `Id` INT(11) NOT NULL ,
  `NameEnglish` VARCHAR(100) NOT NULL ,
  `NameGerman` VARCHAR(100) NOT NULL ,
  `NameFrench` VARCHAR(100) NOT NULL ,
  `NameItalien` VARCHAR(100) NOT NULL ,
  PRIMARY KEY (`Id`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`signcheck`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`signcheck` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `CertificateId` VARBINARY(16) NOT NULL ,
  `Value` LONGBLOB NOT NULL ,
  PRIMARY KEY (`Id`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `pivote`.`signcheck`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `pivote`.`signcheckcookie` (
  `NotaryId` VARBINARY(16) NOT NULL ,
  `Cookie` LONGBLOB NOT NULL ,
  `Code` VARBINARY(64) NOT NULL ,
  `Expires` DATETIME NOT NULL ,
  PRIMARY KEY (`NotaryId`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
