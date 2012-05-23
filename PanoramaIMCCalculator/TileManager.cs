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

        private void saveImage()
        {
            string filename = "Images/app.jpg";
            StreamResourceInfo sr = Application.GetResourceStream(new Uri(filename, UriKind.Relative));


            BitmapImage bmp = new BitmapImage();


            bmp.SetSource(sr.Stream);
            WriteableBitmap wb = new WriteableBitmap(bmp);

            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                myIsolatedStorage.CreateDirectory("Images");

                if (myIsolatedStorage.FileExists(filename))
                {
                    myIsolatedStorage.DeleteFile(filename);
                }

                //using (IsolatedStorageFileStream fileStream =  new IsolatedStorageFileStream(filename, FileMode.Create, myIsolatedStorage)
                //{ 

                IsolatedStorageFileStream filestream = myIsolatedStorage.CreateFile(filename);

                //WriteableBitmap wb = new WriteableBitmap(bitmap);
                Extensions.SaveJpeg(wb, filestream, wb.PixelWidth, wb.PixelHeight, 0, 100);
                filestream.Close();
                // MessageBox.Show("saved suceessfully");

                //BitmapImage bitmap = new BitmapImage();
                //    bitmap.SetSource(imageStream);
                //////}

                //wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, 0, 85);
                //fileStream.Close();
            }
        }


        private void readImage()
        {
            byte[] data;



            // Read the entire image in one go into a byte array

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Open the file - error handling omitted for brevity
                // Note: If the image does not exist in isolated storage the following exception will be generated:
                // System.IO.IsolatedStorage.IsolatedStorageException was unhandled 
                // Message=Operation not permitted on IsolatedStorageFileStream 
                using (IsolatedStorageFileStream isfs = isf.OpenFile("/Images/app.jpg", FileMode.Open, FileAccess.Read))
                {
                    // Allocate an array large enough for the entire file
                    data = new byte[isfs.Length];

                    // Read the entire file and then close it
                    isfs.Read(data, 0, data.Length);
                    isfs.Close();
                }
            }

            // Create memory stream and bitmap
            MemoryStream ms = new MemoryStream(data);
            BitmapImage bi = new BitmapImage();

            // Set bitmap source to memory stream
            bi.SetSource(ms);

            // Create an image UI element – Note: this could be declared in the XAML instead
            Image image = new Image();
            image.Source = bi;
        }


        private void createImageForBackTile()
        {
            try
            {
                BitmapImage logo = new BitmapImage(new Uri("/Resources/Images/thumbs-up.png", UriKind.Absolute));

                WriteableBitmap bmp = new WriteableBitmap(logo);

                bmp.Invalidate();

                var isf = IsolatedStorageFile.GetUserStoreForApplication();
                var filename = "/Shared/ShellContent/tile.jpg";
                // ca
                using (var st = new IsolatedStorageFileStream(filename, FileMode.Create, FileAccess.Write, isf))
                {
                    bmp.SaveJpeg(st, 173, 173, 0, 100);
                }

                // ou ca 
                using (var st = isf.CreateFile("/Shared/ShellContent/tile.jpg"))
                {
                    bmp.SaveJpeg(st, 173, 173, 0, 97);
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
            string backTitle = "";
            string backBackgroundImageName = "";
            if (imc < 18.5)
            {
                backTitle = imcManager.weightToGainFromIMC(imc, currentHeight, currentWeight);
                backBackgroundImageName = "thumbs-down.png";
            }
            if (imc > 25)
            {
                backTitle = imcManager.weightToLoseFromIMC(imc, currentHeight, currentWeight);
                backBackgroundImageName = "thumbs-down.png";
            }
            if (18.5 <= imc && imc <= 25)
            {
                backTitle = "Ne changez rien.";
                backBackgroundImageName = "thumbs-up.png";
            }

            // Application Tile is always the first Tile, even if it is not pinned to Start.
            ShellTile TileToFind = ShellTile.ActiveTiles.First();

            // Application should always be found
            if (TileToFind != null)
            {
                createImageForBackTile();

                // Set the properties to update for the Application Tile.
                // Empty strings for the text values and URIs will result in the property being cleared.
                StandardTileData NewTileData = new StandardTileData
                {
                    // Title = "Mon title",
                    // BackgroundImage = new Uri(textBoxBackgroundImage.Text, UriKind.Relative),
                    Count = Convert.ToInt32(imc),
                    BackTitle = backTitle,
                    BackBackgroundImage = new Uri("isostore:/Shared/ShellContent/tile.jpg", UriKind.Absolute),
                    BackContent = backContent
                };

                // Update the Application Tile
                TileToFind.Update(NewTileData);
            }

        }
    }

}
