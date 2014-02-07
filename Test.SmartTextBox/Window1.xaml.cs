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

namespace Test.SmartTextBox
{
	/// <summary>
	/// Demo app for the SmartTextBox control.
	/// </summary>
	public partial class Window1 : System.Windows.Window
	{
		public Window1()
		{
			InitializeComponent();
			this.Loaded += Window1_Loaded;
		}

		void Window1_Loaded( object sender, RoutedEventArgs e )
		{
			this.btnFixNextTypo.Click += btnFixNextTypo_Click;
		}

		void btnFixNextTypo_Click( object sender, RoutedEventArgs e )
		{
			// Find the next spelling error and show the list of suggestions for it.
			int caretIdx = this.smartTextBox.CaretIndex;
			int idx = this.smartTextBox.GetNextSpellingErrorCharacterIndex( caretIdx, LogicalDirection.Forward );
			if( idx > -1 )
			{
				this.smartTextBox.CaretIndex = idx;
				this.smartTextBox.ShowSuggestions();
			}
		}
	}
}