// This file is part of the SimWinInput project, which is released under MIT License.
// For details, see: https://github.com/DavidRieman/SimWinInput

using System.Diagnostics;

namespace SimExamples
{
    using SimWinInput;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Threading;

    public partial class MainWindow : Window
    {
        private DispatcherTimer updateTimer = new DispatcherTimer();

        public delegate void UpdateTextCallback(string message);

        public MainWindow()
        {
            InitializeComponent();
            updateTimer.Interval = TimeSpan.FromMilliseconds(100);
            updateTimer.Tick += UpdateMousePositionText;
            updateTimer.Start();
        }
        private void MoveCursorToCornerButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            //var t = DateTime.Now.Ticks.ToString().Reverse().Take(10).Select(x3 => x3.ToString());
            var t = DateTime.Now.Ticks.ToString();

            
            //bool isPrimary = primaryScreen.Primary;
            //var deviceName = primaryScreen.DeviceName;

            // make a real random but who cares
            Screen primaryScreen = Screen.PrimaryScreen;
            var primaryScreenWorkingArea = primaryScreen.WorkingArea;
            var height = primaryScreenWorkingArea.Height;
            var width = primaryScreenWorkingArea.Width;
            var bottom = primaryScreenWorkingArea.Bottom;
            var isEmpty = primaryScreenWorkingArea.IsEmpty;
            var left = primaryScreenWorkingArea.Left;
            var right = primaryScreenWorkingArea.Right;
            System.Drawing.Point location = primaryScreenWorkingArea.Location;

            Random randomNumberGenerator = new Random();
            var i = 1;
            while (true)
            {
                int nextYAxisMouseLocationPoint = randomNumberGenerator.Next(primaryScreenWorkingArea.Height, primaryScreenWorkingArea.Bottom);
                int nextXAxisMouseLocationPoint = randomNumberGenerator.Next(left, right);
                SimMouse.Act(SimMouse.Action.MoveOnly, nextXAxisMouseLocationPoint, nextYAxisMouseLocationPoint);
                Thread.Sleep(3000);
                Debug.WriteLine($"Loopcount: {i} ");
                Debug.WriteLine($"x:{nextXAxisMouseLocationPoint} y:{nextYAxisMouseLocationPoint}");
            }



            //SimMouse.Act(SimMouse.Action.MoveOnly, 0, 0);
        }


        private void MoveCursorToCornerButton_ClickExperiment(object sender, RoutedEventArgs e)
        {
            
            
            //var t = DateTime.Now.Ticks.ToString().Reverse().Take(10).Select(x3 => x3.ToString());
            var t = DateTime.Now.Ticks.ToString();

            
            //bool isPrimary = primaryScreen.Primary;
            //var deviceName = primaryScreen.DeviceName;

            // make a real random but who cares
            Screen primaryScreen = Screen.PrimaryScreen;
            
            var primaryScreenWorkingArea = primaryScreen.WorkingArea;
            var height = primaryScreenWorkingArea.Height;
            var width = primaryScreenWorkingArea.Width;
            var bottom = primaryScreenWorkingArea.Bottom;
            var isEmpty = primaryScreenWorkingArea.IsEmpty;
            var left = primaryScreenWorkingArea.Left;
            var right = primaryScreenWorkingArea.Right;
            System.Drawing.Point location = primaryScreenWorkingArea.Location;

            Random randomNumberGenerator = new Random();
            
            var startY = (height) / 2 ;
            var startX = (width) / 2 ;


            var i = 1;
            while (true)
            {
                SimMouse.Act(SimMouse.Action.MoveOnly, startX, startY);
                
                var curloc = location;
                //int nextYAxisMouseLocationPoint = randomNumberGenerator.Next(primaryScreenWorkingArea.Height, primaryScreenWorkingArea.Bottom);
                //int nextXAxisMouseLocationPoint = randomNumberGenerator.Next(left, right);
                var minValue = startY + -2;
                int deviation = 5;
                for (int j = 0; j < 10000; j++)
                {
                    int x = 1;
                    int y = 1;
                    
                    int nextXAxisMouseLocationPoint = randomNumberGenerator.Next(startY - deviation, startY + deviation);
                    int nextYAxisMouseLocationPoint = randomNumberGenerator.Next(startX -deviation , startX + deviation);
                    SimMouse.Act(SimMouse.Action.MoveOnly, nextXAxisMouseLocationPoint, nextYAxisMouseLocationPoint);
                    Thread.Sleep(1000);
                }
                
                //SimMouse.Act(SimMouse.Action.MoveOnly, nextXAxisMouseLocationPoint, nextYAxisMouseLocationPoint);
                Thread.Sleep(3000);
                Debug.WriteLine($"Loopcount: {i} ");
                //Debug.WriteLine($"x:{nextXAxisMouseLocationPoint} y:{nextYAxisMouseLocationPoint}");
            }



            //SimMouse.Act(SimMouse.Action.MoveOnly, 0, 0);
        }


        //private void MoveCursorToCornerButton_Click(object sender, RoutedEventArgs e)
        //{
        //    SimMouse.Act(SimMouse.Action.MoveOnly, 0, 0);
        //}

        private void MoveCursorNearCornerButton_Click(object sender, RoutedEventArgs e)
        {
            SimMouse.Act(SimMouse.Action.MoveOnly, 1, 1);
        }

        private void MoveCursor3_Click(object sender, RoutedEventArgs e)
        {
            SimMouse.Act(SimMouse.Action.MoveOnly, 1893, 1061);
        }

        private void MoveCursorSecondScreen_Click(object sender, RoutedEventArgs e)
        {
            // Demonstrates failure of mouse_event to position onto additional screens:
            //SimMouse.Act(SimMouse.Action.MoveOnly, 3000, 500);

            // Demonstrate Cursor.position handling additional screen positioning tactics:
            if (Screen.AllScreens.Length > 1)
            {
                var screen = Screen.AllScreens.Last();
                var x = screen.Bounds.X + screen.Bounds.Width / 2;
                var y = screen.Bounds.Y + screen.Bounds.Height / 2;
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
            }
            else
            {
                System.Windows.MessageBox.Show("You need more than one monitor to move the cursor to a secondary monitor.");
            }
        }

        private void TypeASDF_Click(object sender, RoutedEventArgs e)
        {
            this.TextEntry.Focus();
            SimKeyboard.Press((byte)'A');
            SimKeyboard.Press((byte)'S');
            SimKeyboard.Press((byte)'D');
            SimKeyboard.Press((byte)'F');
        }

        private void TypeControlA_Click(object sender, RoutedEventArgs e)
        {
            this.TextEntry.Focus();
            SimKeyboard.KeyDown(0xA2); // Left CONTROL
            SimKeyboard.KeyDown((byte)'A');
            Thread.Sleep(10);
            SimKeyboard.KeyUp(0xA2);
            SimKeyboard.KeyUp((byte)'A');
        }

        private void UpdateMousePositionText(object sender, EventArgs e)
        {
            var mousePos = System.Windows.Forms.Cursor.Position;
            this.MousePositionText.Text = string.Format("Mouse cursor: ({0},{1})", mousePos.X, mousePos.Y);
        }
    }
}
