// <auto-generated>
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDAL
{
    // Address
    public partial class SalesLT_Address
    {
        public int AddressId { get; set; } // AddressID (Primary key)
        public string AddressLine1 { get; set; } // AddressLine1 (length: 60)
        public string AddressLine2 { get; set; } // AddressLine2 (length: 60)
        public string City { get; set; } // City (length: 30)
        public string StateProvince { get; set; } // StateProvince (length: 50)
        public string CountryRegion { get; set; } // CountryRegion (length: 50)
        public string PostalCode { get; set; } // PostalCode (length: 15)
        public Guid Rowguid { get; set; } // rowguid
        public DateTime ModifiedDate { get; set; } // ModifiedDate

        // Reverse navigation

        /// <summary>
        /// Child SalesLT_CustomerAddresses where [CustomerAddress].[AddressID] point to this entity (FK_CustomerAddress_Address_AddressID)
        /// </summary>
        public ICollection<SalesLT_CustomerAddress> SalesLT_CustomerAddresses { get; set; } // CustomerAddress.FK_CustomerAddress_Address_AddressID

        /// <summary>
        /// Child SalesLT_SalesOrderHeaders where [SalesOrderHeader].[BillToAddressID] point to this entity (FK_SalesOrderHeader_Address_BillTo_AddressID)
        /// </summary>
        public ICollection<SalesLT_SalesOrderHeader> SalesLT_SalesOrderHeaders_BillToAddressId { get; set; } // SalesOrderHeader.FK_SalesOrderHeader_Address_BillTo_AddressID

        /// <summary>
        /// Child SalesLT_SalesOrderHeaders where [SalesOrderHeader].[ShipToAddressID] point to this entity (FK_SalesOrderHeader_Address_ShipTo_AddressID)
        /// </summary>
        public ICollection<SalesLT_SalesOrderHeader> SalesLT_SalesOrderHeaders_ShipToAddressId { get; set; } // SalesOrderHeader.FK_SalesOrderHeader_Address_ShipTo_AddressID

        public SalesLT_Address()
        {
            SalesLT_CustomerAddresses = new List<SalesLT_CustomerAddress>();
            SalesLT_SalesOrderHeaders_BillToAddressId = new List<SalesLT_SalesOrderHeader>();
            SalesLT_SalesOrderHeaders_ShipToAddressId = new List<SalesLT_SalesOrderHeader>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
