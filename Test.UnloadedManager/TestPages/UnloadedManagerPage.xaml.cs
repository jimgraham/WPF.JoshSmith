using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test.UnloadedManager.TestPages
{
	/// <summary>
	/// Demonstrates the UnloadedManager class.
	/// </summary>
	public partial class UnloadedManagerPage : Page
	{
		int logCounter = 0;

		public UnloadedManagerPage()
		{
			InitializeComponent();
			this.Loaded += UnloadedManagerPage_Loaded;
			this.btnClearLog.Click += btnClearLog_Click;
		}

		void UnloadedManagerPage_Loaded( object sender, RoutedEventArgs e )
		{
			// Populate the ListBox with some ImageInfo objects.

			List<ImageInfo> imageInfos = new List<ImageInfo>();

			List<string> fileNames = new List<string>();
			fileNames.AddRange( System.IO.Directory.GetFiles( @"..\..\..\Resources\Images\Robots", "*.jpg" ) );
			fileNames.AddRange( System.IO.Directory.GetFiles( @"..\..\..\Resources\Images\Composers", "*.jpg" ) );
			foreach( string fileName in fileNames )
			{
				string absolutePath = System.IO.Path.GetFullPath( fileName );
				ImageInfo imageInfo = new ImageInfo( absolutePath );
				imageInfo.ImageLoaded += imageInfo_ImageLoaded;
				imageInfo.ImageUnloaded += imageInfo_ImageUnloaded;
				imageInfos.Add( imageInfo );
			}

			listBoxImages.ItemsSource = imageInfos;
		}

		void imageInfo_ImageLoaded( object sender, EventArgs e )
		{
			this.Log( sender as ImageInfo, true );
		}

		void imageInfo_ImageUnloaded( object sender, EventArgs e )
		{
			this.Log( sender as ImageInfo, false );
		}

		void Log( ImageInfo imageInfo, bool imageLoaded )
		{
			string msg = String.Format( 
				"{0} - {1} BitmapImage: {2}", 
				++this.logCounter, 
				imageLoaded ? "Loaded" : "Unloaded",
				System.IO.Path.GetFileName( imageInfo.FileName ) );
			this.listBoxLog.Items.Add( msg );
			this.listBoxLog.ScrollIntoView( msg );
			this.listBoxLog.SelectedItem = msg;
		}
		
		void btnClearLog_Click( object sender, RoutedEventArgs e )
		{
			this.listBoxLog.Items.Clear();
			this.logCounter = 0;
		}
	}
}