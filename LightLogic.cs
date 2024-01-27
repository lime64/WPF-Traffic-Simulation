using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Movement
{
    public class LightLogic
    {
        public enum TLColor
        {
            Green = 0,
            Orange,
            Red,
        }

        /// <summary>
        /// 2Dimensional array containing the TLColor enum,
        /// Think of the lights and their states.
        /// </summary>
        public static TLColor[,] StateArray =
        { 
            { TLColor.Green, TLColor.Green, TLColor.Red, TLColor.Red },
            { TLColor.Orange, TLColor.Orange, TLColor.Red, TLColor.Red },
            { TLColor.Red, TLColor.Red, TLColor.Green, TLColor.Green },
            { TLColor.Red, TLColor.Red, TLColor.Orange, TLColor.Orange }
        };

        public static void SetColors(int currentTick,params Lights[] currentLight)
        { 
            for (int currentArrayIndex = 0; currentArrayIndex < 4; currentArrayIndex++)
            {
                switch (StateArray[currentTick, currentArrayIndex])
                {
                    case TLColor.Green:
                        currentLight[currentArrayIndex].GreenLight.Background = Brushes.White;
                        currentLight[currentArrayIndex].OrangeLight.Background = Brushes.Orange;
                        currentLight[currentArrayIndex].RedLight.Background = Brushes.Red;
                        break;
                    case TLColor.Orange:
                        currentLight[currentArrayIndex].GreenLight.Background = Brushes.Green;
                        currentLight[currentArrayIndex].OrangeLight.Background = Brushes.White;
                        currentLight[currentArrayIndex].RedLight.Background = Brushes.Red;
                        break;
                    case TLColor.Red:
                        currentLight[currentArrayIndex].GreenLight.Background = Brushes.Green;
                        currentLight[currentArrayIndex].OrangeLight.Background = Brushes.Orange;
                        currentLight[currentArrayIndex].RedLight.Background = Brushes.White;
                        break;
                        
                }
            }
        }
    }
}
