using System;
using System.Globalization;

namespace StandUpTimer.ValueConverters;

internal class TimeOnlyToTimeSpanConverter: BaseValueConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TimeOnly time)
            return time.ToTimeSpan();

        return new TimeSpan();
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TimeSpan time)
            return TimeOnly.FromTimeSpan(time);

        return new TimeOnly();
    }
}