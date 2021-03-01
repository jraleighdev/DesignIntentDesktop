using FontAwesome.WPF;
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

namespace DesignIntentDesktop.Components.Shared.Buttons
{
    /// <summary>
    /// Interaction logic for IconButton.xaml
    /// </summary>
    public partial class IconButton : UserControl
    {
        public static DependencyProperty CommandProperty
        = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(IconButton));

        public static DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(IconButton));

        //public static DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(IconButton),
        //    new PropertyMetadata(true));

        public static DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(IconButton));

        public static DependencyProperty IconColorProperty = DependencyProperty.Register("IconColor", typeof(Brush), typeof(IconButton), 
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static DependencyProperty BackGroundColorProperty = DependencyProperty.Register("BackGroundColor", typeof(Brush), typeof(IconButton),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public IconButton()
        {
            InitializeComponent();
        }

        //public bool IsEnabled
        //{
        //    get
        //    {
        //        return (bool)GetValue(IsEnabledProperty);
        //    }
        //    set
        //    {
        //        SetValue(IsEnabledProperty, value);
        //    }
        //}

        public FontAwesomeIcon Icon
        {
            get
            {
                return (FontAwesomeIcon)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }

        public Brush IconColor
        {
            get
            {
                return (Brush)GetValue(IconColorProperty);
            }
            set
            {
                SetValue(IconColorProperty, value);
            }
        }

        public Brush BackGroundColor
        {
            get
            {
                return (Brush)GetValue(BackGroundColorProperty);
            }
            set
            {
                SetValue(BackGroundColorProperty, value);
            }
        }

        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }
    }
}
