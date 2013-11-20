﻿using System;
using System.Windows;
using Microsoft.Phone.Shell;
using System.Linq;
using PanoramaIMCCalculator.Managers;
using System.IO.IsolatedStorage;
using System.IO;
using PanoramaIMCCalculator.Resources.AppResources;

namespace PanoramaIMCCalculator.Managers
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
            if (16.5 <= imc && imc < 18.5) // Maigreur
            {
                backTitle = imcManager.weightToGainFromIMC(imc, currentHeight, currentWeight);
            }
            if (25 < imc && imc <= 30) // Surpoids
            {
                backTitle = imcManager.weightToLoseFromIMC(imc, currentHeight, currentWeight);
            }
            if (30 < imc) // Obésité et Obésité sévère
            {
                backTitle = imcManager.weightToLoseFromIMC(imc, currentHeight, currentWeight);
            }
            if (18.5 <= imc && imc <= 25) // Zone normale
            {
                backTitle = AppResources.LiveTileBackTitleNormal;
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
            if (16.5 <= imc && imc < 18.5) // Maigreur
            {
                backBackgroundImageName = "BackTileMaigreur";
            }
            if (25 < imc && imc <= 30) // Surpoids
            {
                backBackgroundImageName = "BackTileSurpoids";
            }
            if (imc > 30) // Obésité modérée et sévère
            {
                backBackgroundImageName = "BackTileObese";
            }
            if (18.5 <= imc && imc <= 25) // Zone normale
            {
                backBackgroundImageName = "BackTileNormal";
            }

            // On ajoute l'extension en fonction du genre de la personne.
            if (App.isMale)
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
