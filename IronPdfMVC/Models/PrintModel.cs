using IronPdfMVC.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IronPdfMVC.Models
{
    public class PrintModel
    {
        public RowspanOutput RowspanOutput { get; set; }
        public IList<SelectListItem> RowspanOutputOptions { get; set; } = new List<SelectListItem>();
        public bool UseCustomFont { get; set; }
        public string SelectedFont { get; set; } = IronSoftware.Drawing.FontTypes.Arial.Name;

        public List<string> CustomFonts { get; set; } = new List<string>
        {
            "ToThePointRegular",
            "SplendidB",
            "SplendidN"
        };
    }
}
