using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using AngleSharp.Dom;
using System.Threading.Tasks;
using System.Collections.Generic;
using deadlineViewer.Parser;
using deadlineViewer.Parser.Notion;

namespace deadlineViewer
{
    /// <summary>
    /// Interaction logic for DeadlineVeiwerControl.
    /// </summary>
    /// 
    class Deadline
    {
        public string WorkName { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public string DeadLine { get; set; }
    }
    public partial class DeadlineVeiwerControl : UserControl
    {
        ParserWorker<string[]> parser;
        Task tasker;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeadlineVeiwerControl"/> class.
        /// </summary>
        public DeadlineVeiwerControl()
        {
            this.InitializeComponent();
            parser = new ParserWorker<string[]>(
                    new NotionParser()
                );

            parser.OnNewData += Parser_OnNewData;

        }

        private async void Parser_OnNewData(object arg1, string[] arg2)
        {
            for (int i = 0; i < 64; i += 4)
            {
                ListItems.Items.Add(new Deadline
                {
                    WorkName = arg2[i],
                    Subject = arg2[i + 1],
                    Status = arg2[i + 2],
                    DeadLine = arg2[i + 3]
                });
            }   
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            parser.Settings = new NotionSettings();
            parser.Start();
        }
    }
}