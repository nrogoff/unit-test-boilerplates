erDiagram
      
"SalesLT.Customer" {
    int CustomerID "PK"
          bit NameStyle ""
          nvarchar(8) Title ""
          nvarchar(50) FirstName ""
          nvarchar(50) MiddleName ""
          nvarchar(50) LastName ""
          nvarchar(10) Suffix ""
          nvarchar(128) CompanyName ""
          nvarchar(256) SalesPerson ""
          nvarchar(50) EmailAddress ""
          nvarchar(25) Phone ""
          varchar(128) PasswordHash ""
          varchar(10) PasswordSalt ""
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"SalesLT.ProductModel" {
    int ProductModelID "PK"
          nvarchar(50) Name ""
          xml(-1) CatalogDescription ""
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"SalesLT.ProductDescription" {
    int ProductDescriptionID "PK"
          nvarchar(400) Description ""
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"SalesLT.Product" {
    int ProductID "PK"
          nvarchar(50) Name ""
          nvarchar(25) ProductNumber ""
          nvarchar(15) Color ""
          money StandardCost ""
          money ListPrice ""
          nvarchar(5) Size ""
          decimal Weight ""
          int ProductCategoryID "FK"
          int ProductModelID "FK"
          datetime SellStartDate ""
          datetime SellEndDate ""
          datetime DiscontinuedDate ""
          varbinary(-1) ThumbNailPhoto ""
          nvarchar(50) ThumbnailPhotoFileName ""
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"SalesLT.ProductModelProductDescription" {
    int ProductModelID "FK, PK"
          int ProductDescriptionID "FK, PK"
          nchar(6) Culture "PK"
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"SalesLT.ProductCategory" {
    int ProductCategoryID "PK"
          int ParentProductCategoryID "FK"
          nvarchar(50) Name ""
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"dbo.BuildVersion" {
    tinyint SystemInformationID "PK"
          nvarchar(25) Database-Version ""
          datetime VersionDate ""
          datetime ModifiedDate ""
          
}
"dbo.ErrorLog" {
    int ErrorLogID "PK"
          datetime ErrorTime ""
          nvarchar(128) UserName ""
          int ErrorNumber ""
          int ErrorSeverity ""
          int ErrorState ""
          nvarchar(126) ErrorProcedure ""
          int ErrorLine ""
          nvarchar(4000) ErrorMessage ""
          
}
"SalesLT.Address" {
    int AddressID "PK"
          nvarchar(60) AddressLine1 ""
          nvarchar(60) AddressLine2 ""
          nvarchar(30) City ""
          nvarchar(50) StateProvince ""
          nvarchar(50) CountryRegion ""
          nvarchar(15) PostalCode ""
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"SalesLT.CustomerAddress" {
    int CustomerID "FK, PK"
          int AddressID "FK, PK"
          nvarchar(50) AddressType ""
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"SalesLT.SalesOrderDetail" {
    int SalesOrderID "FK, PK"
          int SalesOrderDetailID "PK"
          smallint OrderQty ""
          int ProductID "FK"
          money UnitPrice ""
          money UnitPriceDiscount ""
          numeric LineTotal ""
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"SalesLT.SalesOrderHeader" {
    int SalesOrderID "PK"
          tinyint RevisionNumber ""
          datetime OrderDate ""
          datetime DueDate ""
          datetime ShipDate ""
          tinyint Status ""
          bit OnlineOrderFlag ""
          nvarchar(25) SalesOrderNumber ""
          nvarchar(25) PurchaseOrderNumber ""
          nvarchar(15) AccountNumber ""
          int CustomerID "FK"
          int ShipToAddressID "FK"
          int BillToAddressID "FK"
          nvarchar(50) ShipMethod ""
          varchar(15) CreditCardApprovalCode ""
          money SubTotal ""
          money TaxAmt ""
          money Freight ""
          money TotalDue ""
          nvarchar(-1) Comment ""
          uniqueidentifier rowguid ""
          datetime ModifiedDate ""
          
}
"View: SalesLT.vProductModelCatalogDescription" {
    SalesLT_ProductModel ProductModelID ""
          SalesLT_ProductModel Name ""
          SalesLT_ProductModel CatalogDescription ""
          SalesLT_ProductModel rowguid ""
          SalesLT_ProductModel ModifiedDate ""
          
}
"View: SalesLT.vProductAndDescription" {
    SalesLT_ProductModel ProductModelID ""
          SalesLT_ProductModel Name ""
          SalesLT_ProductDescription ProductDescriptionID ""
          SalesLT_ProductDescription Description ""
          SalesLT_Product ProductID ""
          SalesLT_ProductModelProductDescription Culture ""
          
}
"View: SalesLT.vGetAllCategories" {
    SalesLT_ProductCategory ProductCategoryID ""
          SalesLT_ProductCategory ParentProductCategoryID ""
          SalesLT_ProductCategory Name ""
          
}
      "SalesLT.Product" |o--|{ "SalesLT.ProductCategory": "ProductCategoryID"
"SalesLT.Product" |o--|{ "SalesLT.ProductModel": "ProductModelID"
"SalesLT.ProductModelProductDescription" ||--|{ "SalesLT.ProductModel": "ProductModelID"
"SalesLT.ProductModelProductDescription" ||--|{ "SalesLT.ProductDescription": "ProductDescriptionID"
"SalesLT.ProductCategory" |o--|{ "SalesLT.ProductCategory": "ProductCategoryID"
"SalesLT.CustomerAddress" ||--|{ "SalesLT.Customer": "CustomerID"
"SalesLT.CustomerAddress" ||--|{ "SalesLT.Address": "AddressID"
"SalesLT.SalesOrderDetail" ||--|{ "SalesLT.SalesOrderHeader": "SalesOrderID"
"SalesLT.SalesOrderDetail" ||--|{ "SalesLT.Product": "ProductID"
"SalesLT.SalesOrderHeader" ||--|{ "SalesLT.Customer": "CustomerID"
"SalesLT.SalesOrderHeader" |o--|{ "SalesLT.Address": "AddressID"
"SalesLT.SalesOrderHeader" |o--|{ "SalesLT.Address": "AddressID"
"View: SalesLT.vProductModelCatalogDescription" ||--|| "SalesLT.ProductModel": "vProductModelCatalogDescription => ProductModel"
"View: SalesLT.vProductModelCatalogDescription" ||--|| "SalesLT.ProductModel": "vProductModelCatalogDescription => ProductModel"
"View: SalesLT.vProductModelCatalogDescription" ||--|| "SalesLT.ProductModel": "vProductModelCatalogDescription => ProductModel"
"View: SalesLT.vProductModelCatalogDescription" ||--|| "SalesLT.ProductModel": "vProductModelCatalogDescription => ProductModel"
"View: SalesLT.vProductModelCatalogDescription" ||--|| "SalesLT.ProductModel": "vProductModelCatalogDescription => ProductModel"
"View: SalesLT.vProductAndDescription" ||--|| "SalesLT.ProductModel": "vProductAndDescription => ProductModel"
"View: SalesLT.vProductAndDescription" ||--|| "SalesLT.ProductModel": "vProductAndDescription => ProductModel"
"View: SalesLT.vProductAndDescription" ||--|| "SalesLT.ProductDescription": "vProductAndDescription => ProductDescription"
"View: SalesLT.vProductAndDescription" ||--|| "SalesLT.ProductDescription": "vProductAndDescription => ProductDescription"
"View: SalesLT.vProductAndDescription" ||--|| "SalesLT.Product": "vProductAndDescription => Product"
"View: SalesLT.vProductAndDescription" ||--|| "SalesLT.ProductModelProductDescription": "vProductAndDescription => ProductModelProductDescription"
"View: SalesLT.vGetAllCategories" ||--|| "SalesLT.ProductCategory": "vGetAllCategories => ProductCategory"
"View: SalesLT.vGetAllCategories" ||--|| "SalesLT.ProductCategory": "vGetAllCategories => ProductCategory"
"View: SalesLT.vGetAllCategories" ||--|| "SalesLT.ProductCategory": "vGetAllCategories => ProductCategory"
