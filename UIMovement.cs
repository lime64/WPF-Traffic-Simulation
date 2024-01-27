using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Movement
{
    /// <summary>
    /// This class contains Methods to Move and Place the UIElements to a specific point, using the Canvas.
    /// </summary>
    public class UIMovement
    {
        /// <summary>
        /// A set of directions
        /// </summary>
        public enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }

        /// <summary>
        /// Add your UIElement, what canvas to add it on, the movement speed and the direction.
        /// </summary>
        /// <param name="uIElement"></param>
        /// <param name="canvas"></param>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public static void Move(UIElement uIElement, Canvas canvas, int speed, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Up(uIElement, canvas, speed);
                    break;

                case Direction.Down:
                    Down(uIElement, canvas, speed);
                    break;

                case Direction.Left:
                    Left(uIElement, canvas, speed);
                    break;

                case Direction.Right:
                    Right(uIElement, canvas, speed);
                    break;
            }
        }

        private static void Up(UIElement uIElement, Canvas canvas, int speed)
        {
            Canvas.SetTop(uIElement, Canvas.GetTop(uIElement) - speed);
        }

        private static void Down(UIElement uIElement, Canvas canvas, int speed)
        {
            Canvas.SetTop(uIElement, Canvas.GetTop(uIElement) + speed);
        }

        private static void Left(UIElement uIElement, Canvas canvas, int speed)
        {
            Canvas.SetLeft(uIElement, Canvas.GetLeft(uIElement) - speed);
        }

        private static void Right(UIElement uIElement, Canvas canvas, int speed)
        {
            Canvas.SetLeft(uIElement, Canvas.GetLeft(uIElement) + speed);
        }


        /// <summary>
        /// place a UIElement on the screen at .SetTop(x), .SetLeft(y),
        /// Only available for canvas-type elements
        /// </summary>
        /// <param name="uIElement"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetElement(UIElement uIElement, int x, int y)
        {
            Canvas.SetTop(uIElement, x);
            Canvas.SetLeft(uIElement, y); 
        }

        /// <summary>
        /// ImplementUI() for an array of car objects.
        /// </summary>
        /// <param name="cars"></param>
        public static void ImplementCars(Canvas canvas,params Car[] cars)
        {
            //Debug.WriteLine($"showing on {canvas.Name}");

            foreach (Car car in cars)
            {
                ImplementUI(car, canvas);
            }
        }

        /// <summary>
        /// Checks if object is there, if it isn't add it to the UIElement.
        /// </summary>
        /// <param name="uIElement"></param>
        public static void ImplementUI(UIElement uIElement, Canvas canvas)
        {
            if (uIElement != null)
            {
                if (!canvas.Children.Contains(uIElement))
                {
                    canvas.Children.Add(uIElement);
                }
            }
        }
    }
}
