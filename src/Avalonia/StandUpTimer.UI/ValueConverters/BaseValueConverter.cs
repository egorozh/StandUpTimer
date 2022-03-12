using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using System.Globalization;

namespace StandUpTimer.UI.ValueConverters;

internal abstract class BaseValueConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public abstract object Convert(object? value, Type targetType, object? parameter, CultureInfo culture);

    public virtual object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}