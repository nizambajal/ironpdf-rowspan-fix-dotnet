using IronPdfMVC.Models;

namespace IronPdfMVC.Factories
{
    public class InvoicePdfFactory
    {
        public static InvoicePdfViewModel GetInvoicePdfViewModel()
        {
            var items = new List<ItemVm>
            {
                new ItemVm
                {
                    Description = "Website Design",
                    Qty = 1,
                    UnitPrice = 1200m,
                    Total = 1200m
                },
                new ItemVm
                {
                    Description = "Hosting (12 months)",
                    Qty = 1,
                    UnitPrice = 240m,
                    Total = 240m
                }
            };

            return new InvoicePdfViewModel
            {
                Company = new CompanyVm
                {
                    Name = "Acme Corp",
                    LogoUrl = "https://example.com/logo.png",
                    Address = "123 Business St, NY"
                },
                Customer = new CustomerVm
                {
                    Name = "John Doe",
                    Email = "john@email.com",
                    Address = "45 Main Street"
                },
                Invoice = new InvoiceVm
                {
                    Number = "INV-2026-001",
                    Date = "2026-01-31",
                    DueDate = "2026-02-15",
                    Currency = "$"
                },
                Items = items,
                Summary = new SummaryVm
                {
                    Subtotal = 1440m,
                    Tax = 144m,
                    GrandTotal = 1584m
                }
            };
        }

        public static string PrepareRowspanTableContent(string htmlContent)
        {
            for (int i = 1; i <= 100; i++)
            {
                if (i == 1)
                {
                    htmlContent += $@"
                            <td>{i}</td>
                            <td>Row {i}</td>
                        </tr>";
                }
                else
                {
                    htmlContent += $@"
                        <tr>
                            <td>{i}</td>
                            <td>Row {i}</td>
                        </tr>";
                }
            }

            return htmlContent;
        }
    }
}
