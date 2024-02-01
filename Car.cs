using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace Movement
{
    public class Car : Canvas
    {
        /// <summary>
        /// A set of orientations
        /// </summary>
        public enum Orientation
        {
            Horizontal,
            Vertical
        }

        /// <summary>
        /// Add the name of your object, the direction it will face and optionally a color.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orientation"></param>
        /// <param name="fill"></param>
        public Car(string name, Orientation orientation, Brush? color = null)
        {
            this.Name = name;
            this.Background = color;

            if (orientation == Orientation.Horizontal)
            {
                this.Width = 50;
                this.Height = 15;
            }
            else
            {
                this.Width = 15;
                this.Height = 50;
            }
        }
    }
}
