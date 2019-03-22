namespace CSharpUtils.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    using Converter = System.Convert;

    public class RatioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (double)value * Converter.ToDouble(parameter);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException("Can`t convert back");
    }
}