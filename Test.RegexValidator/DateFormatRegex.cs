using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Test.RegexValidator
{
	public static class DateFormatRegex
	{
		/// <summary>
		/// Returns a regular expression which can be used to validate date strings,
		/// based on the current culture of the UI thread.
		/// </summary>
		public static string DateRegex
		{
			get
			{
				// M/d/yyyy (USA)
				// dd/MM/yyyy (Great Britian)			
				//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo( "en-GB" );

				string format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
				//Debug.WriteLine( "DateTime Format: " + format );

				string regex = null;

				switch( format )
				{
					case "M/d/yyyy":
						regex = @"^(0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$";
						break;

					case "dd/MM/yyyy":
						regex = @"^(0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](19|20)\d\d$";
						break;

					default:
						// I'm too lazy to figure out regexs for all the date formats.
						// If you need to account for some other culture, just add another
						// case to this switch with the regex in it.
						Debug.Fail( "Unexpected date format: " + format );
						break;
				}

				return regex;
			}
		}
	}
}