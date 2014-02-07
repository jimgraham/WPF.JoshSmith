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
using WPF.JoshSmith.Controls;

namespace Test.SlidingListBox
{
	/// <summary>
	/// Demo application for the SlidingListBox control.
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
			this.Loaded += Window1_Loaded;
		}

		void Window1_Loaded( object sender, RoutedEventArgs e )
		{
			//
			// Create data sources for the SlidingListBoxs.
			//

			Brush[] brushes = new SolidColorBrush[8];
			for( int i = 0; i < 8; ++i )
			{
				byte val = (byte)(255 / (i + 1) * i);
				Color color = Color.FromRgb( val, val, val );
				brushes[i] = new SolidColorBrush( color );
			}
			this.slidingListBoxDown.ItemsSource = brushes;

			this.slidingListBoxRight.ItemsSource = new string[]
            {
                "Argentina",
                "Brooklyn",
                "Cambodia",
                "Denmark",
                "England",
                "Fairfield",
                "Germany",
                "Hamburg",
                "Indonesia",
            };

			this.slidingListBoxLeft.ItemsSource = new string[]
            {
                "Once",
                "Upon",
                "A",
                "Time",
                "There",
                "Were",
                "These",
                "Things",
                "Called",
                "HWNDs..."
            };

			this.slidingListBoxUp.ItemsSource = new int[]
            {
                123,
                234,
                345,
                456,
                567,
                678,
                789
            };
		}
	}
}