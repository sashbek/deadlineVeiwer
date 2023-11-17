using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;

// this class gets a table from parsed by anglesharp html page
namespace deadlineViewer.Parser.Notion
{
    internal class NotionParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var all_tables_in_notion = new List<string>();
            // here are all parameters, that descrabe the table: type = div, class = notion-table-veiw-cell
            var tables = document.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName.Contains("notion-table-view-cell"));

            foreach ( var item in tables )
            {
                all_tables_in_notion.Add(item.TextContent);
            }

            return all_tables_in_notion.ToArray();
        }
    }
}
