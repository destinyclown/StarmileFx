ALTER TABLE `Product` ADD INDEX index_CnName ( `CnName` );
ALTER TABLE `Product` ADD INDEX index_EnName ( `EnName` );
ALTER TABLE `OnLineOrderParent` ADD INDEX index_BatchProcessingID ( `BatchProcessingID` );
ALTER TABLE `OnLineOrderDetail` ADD INDEX index_OrderID ( `OrderID` );
ALTER TABLE `OnLineOrderDetail` ADD INDEX index_ProductID ( `ProductID` );
ALTER TABLE `OffLineOrderDetail` ADD INDEX index_OrderID ( `OrderID` );
ALTER TABLE `OffLineOrderDetail` ADD INDEX index_ProductID ( `ProductID` );
ALTER TABLE `CustomerComment` ADD INDEX index_OrderID ( `OrderID` );
ALTER TABLE `CustomerComment` ADD INDEX index_ProductID ( `ProductID` );
ALTER TABLE `CustomerSign` ADD INDEX index_Mode ( `Mode` );
ALTER TABLE `Resources` ADD INDEX index_ProductID_Type ( `ProductID`,`Type` );
ALTER TABLE `Resources` ADD INDEX index_ResourcesCode_Type ( `ResourcesCode`,`Type` );