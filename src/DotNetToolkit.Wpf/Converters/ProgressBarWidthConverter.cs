using System;

namespace DotNetToolkit.Wpf.Converters
{
    using System.Globalization;
    using System.Windows.Data;

    public class ProgressBarWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var contentWidth = (double)values[0];
            var parentMinWidth = (double)values[1];

            return Math.Max(contentWidth, parentMinWidth);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
