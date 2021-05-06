using System;
using DesignIntentDesktop.Helpers;
using DesignIntentDesktop.ServiceProxies;
using DesignIntentDesktop.ViewModels.Base;
using InventorWrapper.BOM;
using InventorWrapper.IProps;

namespace DesignIntentDesktop.Components.DocumentDisplay
{
    public class BillItemViewModel : BaseViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string StockNumber { get; set; }

        public int Qty { get; set; }

        public double Length { get; set; }

        public BillItemDto ToDto()
        {
            var billItem = new BillItemDto();

            billItem.Map(this);

            return billItem;
        }

        public BillItemViewModel(string name, string partNumber, string description, string stockNumber, int qty, double length)
        {
            Id = Guid.NewGuid();
            Name = name;
            PartNumber = partNumber;
            Description = description;
            StockNumber = stockNumber;
            Qty = qty;
            Length = length;
        }

        public BillItemViewModel(InventorBomItem bomItem)
        {
            Id = Guid.NewGuid();
            Name = bomItem.Document.Name;
            PartNumber = bomItem.Document.Properties.GetPropertyValue(IpropertyEnum.PartNumber);
            Description = bomItem.Document.Properties.GetPropertyValue(IpropertyEnum.Description);
            StockNumber = bomItem.Document.Properties.GetPropertyValue(IpropertyEnum.StockNumber);
            Qty = bomItem.Qty;
            Length = ConvererHelpers.ConvertDouble(
                bomItem.Document.Properties.GetPropertyValue(IpropertyEnum.Custom, "Length"));

            if (string.IsNullOrEmpty(StockNumber))
            {
                StockNumber = "Enter Value";
            }
        }
    }
}