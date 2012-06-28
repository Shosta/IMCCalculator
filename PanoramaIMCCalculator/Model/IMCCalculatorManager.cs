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

namespace PanoramaIMCCalculator.Model
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

            return "Gagnez " + Math.Round(kiloToGain, 2) + " kilos.";
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

            return "Perdez " + Math.Round(kiloToLose, 2) + " kilos.";
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imc"></param>
        /// <returns></returns>
        public string infoFromIMC(double imc)
        {
            string result = "Vous êtes dans la zone normale.";
            if (imc > 35)
            {
                result = "Vous êtes dans la zone d'obésité sévère.";
            }
            else if (30 < imc && imc <= 35)
            {
                result = "Vous êtes dans la zone d'obésité modérée.";
            }
            else if (25 < imc && imc <= 30)
            {
                result = "Vous êtes dans la zone de surpoids.";
            }
            else if (16.5 <= imc && imc < 18.5)
            {
                result = "Vous êtes dans la zone de maigreur.";
            }
            else if (imc < 16.5)
            {
                result = "Vous êtes dans la zone de dénutrition.";
            }

            return result;
        }

    }
}
