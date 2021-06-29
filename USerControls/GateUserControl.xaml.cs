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
    /// Interaction logic for GateUserControl.xaml
    /// </summary>
    public partial class GateUserControl : UserControl
    {
        // Using a DependencyProperty as the backing store for SetNameProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SetNamePropertyProperty =
            DependencyProperty.Register("SetNameProperty", typeof(string), typeof(GateUserControl), new PropertyMetadata("", new PropertyChangedCallback(OnNameChanged)));

        // Using a DependencyProperty as the backing store for SetLuggageProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SetLuggagePropertyProperty =
            DependencyProperty.Register("SetLuggageProperty", typeof(string), typeof(GateUserControl), new PropertyMetadata("", new PropertyChangedCallback(OnLuggageChanged)));

        // Using a DependencyProperty as the backing store for SetBackgroundproperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SetBackgroundpropertyProperty =
            DependencyProperty.Register("SetBackgroundproperty", typeof(Brush), typeof(GateUserControl), new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom("#FF0C4561"), new PropertyChangedCallback(OnBackgroundChanged)));

        //callback methods
        private static void OnLuggageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GateUserControl guc = d as GateUserControl;
            guc.lbl_luggageCount.Content = e.NewValue.ToString();
        }
        private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GateUserControl guc = d as GateUserControl;
            guc.lbl_Name.Content = e.NewValue.ToString();
        }
        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GateUserControl guc = d as GateUserControl;
            guc.Gate.Background = (Brush)e.NewValue;
        }

        //properties
        public string SetLuggageProperty
        {
            get { return (string)GetValue(SetLuggagePropertyProperty); }
            set { SetValue(SetLuggagePropertyProperty, value); }
        }
        public string SetNameProperty
        {
            get { return (string)GetValue(SetNamePropertyProperty); }
            set { SetValue(SetNamePropertyProperty, value); }
        }
        public Brush SetBackgroundproperty
        {
            get { return (Brush)GetValue(SetBackgroundpropertyProperty); }
            set { SetValue(SetBackgroundpropertyProperty, value); }
        }

        public GateUserControl()
        {
            InitializeComponent();
        }
    }
}
