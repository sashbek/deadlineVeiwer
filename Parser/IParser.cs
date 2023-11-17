using AngleSharp.Html.Dom;

namespace deadlineViewer.Parser
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
