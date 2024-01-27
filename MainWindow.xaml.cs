using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using static Movement.Lights;


namespace Movement
{
    public partial class MainWindow : Window
    {
        //Creating our global timer
        DispatcherTimer carTimer = new DispatcherTimer();
        DispatcherTimer lightTimer = new DispatcherTimer();

        //Creating car objects (our prefered UIElements) with their respective parameters
        Car north = new Car("north", Car.Orientation.Vertical, Brushes.Green);
        Car south = new Car("south", Car.Orientation.Vertical, Brushes.Red);
        Car east = new Car("east", Car.Orientation.Horizontal, Brushes.Yellow);
        Car west = new Car("west", Car.Orientation.Horizontal, Brushes.Blue);
        
        //Speed to move UIElements at
        int speedY = 2;
        int speedX = 2;

        //Tick, used for my lightArray
        static int Tick;

        //Change the timings here
        static TimeSpan carTimeSpan = TimeSpan.FromMilliseconds(4);
        static TimeSpan lightTimeSpan = TimeSpan.FromSeconds(3);

        //Declaration of constant coordinates
        //Right before center
        const double minYPos = 110;
        //Right after center
        const double maxYPos = 230;
        //Right before center
        const double minXPos = 300;
        //Right after center
        const double maxXPos = 435;
        
        
        //DEBUGGER
        //Extra rendering to show on debugger
        private int frameCount = 0;
        private double fps = 0;
        private Stopwatch stopwatch = new Stopwatch();

        public bool isButtonClicked = false;


        public MainWindow()
        {
            InitializeComponent();
            stopwatch.Start();


            //Adding our objects to the UI, position is undefined
            UIMovement.ImplementCars(View,north, south, west, east);

            //Defining/Spawning objects position by .SetElement(UIElement,x,y)
            resetYPositions();
            resetXPositions();

            //Invoking our carTimer_tick event
            carTimer.Tick += carTimer_tick;
            carTimer.Interval = carTimeSpan;
            carTimer.Start();

            //Invoking our lightTimer_tick event
            lightTimer.Tick += lightTimer_tick;
            lightTimer.Interval = lightTimeSpan;
            lightTimer.Start();

            // Hook into the CompositionTarget.Rendering event to count frames
            CompositionTarget.Rendering += CompositionTarget_Rendering;

            DispatcherTimer fpsTimer = new DispatcherTimer();
            fpsTimer.Interval = TimeSpan.FromSeconds(1);
            fpsTimer.Tick += UpdateFPS;
            fpsTimer.Start();
        }


        //CAR
        private void carTimer_tick(object? sender, EventArgs e)
        {
            speedY = 2;
            speedX = 2;
            double YPos = Canvas.GetTop(north);
            double XPos = Canvas.GetLeft(west);

            if (northLight.RedLight.Background == Brushes.White && !isYcrossing(north))
            {
                UIMovement.Move(north, View, speedY, UIMovement.Direction.Down);
                UIMovement.Move(south, View, speedY, UIMovement.Direction.Up);
            }
            else if (northLight.GreenLight.Background == Brushes.White)
            {
                UIMovement.Move(north, View, speedY, UIMovement.Direction.Down);
                UIMovement.Move(south, View, speedY, UIMovement.Direction.Up);
            }
            else if (northLight.OrangeLight.Background == Brushes.White && isYcrossing(north))
            {
                speedY = 4;
                UIMovement.Move(north, View, speedY, UIMovement.Direction.Down);
                UIMovement.Move(south, View, speedY, UIMovement.Direction.Up);
            }
            else if (northLight.OrangeLight.Background == Brushes.White && !isYcrossing(north))
            {
                //this condition barely gets met but it's there to be safe and never have cars stuck in the middle
                speedY = 1;
                UIMovement.Move(north, View, speedY, UIMovement.Direction.Down);
                UIMovement.Move(south, View, speedY, UIMovement.Direction.Up);
            }


            if (!isXcrossing(west))
            {
                UIMovement.Move(west, View, speedX, UIMovement.Direction.Right);
                UIMovement.Move(east, View, speedX, UIMovement.Direction.Left);
            }
            else if (westLight.GreenLight.Background == Brushes.White)
            {
                UIMovement.Move(west, View, speedX, UIMovement.Direction.Right);
                UIMovement.Move(east, View, speedX, UIMovement.Direction.Left);
            }
            else if (westLight.OrangeLight.Background == Brushes.White && isXcrossing(west))
            {
                speedX = 4;
                UIMovement.Move(west, View, speedX, UIMovement.Direction.Right);
                UIMovement.Move(east, View, speedX, UIMovement.Direction.Left);
            }
            else if (westLight.OrangeLight.Background == Brushes.White && !isXcrossing(west))
            {
                speedX = 1;
                UIMovement.Move(west, View, speedX, UIMovement.Direction.Right);
                UIMovement.Move(east, View, speedX, UIMovement.Direction.Left);
            }

            //Above 450 is considered offscreen
            if (YPos > 450)
            {
                resetYPositions();
            }
            //Above 800 is considered offscreen
            if (XPos > 800)
            { 
                resetXPositions();
            }

            UpdateDebugger();
        }


        //LIGHTS
        private void lightTimer_tick(object? sender, EventArgs e)
        {
            //This seperate timer give me more control over the Array loop
            Lights[] lightsArray = { northLight, southLight, westLight, EastLight };

            //We loop the 3 cases each tick event of the timer
            if (Tick >= 4)
            {
                Tick = 0;
            }
            LightLogic.SetColors(Tick, lightsArray);
            ++Tick;
        }

        private void CompositionTarget_Rendering(object ?sender, EventArgs e)
        {
            // Incrementing each frame
            frameCount++;
        }

        private void UpdateFPS(object ?sender, EventArgs e)
        {
            // Calculate FPS and update the TextBlock
            fps = frameCount / stopwatch.Elapsed.TotalSeconds;
            

            // Reset frame count and restart the stopwatch
            frameCount = 0;
            stopwatch.Restart();
        }

        //Check if our crossroad is being crossed
        private bool isYcrossing(Car car)
        {
            if(Canvas.GetTop(car) < minYPos || Canvas.GetTop(car) > maxYPos)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isXcrossing(Car car)
        {
            if (Canvas.GetLeft(car) < minXPos || Canvas.GetLeft(car) > maxXPos)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //Method used for "respawning" the cars
        private void resetYPositions()
        {
            UIMovement.SetElement(north, -250, 370);
            UIMovement.SetElement(south, 615, 400);
        }
        private void resetXPositions()
        {
            UIMovement.SetElement(west, 212, -250);
            UIMovement.SetElement(east, 185, 985);
        }

        //Shows some information in the Info TextBlock
        private void UpdateDebugger()
        {
                Info.Text =
               $"FPS: {fps:F2} \n" +
               $"Northen car Y position = {Canvas.GetTop(north)}\n" +
               $"Western car X position = {Canvas.GetLeft(west)}\n" +
               $"Vertical Car Speed = {speedY}\n" +
               $"Horizontal Car Speed = {speedX}\n" +
               $"Are North-South crossing? = {isYcrossing(north)}\n" +
               $"Are West-East crossing? = {isXcrossing(west)}\n" +
               $"Current Tick of TLColor[,] StateArray = {Tick}\n";
        }

        private void Debugbtn_Click(object sender, RoutedEventArgs e)
        {
            Info.Visibility = (Info.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;

            if (Info.Visibility == Visibility.Visible)
            {
                Debugbtn.Content = "Hide debug info";
            }
            else
            {
                Debugbtn.Content = "Show debug info";
            }
        }
    }
}