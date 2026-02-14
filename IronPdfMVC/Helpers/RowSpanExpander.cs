using HtmlAgilityPack;

namespace IronPdfMVC.Helpers
{
    public static class RowspanExpander
    {
        private const string STYLE_ATTRIBUTE = "style";

        public static string ExpandRowspans(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var tables = doc.DocumentNode.SelectNodes("//table");
            if (tables == null)
                return html;

            foreach (var table in tables)
            {
                ExpandTable(table);
            }

            return doc.DocumentNode.OuterHtml;
        }

        private static void ExpandTable(HtmlNode table)
        {
            var rows = table.SelectNodes(".//tr");
            if (rows == null)
                return;

            // Tracks active rowspans per column index
            var activeRowspans = new Dictionary<int, Queue<HtmlNode>>();

            foreach (var row in rows)
            {
                var cells = row.Elements("td")
                               .Concat(row.Elements("th"))
                               .ToList();

                int colIndex = 0;

                // Inject pending rowspan cells
                while (activeRowspans.ContainsKey(colIndex))
                {
                    var queue = activeRowspans[colIndex];
                    if (queue.Count == 0)
                    {
                        activeRowspans.Remove(colIndex);
                        break;
                    }

                    var clonedCell = queue.Dequeue();
                    //row.PrependChild(clonedCell.Clone());
                    InsertCellAtColumn(row, clonedCell.Clone(), colIndex);
                    colIndex++;
                }

                foreach (var cell in cells)
                {
                    while (activeRowspans.ContainsKey(colIndex))
                    {
                        colIndex++;
                    }

                    if (cell.Attributes["rowspan"] != null &&
                        int.TryParse(cell.Attributes["rowspan"].Value, out int span) &&
                        span > 1)
                    {
                        // Remove bottom border from original rowspan cell
                        AppendInlineStyle(cell, "border-bottom: none;");

                        var clones = new Queue<HtmlNode>();

                        for (int i = 1; i < span; i++)
                        {
                            var clone = cell.Clone();
                            clone.Attributes.Remove("rowspan");
                            clone.InnerHtml = string.Empty;

                            //clone.AddClass("rowspan-filler");
                            AppendInlineStyle(clone, "border-top: none;border-bottom: none;padding-top: 0;padding-bottom: 0;");

                            clones.Enqueue(clone);
                        }

                        activeRowspans[colIndex] = clones;
                        cell.Attributes.Remove("rowspan");
                    }

                    colIndex++;
                }
            }
        }

        private static void AppendInlineStyle(HtmlNode node, string style)
        {
            var existing = node.GetAttributeValue(STYLE_ATTRIBUTE, string.Empty);

            if (!string.IsNullOrWhiteSpace(existing) && !existing.Trim().EndsWith(";"))
            {
                existing += ";";
            }

            node.SetAttributeValue(STYLE_ATTRIBUTE, existing + style);
        }

        private static void InsertCellAtColumn(HtmlNode row, HtmlNode cell, int colIndex)
        {
            var existingCells = row.Elements("td")
                                   .Concat(row.Elements("th"))
                                   .ToList();

            if (colIndex >= existingCells.Count)
            {
                row.AppendChild(cell);
            }
            else
            {
                row.InsertBefore(cell, existingCells[colIndex]);
            }
        }
    }
}
