using Microsoft.VisualStudio.PlatformUI;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

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

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            Button add_b = new Button
            {
                Content = "Click)",
                Height = 20,
                Width = 20
            };
            add_b.Click += new RoutedEventHandler(delete_self);
            main_grid.Children.Add(add_b);
        }
        private void delete_self(object sender, RoutedEventArgs e)
        {
            main_grid.Children.Remove((Button)sender);
        }
    }
}