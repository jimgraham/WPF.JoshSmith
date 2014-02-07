using System;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using WPF.JoshSmith.ServiceProviders.UI;

namespace Test.UnloadedManager
{
	/// <summary>
	/// Lazily loads a BitmapImage, based on the FileName property.
	/// </summary>
	public class ImageInfo : IUnloadable
	{
		private string fileName;
		private BitmapImage image;

		public ImageInfo( string fileName )
		{
			this.fileName = fileName;
		}

		public event EventHandler ImageLoaded;
		public event EventHandler ImageUnloaded;

		public string FileName
		{
			get { return this.fileName; }
		}

		public BitmapImage Image
		{
			get
			{
				if( this.image == null )
				{
					this.image = new BitmapImage( new Uri( this.FileName, UriKind.RelativeOrAbsolute ) );

					if( this.ImageLoaded != null )
						this.ImageLoaded( this, EventArgs.Empty );
				}

				return this.image;
			}
		}

		void IUnloadable.Unload()
		{			
			this.image = null;

			if( this.ImageUnloaded != null )
				this.ImageUnloaded( this, EventArgs.Empty );
		}
	}
}
