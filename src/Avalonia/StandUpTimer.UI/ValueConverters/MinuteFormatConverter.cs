using StandUpTimer.Localization;
using System.Globalization;

namespace StandUpTimer.UI.ValueConverters;

internal class MinuteFormatConverter : BaseValueConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
            return $"{doubleValue} {LocalizationResources.Minute}";

        return value;
    }
}