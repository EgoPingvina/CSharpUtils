namespace CSharpUtils.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    using Converter = System.Convert;

    [ValueConversion(typeof(object[]), typeof(string))]
    public class StringFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => string.Format(culture, (string)values[1], values[0]);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (targetTypes[0] == typeof(int))
                return new object[]
                    {
                        Converter.ToInt32(value),
                        string.Empty
                    };

            return double.TryParse((string)value, out double val)
                ? new object[] { val, string.Empty }
                : new object[] { 0.0, string.Empty };
        }
    }
}