using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using RentACar.ViewModel.ViewModels;

namespace RentACar.Wpf
{
    public class PageToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PageType current && parameter is string targetName &&
                Enum.TryParse<PageType>(targetName, out var target))
            {
                return current == target ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
