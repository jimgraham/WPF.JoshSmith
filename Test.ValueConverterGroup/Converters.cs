using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml;

namespace Test.ValueConverterGroup
{
	#region ColorToSolidColorBrushConverter

	/// <summary>
	/// Converts a Color to a SolidColorBrush and vice versa.
	/// </summary>
	[ValueConversion( typeof( Color ), typeof( SolidColorBrush ) )]
	public class ColorToSolidColorBrushConverter : IValueConverter
	{
		object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			Debug.Assert( value is Color, "value should be a Color" );
			Debug.Assert( typeof( Brush ).IsAssignableFrom( targetType ), "targetType should be Brush or derived from Brush" );

			return new SolidColorBrush( (Color)value );
		}

		object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			Debug.Assert( value is SolidColorBrush, "value should be a SolidColorBrush" );
			Debug.Assert( targetType == typeof( Color ), "targetType should be Color" );

			return (value as SolidColorBrush).Color;
		}
	}

	#endregion // ColorToSolidColorBrushConverter

	#region EnumToDescriptionConverter

	/// <summary>
	/// Returns the value of the DescriptionAttribute applied to an enum value, or an empty string
	/// if the enum value is not decorated with the attribute.
	/// </summary>
	[ValueConversion( typeof( Enum ), typeof( string ) )]
	public class EnumToDescriptionConverter : IValueConverter
	{
		object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			Debug.Assert( value is Enum, "value should be an Enum" );
			Debug.Assert( targetType.IsAssignableFrom( typeof( string ) ), "targetType should assignable from a String" );

			FieldInfo field = value.GetType().GetField( value.ToString() );
			object[] attrs = field.GetCustomAttributes( typeof( DescriptionAttribute ), false );
			if( attrs.Length > 0 )
			{
				DescriptionAttribute attr = attrs[0] as DescriptionAttribute;
				return attr.Description;
			}

			return "";
		}

		object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException( "ConvertBack not supported." );
		}
	}

	#endregion // EnumToDescriptionConverter

	#region EnumToDisplayNameConverter

	/// <summary>
	/// Returns the value of the DescriptionAttribute applied to an enum value, or an empty string
	/// if the enum value is not decorated with the attribute.
	/// </summary>
	[ValueConversion( typeof( Enum ), typeof( string ) )]
	public class EnumToDisplayNameConverter : IValueConverter
	{
		object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			Debug.Assert( value is Enum, "value should be an Enum" );
			Debug.Assert( targetType.IsAssignableFrom( typeof( string ) ), "targetType should assignable from a String" );

			return value.ToString();
		}

		object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException( "ConvertBack not supported." );
		}
	}

	#endregion // EnumToDisplayNameConverter

	#region IntegerStringToProcessingStateConverter

	[ValueConversion( typeof( string ), typeof( ProcessingState ) )]
	public class IntegerStringToProcessingStateConverter : IValueConverter
	{
		object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			int state;
			bool numeric = Int32.TryParse( value as string, out state );
			Debug.Assert( numeric, "value should be a String which contains a number" );
			Debug.Assert( targetType.IsAssignableFrom( typeof( ProcessingState ) ), "targetType should be ProcessingState" );

			switch( state )
			{
				case -1:
					return ProcessingState.Complete;

				case 0:
					return ProcessingState.Pending;

				case +1:
					return ProcessingState.Active;
			}
			return ProcessingState.Unknown;
		}

		object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException( "ConvertBack not supported." );
		}
	}

	#endregion // IntegerStringToProcessingStateConverter

	#region ProcessingStateToColorConverter

	[ValueConversion( typeof( ProcessingState ), typeof( Color ) )]
	public class ProcessingStateToColorConverter : IValueConverter
	{
		object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			Debug.Assert( value is ProcessingState, "value should be a ProcessingState" );
			Debug.Assert( targetType == typeof( Color ), "targetType should be Color" );

			switch( (ProcessingState)value )
			{
				case ProcessingState.Pending:
					return Colors.Red;

				case ProcessingState.Complete:
					return Colors.Gold;

				case ProcessingState.Active:
					return Colors.Green;
			}
			return Colors.Transparent;
		}

		object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException( "ConvertBack not supported." );
		}
	}

	#endregion // ProcessingStateToColorConverter	

	#region XmlAttributeToStringStateConverter

	/// <summary>
	/// Converts an XmlAttribute to its Value, as a string.
	/// </summary>
	[ValueConversion( typeof( XmlAttribute ), typeof( string ) )]
	public class XmlAttributeToStringStateConverter : IValueConverter
	{
		object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			Debug.Assert( value is XmlAttribute, "value should be an XmlAttribute" );
			Debug.Assert( targetType == typeof( string ), "targetType should be String" );

			XmlAttribute attr = value as XmlAttribute;
			return attr.Value;
		}

		object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException( "ConvertBack not supported." );
		}
	}

	#endregion // XmlAttributeToStringStateConverter
}