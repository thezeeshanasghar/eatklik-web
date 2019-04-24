  IF EXISTS(SELECT 1 FROM information_schema.tables 
  WHERE table_name = '__EFMigrationsHistory' AND table_schema = DATABASE()) 
BEGIN
CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

END;

CREATE TABLE `Cities` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `ImagePath` text NULL,
    `Status` int NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Cuisines` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `Description` text NULL,
    `ImagePath` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Menus` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `RestaurantContacts` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Email` text NULL,
    `MobileNo` text NULL,
    `URL` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Settings` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `Value` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Customers` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `Email` text NULL,
    `Password` text NULL,
    `ImagePath` text NULL,
    `Address` text NULL,
    `Status` int NOT NULL,
    `CityId` bigint NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Customers_Cities_CityId` FOREIGN KEY (`CityId`) REFERENCES `Cities` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Promotions` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `Content` text NULL,
    `PromoType` int NOT NULL,
    `CityId` bigint NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Promotions_Cities_CityId` FOREIGN KEY (`CityId`) REFERENCES `Cities` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `RestaurantLocations` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Address` text NULL,
    `latitude` bigint NOT NULL,
    `longitude` bigint NOT NULL,
    `CityId` bigint NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RestaurantLocations_Cities_CityId` FOREIGN KEY (`CityId`) REFERENCES `Cities` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Riders` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `MobileNo` text NULL,
    `Password` text NULL,
    `Status` int NOT NULL,
    `CityId` bigint NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Riders_Cities_CityId` FOREIGN KEY (`CityId`) REFERENCES `Cities` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `MenuItems` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `ImagePath` text NULL,
    `Size` int NOT NULL,
    `Price` bigint NOT NULL,
    `Status` int NOT NULL,
    `MenuId` bigint NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_MenuItems_Menus_MenuId` FOREIGN KEY (`MenuId`) REFERENCES `Menus` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Restaurants` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` text NULL,
    `MinOrderPrice` bigint NOT NULL,
    `MaxOrderPrice` bigint NOT NULL,
    `Description` text NULL,
    `LogoImagePath` text NULL,
    `CoverImagePath` text NULL,
    `RestaurantLocationId` bigint NOT NULL,
    `RestaurantContactId` bigint NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Restaurants_RestaurantContacts_RestaurantContactId` FOREIGN KEY (`RestaurantContactId`) REFERENCES `RestaurantContacts` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Restaurants_RestaurantLocations_RestaurantLocationId` FOREIGN KEY (`RestaurantLocationId`) REFERENCES `RestaurantLocations` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `RestaurantCuisine` (
    `RestaurantId` bigint NOT NULL,
    `CuisineId` bigint NOT NULL,
    PRIMARY KEY (`RestaurantId`, `CuisineId`),
    CONSTRAINT `FK_RestaurantCuisine_Cuisines_CuisineId` FOREIGN KEY (`CuisineId`) REFERENCES `Cuisines` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_RestaurantCuisine_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `RestaurantTimings` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `WeekDay` int NOT NULL,
    `StartTime` text NULL,
    `EndTime` text NULL,
    `RestaurantId` bigint NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RestaurantTimings_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Reviews` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Rating` decimal(18, 2) NOT NULL,
    `Comment` text NULL,
    `Status` int NOT NULL,
    `CustomerId` bigint NOT NULL,
    `RestaurantId` bigint NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Reviews_Customers_CustomerId` FOREIGN KEY (`CustomerId`) REFERENCES `Customers` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Reviews_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Customers_CityId` ON `Customers` (`CityId`);

CREATE INDEX `IX_MenuItems_MenuId` ON `MenuItems` (`MenuId`);

CREATE INDEX `IX_Promotions_CityId` ON `Promotions` (`CityId`);

CREATE INDEX `IX_RestaurantCuisine_CuisineId` ON `RestaurantCuisine` (`CuisineId`);

CREATE INDEX `IX_RestaurantLocations_CityId` ON `RestaurantLocations` (`CityId`);

CREATE INDEX `IX_Restaurants_RestaurantContactId` ON `Restaurants` (`RestaurantContactId`);

CREATE INDEX `IX_Restaurants_RestaurantLocationId` ON `Restaurants` (`RestaurantLocationId`);

CREATE INDEX `IX_RestaurantTimings_RestaurantId` ON `RestaurantTimings` (`RestaurantId`);

CREATE INDEX `IX_Reviews_CustomerId` ON `Reviews` (`CustomerId`);

CREATE INDEX `IX_Reviews_RestaurantId` ON `Reviews` (`RestaurantId`);

CREATE INDEX `IX_Riders_CityId` ON `Riders` (`CityId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190423131435_init89', '2.2.4-servicing-10062');

ALTER TABLE `Menus` ADD `ImagePath` text NULL;

ALTER TABLE `Menus` ADD `RestaurantId` bigint NOT NULL DEFAULT 0;

ALTER TABLE `Menus` ADD `Status` int NOT NULL DEFAULT 0;

CREATE INDEX `IX_Menus_RestaurantId` ON `Menus` (`RestaurantId`);

ALTER TABLE `Menus` ADD CONSTRAINT `FK_Menus_Restaurants_RestaurantId` FOREIGN KEY (`RestaurantId`) REFERENCES `Restaurants` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190423131527_init890', '2.2.4-servicing-10062');

