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
    /// Interaction logic for TerminalUserControl.xaml
    /// </summary>
    public partial class TerminalUserControl : UserControl
    {
        public static DependencyProperty setFlightNumberProperty =
            DependencyProperty.Register("FlightNo", typeof(string), typeof(TerminalUserControl), new PropertyMetadata("", new PropertyChangedCallback(OnFlightNoChanged)));

        // Using a DependencyProperty as the backing store for Gate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GateProperty =
            DependencyProperty.Register("Gate", typeof(string), typeof(TerminalUserControl), new PropertyMetadata("", new PropertyChangedCallback(OnGateChanged)));

        // Using a DependencyProperty as the backing store for Arrival.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArrivalProperty =
            DependencyProperty.Register("Arrival", typeof(DateTime), typeof(TerminalUserControl), new PropertyMetadata(DateTime.Now, new PropertyChangedCallback(OnArrivalChanged)));

        // Using a DependencyProperty as the backing store for Departure.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DepartureProperty =
            DependencyProperty.Register("Departure", typeof(string), typeof(TerminalUserControl), new PropertyMetadata("", new PropertyChangedCallback(OnDepartureChanged)));

        // Using a DependencyProperty as the backing store for Destination.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DestinationProperty =
            DependencyProperty.Register("Destination", typeof(string), typeof(TerminalUserControl), new PropertyMetadata("", new PropertyChangedCallback(OnDestintionChanged)));

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(TerminalUserControl), new PropertyMetadata("", new PropertyChangedCallback(OnStatusChanged)));

        private static void OnFlightNoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TerminalUserControl tuc = d as TerminalUserControl;
            tuc.lbl_FlightNo.Content = e.NewValue.ToString();
        }
        private static void OnGateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TerminalUserControl tuc = d as TerminalUserControl;
            tuc.lbl_Gate.Content = e.NewValue.ToString();
        }
        private static void OnDepartureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TerminalUserControl tuc = d as TerminalUserControl;
            tuc.lbl_Departure.Content = e.NewValue.ToString();
        }
        private static void OnArrivalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TerminalUserControl tuc = d as TerminalUserControl;
            DateTime arrival = (DateTime)e.NewValue;
            tuc.lbl_Arrival.Content = arrival.ToShortTimeString();
        }
        private static void OnDestintionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TerminalUserControl tuc = d as TerminalUserControl;
            tuc.lbl_Destination.Content = e.NewValue.ToString();
        }
        private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TerminalUserControl tuc = d as TerminalUserControl;
            tuc.lbl_Status.Content = e.NewValue.ToString();
        }

        public string FlightNo
        {
            get { return (string)GetValue(setFlightNumberProperty); }
            set { SetValue(setFlightNumberProperty, value); }
        }
        public string Gate
        {
            get { return (string)GetValue(GateProperty); }
            set { SetValue(GateProperty, value); }
        }
        public DateTime Arrival
        {
            get { return (DateTime)GetValue(ArrivalProperty); }
            set { SetValue(ArrivalProperty, value); }
        }
        public string Departure
        {
            get { return (string)GetValue(DepartureProperty); }
            set { SetValue(DepartureProperty, value); }
        }
        public string Destination
        {
            get { return (string)GetValue(DestinationProperty); }
            set { SetValue(DestinationProperty, value); }
        }
        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }



        public Brush BackGroundPropterty
        {
            get { return (Brush)GetValue(BackGroundProptertyProperty); }
            set { SetValue(BackGroundProptertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackGroundPropterty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackGroundProptertyProperty =
            DependencyProperty.Register("BackGroundPropterty", typeof(Brush), typeof(TerminalUserControl), new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom("#1BA1E2"), new PropertyChangedCallback(backgroundChanged)));

        private static void backgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TerminalUserControl tuc = d as TerminalUserControl;
            tuc.TerminalElement.Background = tuc.BackGroundPropterty;
        }

        public TerminalUserControl()
        {
            InitializeComponent();
        }
    }
}
