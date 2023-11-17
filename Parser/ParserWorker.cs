using AngleSharp.Html.Parser;
using System;

namespace deadlineViewer.Parser
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings settings;
        HtmlLoader loader;

        #region Properties
        public IParser<T> Parser
        {
            get 
            { 
                return parser; 
            }
            set
            {
                parser = value;
            }
        }
        public IParserSettings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = value; loader = new HtmlLoader(value);
            }
        }

        #endregion

        public event Action<object, T> OnNewData;
        public void Start()
        {
            Worker();
        }

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings settings) : this(parser)
        {
            this.settings = settings;
        }

        private async void Worker()
        {
            // get html page source from notion using chrome emulator
            var source = await loader.GetSource();
            // parsing this page with anglesharp parser 
            var domParser = new HtmlParser();
            var document = await domParser.ParseDocumentAsync(source);
            // parsing parsed doc with sp parser to get table
            var result = parser.Parse(document);

            //run action
            OnNewData?.Invoke(this, result);
        }
    }
}
