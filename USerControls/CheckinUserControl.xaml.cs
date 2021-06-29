using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BagageSystem_WPF
{
    /// <summary>
    /// Interaction logic for CheckinUserControl.xaml
    /// </summary>
    public partial class CheckinUserControl : UserControl
    {
        //properties
       
        // Using a DependencyProperty as the backing store for AirlineProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AirlinePropertyProperty =
            DependencyProperty.Register("AirlineProperty", typeof(string), typeof(CheckinUserControl), new PropertyMetadata("", new PropertyChangedCallback(airlinechange)));

        // Using a DependencyProperty as the backing store for NameProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NamePropertyProperty =
            DependencyProperty.Register("NameProp", typeof(string), typeof(CheckinUserControl), new PropertyMetadata("", new PropertyChangedCallback(OnNameChanged)));

        // Using a DependencyProperty as the backing store for BackgroundProberty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundProbertyProperty =
            DependencyProperty.Register("BackgroundProberty", typeof(Brush), typeof(CheckinUserControl), new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom("#1BA1E2"), new PropertyChangedCallback(OnBackgroundChanged)));

        //get setter
        public string AirlineProperty
        {
            get { return (string)GetValue(AirlinePropertyProperty); }
            set { SetValue(AirlinePropertyProperty, value); }
        }
        public string NameProp
        {
            get { return (string)GetValue(NamePropertyProperty); }
            set { SetValue(NamePropertyProperty, value); }
        }
        public Brush BackgroundProberty
        {
            get { return (Brush)GetValue(BackgroundProbertyProperty); }
            set { SetValue(BackgroundProbertyProperty, value); }
        }

        //callback methods
        private static void airlinechange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckinUserControl cic = d as CheckinUserControl;
            cic.lbl_airline.Content = e.NewValue.ToString();
        }
        private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckinUserControl cic = d as CheckinUserControl;
            cic.lbl_name.Content = e.NewValue.ToString();
        }
        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckinUserControl cic = d as CheckinUserControl;
            cic.border.Background = (Brush)e.NewValue;
        }

        public CheckinUserControl()
        {
            InitializeComponent();
        }
    }
}
