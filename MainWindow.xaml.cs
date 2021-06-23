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
using System.Diagnostics;
using BagageSorteringssystem;
namespace BagageSystem_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Manager Controller;
        public MainWindow()
        {
            InitializeComponent();
            Controller = new Manager();
            Controller.luggageProducer.LuggageChanged += Controller_LuggageChanged;
            Controller.flightMan.TimeHandler += Flightman_TimeChanged;

            Controller.flightMan.Checkinhandler[0] += Checkin_Changed;
        }

        private void Checkin_Changed(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {

                if (e is LuggageCounterEventArgs)
                {
                    Check_In check_In = (Check_In)sender;
                    int selection = ((LuggageCounterEventArgs)e).LuggageCount;
                    if (check_In.MyStatus == Check_In.Status.open)
                    {

                        switch (selection)
                        {
                            case 0:
                                CheckIn1.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#1BA1E2");
                                break;
                            case 1:
                                CheckIn2.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#1BA1E2");
                                break;
                            default:
                                break;
                        }



                    }

                }
            });
        }

        //show current time
        private void Flightman_TimeChanged(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {

                if (e is TimeChangedEvent)
                {
                    Timer_counter.Content = $"{((TimeChangedEvent)e).CurrentTime.ToShortTimeString()}";
                }
            });
        }

        //show number of luggage incoming
        private void Controller_LuggageChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("luggage incoming");
            Dispatcher.Invoke(() =>
            {

                if (e is LuggageCounterEventArgs)
                {
                    Queue_text.Text = $"In Queue for check in {((LuggageCounterEventArgs)e).LuggageCount}";
                }
            });
        }

        //enable or disable simulation
        private void EnableDisableSim(object sender, RoutedEventArgs e)
        {
            if (!Controller.Isrunning)
            {
                Controller.RunSim();
                StartSim_Text.Text = "Running";
                StartSim.Background = Brushes.Red;
            }
            else
            {
                Controller.Isrunning = false;
                Controller.flightMan.IsRunning = false;
                StartSim_Text.Text = "Start Simulation";
                StartSim.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#515C8F");
            }


        }

        //change time factor 
        private void TimeFactor_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = (int)Timer_slider.Value;
            if (Controller != null)
            {
                lbl_TimeFactor.Content = $"Time Factor {value}";
                Controller.flightMan.TimeFactor = value;
            }
        }
    }
}
