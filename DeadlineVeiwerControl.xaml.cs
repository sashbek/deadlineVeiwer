using Microsoft.VisualStudio.PlatformUI;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using AngleSharp;
using AngleSharp.Dom;
using System.Threading.Tasks;
using System.Windows.Documents;
using EnvDTE;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Threading;
using System;

namespace deadlineViewer
{
    /// <summary>
    /// Interaction logic for DeadlineVeiwerControl.
    /// </summary>
    public partial class DeadlineVeiwerControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeadlineVeiwerControl"/> class.
        /// </summary>
        public DeadlineVeiwerControl()
        {
            this.InitializeComponent();
        }

        private string siteUrl = "https://notxaa.notion.site/notxaa/M3100-d86c5e1baacf4ab8ba2f6b8f1ab9e451";
        // private string siteUrl = "file:///C:/Users/sasha/Documents/ITMO/DevsTools/deadlineViewer/Resources/page.html";

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
        internal async void Update()
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(siteUrl);
            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);

            GetScrapeResults(document);
        }
        private void GetScrapeResults(IHtmlDocument document)
        {
            IEnumerable<IElement> articleLink = null;

                articleLink = document.All.Where(x =>
                    x.ClassName == "notion-app");
            IHtmlBodyElement body = document.Children[0].FindChild<IHtmlBodyElement>();
            IElement div = body.Children.Where(m => m.Id == "notion-app").First();
            div = body.Children.Where(m => m.ClassName == "notion-app-inner notion-light-theme").First();
            div = body.Children.First();
            div = body.Children.Where(m => m.ClassName == "notion-cursor-listener").First(); 
            div = body.Children.Where(m => m.ClassName == "notion-frame").First();



            //Overwriting articleLink above means we have to print it's result for all QueryTerms
            //Appending to a pre-declared IEnumerable (like a List), could mean taking this out of the main loop.
            if (articleLink.Any())
                {
                    PrintResults(articleLink);
                }
            
        }
        public void PrintResults(IEnumerable<IElement> articleLink)
        {
            //Every element needs to be cleaned and displayed
            TextBox t = new TextBox { Height = 200, Width = 300, Text = articleLink.ToString() };
            main_grid.Children.Add(t);
        }
        private void delete_self(object sender, RoutedEventArgs e)
        {
            main_grid.Children.Remove((Button)sender);
        }
    }
}