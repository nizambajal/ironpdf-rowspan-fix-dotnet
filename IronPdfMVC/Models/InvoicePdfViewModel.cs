namespace IronPdfMVC.Models
{
    public class InvoicePdfViewModel
    {
        public CompanyVm Company { get; set; }
        public CustomerVm Customer { get; set; }
        public InvoiceVm Invoice { get; set; }
        public List<ItemVm> Items { get; set; }
        public SummaryVm Summary { get; set; }
    }

    public class CompanyVm
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Address { get; set; }
    }

    public class CustomerVm
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class InvoiceVm
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public string Currency { get; set; }
    }

    public class ItemVm
    {
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }

    public class SummaryVm
    {
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
