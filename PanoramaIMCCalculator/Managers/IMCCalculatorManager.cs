using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using PanoramaIMCCalculator.Resources.AppResources;

namespace PanoramaIMCCalculator.Managers
{
    public class IMCCalculatorManager
    {

        // IMC calculation.
        /// <summary>
        /// Calculate an IMC for metric height and weight value
        /// </summary>
        /// <param name="height">the user height in cm</param>
        /// <param name="weight"> the user weight in kg</param>
        /// <returns></returns>
        public double calculateMetricIMC(double height, double weight)
        {
            double imc = 0.0;
            imc = (weight * 10000) / (height * height); // Multiply by 10000 to handle that the height is given in cm and not in m.

            return imc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentImc"></param>
        /// <param name="currentHeight"></param>
        /// <param name="currentWeight"></param>
        /// <returns></returns>
        public string weightToGainFromIMC(double currentImc, double currentHeight, double currentWeight)
        {
            double desiredIMC = 18.5;
            double desiredWeight = desiredIMC * (currentHeight / 100) * (currentHeight / 100);

            double kiloToGain = desiredWeight - currentWeight;

            return AppResources.WeightToGainFirstPart + " " + Math.Round(kiloToGain, 2) + AppResources.WeightToGainLastPart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentImc"></param>
        /// <param name="currentHeight"></param>
        /// <param name="currentWeight"></param>
        /// <returns></returns>
        public string weightToLoseFromIMC(double currentImc, double currentHeight, double currentWeight)
        {
            double desiredIMC = 25;
            double desiredWeight = desiredIMC * (currentHeight / 100) * (currentHeight / 100);

            double kiloToLose = currentWeight - desiredWeight;

            return AppResources.WeightToLoseFirstPart + " " + Math.Round(kiloToLose, 2) + AppResources.WeightToLoseLastPart;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imc"></param>
        /// <returns></returns>
        public string infoFromIMC(double imc)
        {
            string result = AppResources.InfoFromNormalIMC;
            if (imc > 35)
            {
                result = AppResources.InfoFromObesiteSevereIMC;
            }
            else if (30 < imc && imc <= 35)
            {
                result = AppResources.InfoFromObesiteIMC;
            }
            else if (25 < imc && imc <= 30)
            {
                result = AppResources.InfoFromSurpoidsIMC;
            }
            else if (16.5 <= imc && imc < 18.5)
            {
                result = AppResources.InfoFromMaigreurIMC;
            }
            else if (imc < 16.5)
            {
                result = AppResources.InfoFromDenutritionIMC;
            }

            return result;
        }

    }
}
