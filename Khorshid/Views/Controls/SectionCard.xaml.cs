using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Khorshid.Views.Controls
{
    /// <summary>
    /// Interaction logic for SectionCard.xaml
    /// </summary>
    public partial class SectionCard : UserControl
    {
        public SectionCard()
        {
            InitializeComponent();
        }

        public ImageSource CardImage
        {
            get { return (ImageSource)GetValue(CardImageProperty); }
            set { SetValue(CardImageProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(SectionCard));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SectionCard));

        public static readonly DependencyProperty CardImageProperty =
            DependencyProperty.Register("CardImage", typeof(ImageSource), typeof(SectionCard));

    }
}
