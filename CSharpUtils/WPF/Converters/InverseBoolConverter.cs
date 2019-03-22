namespace CSharpUtils.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException("Can`t convert back");
    }
}