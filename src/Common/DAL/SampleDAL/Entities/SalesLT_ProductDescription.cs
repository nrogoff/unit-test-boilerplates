// <auto-generated>
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDAL
{
    // ProductDescription
    public partial class SalesLT_ProductDescription
    {
        public int ProductDescriptionId { get; set; } // ProductDescriptionID (Primary key)
        public string Description { get; set; } // Description (length: 400)
        public Guid Rowguid { get; set; } // rowguid
        public DateTime ModifiedDate { get; set; } // ModifiedDate

        // Reverse navigation

        /// <summary>
        /// Child SalesLT_ProductModelProductDescriptions where [ProductModelProductDescription].[ProductDescriptionID] point to this entity (FK_ProductModelProductDescription_ProductDescription_ProductDescriptionID)
        /// </summary>
        public virtual ICollection<SalesLT_ProductModelProductDescription> SalesLT_ProductModelProductDescriptions { get; set; } // ProductModelProductDescription.FK_ProductModelProductDescription_ProductDescription_ProductDescriptionID

        public SalesLT_ProductDescription()
        {
            SalesLT_ProductModelProductDescriptions = new List<SalesLT_ProductModelProductDescription>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
