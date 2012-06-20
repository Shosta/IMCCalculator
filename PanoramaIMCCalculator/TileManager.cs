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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Linq;
using PanoramaIMCCalculator.Model;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Resources;

namespace PanoramaIMCCalculator
{
    public class TileManager
    {
        private string backTitleForIMC(double imc, double currentHeight, double currentWeight)
        {
            IMCCalculatorManager imcManager = new IMCCalculatorManager();
            
            string backTitle = "";
            if (imc < 16.5) // Dénutrition
            {
                backTitle = imcManager.weightToGainFromIMC(imc, currentHeight, currentWeight);
}
            if (imc < 18.5 && imc > 16.5) // Maigreur
            {
                backTitle = imcManager.weightToGainFromIMC(imc, currentHeight, currentWeight);
            }
            if (imc > 25 && imc < 30) // Obésité modérée
            {
                backTitle = imcManager.weightToLoseFromIMC(imc, currentHeight, currentWeight);
            }
            if (imc > 30) // Obésité sévère
            {
                backTitle = imcManager.weightToLoseFromIMC(imc, currentHeight, currentWeight);
             }
            if (18.5 <= imc && imc <= 25) // Zone normale
            {
                backTitle = "Ne changez rien.";
             }

            return backTitle;
        }

        private string imageFileNameForIMC(double imc)
        {
            string backBackgroundImageName = "";
            // On recupere le nom de la ressource en fonction de l'imc.
            if (imc < 16.5) // Dénutrition
            {
                 backBackgroundImageName = "BackTileDenutrition";
            }
            if (imc < 18.5 && imc > 16.5) // Maigreur
            {
                backBackgroundImageName = "BackTileMaigreur";
            }
            if (imc > 25 && imc < 30) // Obésité modérée
            {
                backBackgroundImageName = "BackTileSurpoids";
            }
            if (imc > 30) // Obésité sévère
            {
                backBackgroundImageName = "BackTileObese";
            }
            if (18.5 <= imc && imc <= 25) // Zone normale
            {
                backBackgroundImageName = "BackTileNormal";
            }

            // On ajoute l'extension en fonction du genre de la personne.
            if (MainPage.isMale)
            {
                backBackgroundImageName = backBackgroundImageName + ".png";
            }
            else
            {
                backBackgroundImageName = backBackgroundImageName + "Femme.png";
            }

            return backBackgroundImageName;
        }

        private void createImageForBackTile(string backBackgroundImageName)
        {
            try
            {
                var stream = Application.GetResourceStream(new Uri("Resources/Images/" + backBackgroundImageName, UriKind.Relative));

                var isf = IsolatedStorageFile.GetUserStoreForApplication();
                const string filename = "/Shared/ShellContent/tile.png";

                using (var st = isf.OpenFile(filename, FileMode.Create, FileAccess.Write))
                {
                    stream.Stream.Position = 0;
                    stream.Stream.CopyTo(st);
                    st.Flush();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }


        /// <summary>
        /// Manage the Tile. Change its count, backTitle and backContent.
        /// </summary>
        /// <param name="count">The tile count</param>
        /// <param name="backTitle">The tile backTitle</param>
        /// <param name="backContent">The tile backContent</param>
        public void changeBackTile(double imc, double currentHeight, double currentWeight, string backContent)
        {
            IMCCalculatorManager imcManager = new IMCCalculatorManager();
            string backTitle = backTitleForIMC(imc, currentHeight, currentWeight);
            string backBackgroundImageName = imageFileNameForIMC(imc);
           

            // Application Tile is always the first Tile, even if it is not pinned to Start.
            ShellTile TileToFind = ShellTile.ActiveTiles.First();

            // Application should always be found
            if (TileToFind != null)
            {
                createImageForBackTile(backBackgroundImageName);

                // Set the properties to update for the Application Tile.
                // Empty strings for the text values and URIs will result in the property being cleared.
                StandardTileData NewTileData = new StandardTileData
                {
                    // Title = "Mon title",
                    // BackgroundImage = new Uri(textBoxBackgroundImage.Text, UriKind.Relative),
                    Count = Convert.ToInt32(imc),
                    BackTitle = backTitle,
                    BackBackgroundImage = new Uri("isostore:/Shared/ShellContent/tile.png", UriKind.Absolute),
                    BackContent = backContent
                };

                // Update the Application Tile
                TileToFind.Update(NewTileData);
            }

        }
    }

}
