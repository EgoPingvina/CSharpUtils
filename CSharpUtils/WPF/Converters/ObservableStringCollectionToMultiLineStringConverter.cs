namespace CSharpUtils.WPF.Converters
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Text;
    using System.Windows.Data;

    /// <summary>
    /// Преобразует ObservableCollection<string> в многострочный текст
    /// </summary>
    public class ObservableStringCollectionToMultiLineStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var input = values[0] as ObservableCollection<string>;

            if (input == null)
                throw new InvalidOperationException("В качестве первого входного значения должен быть передан ObservableCollection<string>");

            // Если переданная коллекция пуста-возвращаем пустую строку
            if (input.Count == 0)
                return string.Empty;

            var result = new StringBuilder();
            foreach (string msg in input)
                result.AppendLine(msg);

            return result.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}