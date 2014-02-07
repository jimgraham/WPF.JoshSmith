using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test.RegexValidator
{
	/// <summary>
	/// Tests the RegexValidator and RegexValidationRule classes.
	/// </summary>
	public partial class RegexValidatorWindow : Window
	{
		public RegexValidatorWindow()
		{
			// Uncomment this line to test the localized date format regex 
			// (unless, of course, if you live in Great Britian!).
			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "en-GB" );

			InitializeComponent();

			TestData data = new TestData();
			data.EmailAddress = "yo_mamma@hotbabes26.com";
			data.DateString = DateTime.Now.Date.ToShortDateString();
			data.ProductCode = "WRT.823";
			this.DataContext = data;

			this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
		}
	}
}