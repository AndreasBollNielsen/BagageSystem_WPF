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
            Controller.InitializationHandler += Initialize;


        }

        //update terminal flights info
        private void Flight_Changed(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e is FlightPlanEventArgs)
                {
                    FlightPlanEventArgs flight = ((FlightPlanEventArgs)e);
                    int selection = flight.Index;

                    TerminalUserControl terminalElement = (TerminalUserControl)Terminal_itemsControl.Items.GetItemAt(selection);
                    terminalElement.Status = flight.FlightStatus;
                    terminalElement.Gate = flight.GateName;


                    //set color background
                    if (flight.FlightStatus == "Boarding")
                    {
                        terminalElement.BackGroundPropterty = (SolidColorBrush)new BrushConverter().ConvertFrom("#1BA1E2");

                    }
                    else
                    {
                        terminalElement.BackGroundPropterty = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF7A7A7A");

                    }
                }
            });
        }

        private void Gate_Changed(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e is GateEventArgs)
                {
                    GateEventArgs Gate_obj = ((GateEventArgs)e);
                    int selection = Gate_obj.Index;
                    GateUserControl gate = (GateUserControl)Gates_grid.Children[selection];


                    if (Gate_obj.GateStatus == Gate.Status.open)
                    {
                        gate.SetBackgroundproperty = (SolidColorBrush)new BrushConverter().ConvertFrom("#1BA1E2");
                        gate.SetLuggageProperty = $"{Gate_obj.NumLuggage}/{Gate_obj.MaxLuggage}";



                    }
                    else
                    {
                        gate.SetBackgroundproperty = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0C4561");
                        gate.SetLuggageProperty = $"{Gate_obj.NumLuggage}/{Gate_obj.MaxLuggage}";
                    }

                }
            });
        }

        private void Initialize(object sender, EventArgs e)
        {
            //setup handlers
            //subscribe Checkin eventhandlers
            for (int i = 0; i < Controller.flightMan.Checkinhandler.Length; i++)
            {
                Controller.flightMan.Checkinhandler[i] += Checkin_Changed;
                Controller.flightMan.Gatehandler[i] += Gate_Changed;

                //add flightplan events
                Controller.flightMan.FlightHandler[i] += Flight_Changed;

                //add luggage events
                Manager.gates[i].luggageHandler += Gate_Changed;
                Manager.gates[i].GateStatusHandler += Gate_Changed;
                Manager.gates[i].FlightStatusHandler += Flight_Changed;
            }

            //set gate names
            for (int j = 0; j < Manager.gates.Length; j++)
            {
                if (j < Gates_grid.Children.Count)
                {
                    GateUserControl gate = (GateUserControl)Gates_grid.Children[j];
                    gate.SetNameProperty = Manager.gates[j].GateName;

                }
            }

            //dynamically create terminal info
            InitializeTerminal();
        }

        private void Checkin_Changed(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {

                if (e is CheckInChangedEventArgs)
                {
                    CheckInChangedEventArgs check_In = ((CheckInChangedEventArgs)e);
                    int selection = check_In.Index;
                    if (check_In.MyStatus == Check_In.Status.open)
                    {
                        CheckinUserControl checkin = (CheckinUserControl)CheckInGrid.Children[selection];
                        checkin.BackgroundProberty = (SolidColorBrush)new BrushConverter().ConvertFrom("#1BA1E2");
                        checkin.AirlineProperty = check_In.Name;

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
            // Debug.WriteLine("luggage incoming");
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
                Controller.WPFRunning = true;
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
                Controller.UpdateLocalTimeFactor();

            }
        }

        //add new checkin
        private void AddCheckinButton(object sender, RoutedEventArgs e)
        {
            Controller.flightMan.OpenCheckIn();
        }

        //gets data from Manager and adds it to the terminal
        private void InitializeTerminal()
        {
            foreach (FlightPlan flight in Manager.flightPlans)
            {
                TerminalUserControl usercontrol = new TerminalUserControl();
                usercontrol.FlightNo = flight.FlightNumber;
                usercontrol.Gate = " ";
                usercontrol.Destination = flight.Destination;
                usercontrol.Arrival = flight.ArrivalTime;
                usercontrol.Departure = flight.DepartureTime.ToShortTimeString();
                usercontrol.Status = "Closed";
                Terminal_itemsControl.Items.Add(usercontrol);
            }




            // Terminal_itemsControl.Items.SortDescriptions.Clear();
            // Terminal_itemsControl.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Arrival", System.ComponentModel.ListSortDirection.Ascending));

            //Debug.WriteLine(Manager.gates[0].GateName);
        }
    }
}
