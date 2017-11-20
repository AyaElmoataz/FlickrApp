using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.UI.Converters;
using Windows.UI.Xaml;

namespace FlickrApp.Converters
{
    /// <summary>
    /// This class converts a boolean value into a Visibility enumeration.
    /// </summary>
    public class BoolToVisibilityConverter : BoolToObjectConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoolToVisibilityConverter"/> class.
        /// </summary>
        public BoolToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }
    }
}
