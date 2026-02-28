# IronPDF Rowspan Fix & Custom Font Embedding (.NET)

## 📌 Overview

Generating PDFs from HTML is straightforward — until complex table layouts are involved.

One of the most common real-world issues developers face is **rowspan misalignment** when converting HTML tables into PDFs. While layouts may appear perfect in the browser, they often render incorrectly in PDF output.

This project demonstrates how to:

✅ Fix table rowspan misalignment in IronPDF-generated PDFs  
✅ Embed custom fonts for consistent branding  
✅ Generate PDFs from HTML using .NET  

It serves as a practical reference for developers building reporting systems, invoices, or structured documents using IronPDF.

---

## 🚨 Problem

HTML tables using `rowspan` frequently render incorrectly when converted to PDF:

- Misaligned columns  
- Broken borders  
- Inconsistent row heights  

This happens because PDF rendering requires fixed layouts, unlike the dynamic rendering used by browsers.

---

## 💡 Solution

This project introduces a **pre-render normalization approach** that:

- Detects rowspan usage
- Expands table structure
- Inserts layout-safe filler cells
- Preserves visual alignment

As a result, the final PDF renders correctly without broken table layouts.

---

## ✨ Additional Feature

### Custom Font Embedding

The project also demonstrates how to embed custom fonts into PDFs using IronPDF to ensure:

- Consistent typography
- Accurate layout spacing
- Brand-compliant documents

---

## 🛠️ Tech Stack

- .NET
- IronPDF
- HTML/CSS

---

## 📂 Project Structure

```
IronPdfMVC
├── Controllers
│ └── HomeController.cs
│
├── Enums
│ └── RowspanOutput.cs
│
├── Factories
│ └── InvoicePdfFactory.cs
│
├── Helpers
│ ├── EnumExtensions.cs
│ └── RowSpanExpander.cs // Core logic that fixes rowspan misalignment
│
├── Models
│ ├── ErrorViewModel.cs
│ ├── InvoicePdfViewModel.cs
│ └── PrintModel.cs
│
├── Views
│ ├── Home
│ │ ├── Index.cshtml
│ │ └── Privacy.cshtml
│ │
│ └── Shared
│ ├── _ViewImports.cshtml
│ └── _ViewStart.cshtml
│
├── wwwroot
│
├── appsettings.json
├── Program.cs
│
├── Raw_Rowspan.pdf // PDF generated without fix
└── Fixed_Rowspan.pdf // PDF generated after applying rowspan fix
```
---

## 🚀 How It Works

1. HTML is prepared for rendering  
2. Rowspan structures are normalized  
3. Custom fonts are embedded  
4. IronPDF generates the final PDF  

---

## 📸 Output

The generated PDF maintains:

✔ Correct table alignment  
✔ Proper borders  
✔ Embedded custom fonts  

---

## 📖 Use Cases

- Reports
- Invoices
- Data tables
- Branded documents

---

## 🏷️ Tags

`ironpdf` `dotnet` `html-to-pdf` `pdf-generation` `rowspan-fix` `custom-font`

---

## 📄 License

MIT


