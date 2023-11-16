using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;

namespace deadlineViewer.Parser.Notion
{
    internal class NotionParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName.Contains("notion-table-view-cell"));

            foreach ( var item in items )
            {
                 list.Add(item.TextContent);
            }

            return list.ToArray();
        }
    }
}
