using System;
using DesignIntentDesktop.Helpers;
using DesignIntentDesktop.Models.CloudStorage;
using DesignIntentDesktop.ViewModels.Base;

namespace DesignIntentDesktop.Components.DocumentDisplay
{
    public class DocumentDisplayViewModelItem : BaseViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string StockNumber { get; set; }

        public int Qty { get; set; }

        public double Length { get; set; }

        public BillItem ToDto()
        {
            var billItem = new BillItem();

            billItem.Map(this);

            return billItem;
        }

        public DocumentDisplayViewModelItem(string name, string partNumber, string description, string stockNumber, int qty, double length)
        {
            Id = Guid.NewGuid();
            Name = name;
            PartNumber = partNumber;
            Description = description;
            StockNumber = stockNumber;
            Qty = qty;
            Length = length;
        }
    }
}