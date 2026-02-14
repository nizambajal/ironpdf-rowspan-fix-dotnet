using IronPdfMVC.Enums;
using IronPdfMVC.Factories;
using IronPdfMVC.Helpers;
using IronPdfMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IronPdfMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new PrintModel
            {
                RowspanOutput = RowspanOutput.Raw,
                RowspanOutputOptions = typeof(RowspanOutput).ToSelectList(RowspanOutput.Raw),
                SelectedFont = "ToThePointRegular",
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult PrintPdf(PrintModel model)
        {
            var viewModel = InvoicePdfFactory.GetInvoicePdfViewModel();

            #region Font embedding example
            // inject font as base64 and @font-face before rendering
            var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fonts", $"{model.SelectedFont}.ttf");
            var fontBytes = System.IO.File.ReadAllBytes(fontPath);
            var fontBase64 = Convert.ToBase64String(fontBytes);

            var fontCss = $@"
                  @font-face {{
                    font-family: 'ToThePoint';
                    src: url('data:font/ttf;base64,{fontBase64}') format('truetype');
                    font-weight: normal;
                    font-style: normal;
                    font-display: swap;
                  }}
                  body, table, td, th {{ font-family: 'ToThePoint', sans-serif; }}
            ";

            var normalCss = @"table, th, td {                            
                                border: 1px solid black;
                                border-collapse: collapse;
                                padding: 8px;
                            }";
            #endregion

            // HTML table with rowspan
            string htmlContent = $@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Document</title>

                    <style>";
            htmlContent += normalCss;

            if (model.UseCustomFont)
            {
                htmlContent += fontCss;
            }

            htmlContent += @"</style>
                </head>
                <body>
                <h2>Table with Rowspan</h2>
                <table>
                    <tr>
                        <th></th>
                        <th>Rows</th>
                        <th>Description</th>
                    </tr>
                    <tr>
                        <td rowspan=""100"">Row span 100</td>";

            htmlContent = InvoicePdfFactory.PrepareRowspanTableContent(htmlContent);

            htmlContent += @"
                    </table>
                </body>
                </html>";

            // Create PDF from HTML
            var renderer = new ChromePdfRenderer();
            PdfDocument pdf;

            switch (model.RowspanOutput)
            {
                case RowspanOutput.Fixed:
                    var htmlContentExpandedHtml = RowspanExpander.ExpandRowspans(htmlContent);
                    pdf = renderer.RenderHtmlAsPdf(htmlContentExpandedHtml);
                    pdf.SaveAs("Fixed_Rowspan.pdf");
                    break;
                case RowspanOutput.Raw:
                    pdf = renderer.RenderHtmlAsPdf(htmlContent);
                    pdf.SaveAs("Raw_Rowspan.pdf");
                    break;
                default:
                    pdf = renderer.RenderHtmlAsPdf(htmlContent);
                    break;
            }

            // Return PDF as a file result
            var pdfBytes = pdf.BinaryData;

            return File(pdfBytes, "application/pdf");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
