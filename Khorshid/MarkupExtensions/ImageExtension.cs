using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Khorshid.MarkupExtensions
{
    public class ImageExtension : MarkupExtension
    {
        public ImageSource Source { get; set; }
        public ImageExtension(ImageSource src)
        {
            Source = src;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
         => new Image
         {
             Source = Source
         };
    }
}
