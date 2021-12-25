using System;
using System.Globalization;
using StandUpTimer.Localization;

namespace StandUpTimer.ValueConverters;

internal class MinuteFormatConverter : BaseValueConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
            return doubleValue + " " + LocalizationResources.Minute;

        return value;
    }
}