using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Reflection.Emit;
using System.Drawing.Imaging;
using System.Windows.Media.Converters;
using System.Drawing;
using Image = System.Drawing.Image;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Color = System.Drawing.Color;
using Color1 = System.Windows.Media.Color;
using Path = System.IO.Path;
using MessageBox = System.Windows.MessageBox;
using Haley.Services;
using Haley.Utils;
using System.Windows.Interop;
using System.Diagnostics.Eventing.Reader;
using Haley.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace image_converter_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FolderBrowserDialog _fbdInput = new FolderBrowserDialog();
        FolderBrowserDialog _fbdOutput = new FolderBrowserDialog();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSourcePath(object sender, EventArgs e)
        {


            DialogResult result = _fbdInput.ShowDialog();

            if (!string.IsNullOrWhiteSpace(_fbdInput.SelectedPath))
            {

                sourcePath.Text = _fbdInput.SelectedPath;
                string[] files = Directory.GetFiles(_fbdInput.SelectedPath);
                //path = Path.GetDirectoryName(fbd.SelectedPath);
                System.Windows.MessageBox.Show("Files found in the source folder: " + files.Length.ToString(), "Message");

            }
        }
        //string path;

        private void btnDestinationPath(object sender, EventArgs e)
        {


            DialogResult result1 = _fbdOutput.ShowDialog();
            if (!string.IsNullOrWhiteSpace(_fbdOutput.SelectedPath))
            {

                destinationPath.Text = _fbdOutput.SelectedPath;

            }
            while (sourcePath.Text.Equals(destinationPath.Text))
            {
                System.Windows.MessageBox.Show("You choosed identical path for the input and output images");
                _fbdOutput.ShowDialog();
                destinationPath.Text = _fbdOutput.SelectedPath;
                if (!(sourcePath.Text.Equals(destinationPath.Text)))
                {
                    break;
                }
            }

        }


        private Color selectedColor;
        private void clrPicker_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog oc = new ColorDialog();
            var res = oc.ShowDialog();

            if (res == System.Windows.Forms.DialogResult.OK && isPng.IsChecked == false)
            {
                selectedColor = oc.Color;
            }
            /*var newDialog = new ColorPickerDialog();
            newDialog.ShowDialog();
            selectedColor =newDialog.SelectedColor;*/




        }



        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ComboBoxItem selectedComboBoxItem = selectedResolution.SelectedItem as ComboBoxItem;

                string slctdRes = selectedComboBoxItem.Content.ToString();
                int width = int.Parse(slctdRes.Split('x')[0]);
                int height = int.Parse(slctdRes.Split('x')[1]);


                string sourceFolderPath = sourcePath.Text;
                string destinationFolderPath = destinationPath.Text;

                //string path = Path.GetDirectoryName(_fbdInput.SelectedPath);
                //Path.GetDirectoryName(FolderBrowserDialog.SelectedPath);
                if (string.IsNullOrWhiteSpace(sourceFolderPath) || string.IsNullOrWhiteSpace(destinationFolderPath))
                {
                    MessageBox.Show("Please select source and destination folders.", "Error");
                    return;
                }
                if (!(Directory.Exists(sourceFolderPath)))
                {

                    MessageBox.Show("The name of source path was changed. Choose the folder once again", "Error");

                    return;


                }
                if (!(Directory.Exists(destinationFolderPath)))
                {

                    MessageBox.Show("The name of destination path was changed. Choose the folder once again", "Error");

                    return;


                }

                string[] files = Directory.GetFiles(sourcePath.Text);
                
                //string[] readyFiles = Directory.GetFiles(destinationPath.Text);
                progressBar.Maximum = files.Length;
                progressBar.Value = 0;
                int processedFilesCount = 0;

                foreach (string file in files)
                {
                    processedFilesCount++;

                    //var oldImage = Image.FromFile(file);
                    Bitmap oldImage = new Bitmap(Image.FromFile(file));
                    //Bitmap noBg = White2Transparent(oldImage);
                    Bitmap newImage = new Bitmap(width, height);



                    using (Graphics graphics = Graphics.FromImage(newImage))
                    {
                        //var r = Convert.ToInt32(tbxRedValue);
                        bool hasAlpha = false;

                        for (int y = 0; y < oldImage.Height; y++)
                        {
                            for (int x = 0; x < oldImage.Width; x++)
                            {
                                Color pixel = oldImage.GetPixel(x, y);
                                hasAlpha = pixel.A != 255;
                                if (hasAlpha)
                                {
                                    break;
                                }
                            }
                            if (hasAlpha)
                            {
                                //graphics.Clear(Color.FromArgb(Convert.ToInt32(tbxRedValue.Text), Convert.ToInt32(tbxGreenValue.Text), Convert.ToInt32(tbxBlueValue.Text)));

                                if (isPng.IsChecked == true)
                                {
                                    //do nothing
                                }
                                else
                                {

                                    graphics.Clear(selectedColor);
                                    //this.Background = new SolidColorBrush(selectedColor);

                                }

                            }
                            else
                            {
                                graphics.Clear(Color.White);
                            }
                        }

                        if (oldImage.Height == oldImage.Width)
                        {
                            ///image is already square so no need to recalculate anything
                            graphics.DrawImage(oldImage, 0, 0, width, height);
                        }
                        else if (oldImage.Height > oldImage.Width)
                        {
                            ///image is taller, calculate the new width of the background and centre it due to it
                            var divider = Convert.ToDecimal(oldImage.Height) / Convert.ToDecimal(height);
                            var newWidth = Convert.ToInt32(Convert.ToDecimal(oldImage.Width) / divider);

                            int x = (width / 2) - (newWidth / 2);
                            int y = 0;

                            graphics.DrawImage(oldImage, x, y, newWidth, height);
                        }
                        else
                        {
                            ///image is wider, calculate the new height of the background and centre it due to it
                            var divider = Convert.ToDecimal(oldImage.Width) / Convert.ToDecimal(width);
                            var newHeight = Convert.ToInt32(Convert.ToDecimal(oldImage.Height) / divider);

                            int x = 0;
                            int y = (height / 2) - (newHeight / 2);

                            graphics.DrawImage(oldImage, x, y, width, newHeight);
                        }



                    }

                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);
                    //string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                    bool hasTransparency = false;
                    for (int y = 0; y < oldImage.Height; y++)
                    {
                        for (int x = 0; x < oldImage.Width; x++)
                        {
                            Color pixel = oldImage.GetPixel(x, y);
                            if (pixel.A != 255)
                            {
                                hasTransparency = true;
                                break;
                            }
                        }
                        if (hasTransparency)
                        {
                            break;
                        }
                    }

                    /*DirectoryInfo d = new DirectoryInfo(destinationPath.Text);
                    string[] readyFiles = Directory.GetFiles(destinationPath.Text);
                    
                    bool backgroundColorTheSame = false;

                    foreach (string readyFile in readyFiles)
                    {
                        // Load the existing image
                        using (Bitmap existingImage = new Bitmap(file))
                        {
                            backgroundColorTheSame = false;

                            if(newImage.Width>0 && newImage.Height > 0)
                            {
                                Color pixel = selectedColor;
                                Color existingPixel = existingImage.GetPixel(0, 0);
                                // Check if the pixel color is white (255, 255, 255)
                                if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255)
                                {
                                    // Skip comparison if color is white
                                    backgroundColorTheSame = false;
                                    //continue;
                                    
                                }
                                else if (pixel.R == existingPixel.R || pixel.G == existingPixel.G || pixel.B == existingPixel.B)
                                {
                                    backgroundColorTheSame = true;
                                   
                                }




                            }

                            if (!backgroundColorTheSame)
                            {
                               
                            }

                        }
                        
                    }
                    if (backgroundColorTheSame)
                    {
                        MessageBox.Show("Image the same completed.", "Message");
                        return;
                    }*/







                    /*foreach (string readyFile in readyFiles)
                    {
                        
                        for (int y = 0; y < newImage.Height; y++)
                        {
                            for (int x = 0; x < newImage.Width; x++)
                            {
                                Color pixel = newImage.GetPixel(x, y);
                                Color pixel2 = newImage.Path(x, y);
                                if (pixel.R == pixel2.R)
                                {
                                    backgroundColorTheSame = true;
                                    break;
                                } else
                                {
                                    backgroundColorTheSame = false;
                                }
                            }
                            
                        }
                    }*/




                    string newFilePath;
                    if (hasTransparency && !isPng.IsChecked == true)
                    {
                        /*if(backgroundColorTheSame) {
                            
                        }*/
                        int copyIndex = 1;
                        while (File.Exists(Path.Combine(destinationFolderPath, $"{fileName}_{width}x{height}_{copyIndex.ToString("D2")}{extension}")))
                        {
                            copyIndex++;
                        }
                        newFilePath = Path.Combine(destinationFolderPath, $"{fileName}_{width}x{height}_{copyIndex.ToString("D2")}{extension}");


                    }

                    else
                    {

                        newFilePath = Path.Combine(destinationFolderPath, $"{fileName}_{width}x{height}{extension}");

                    }

                    newImage.Save(newFilePath);

                    oldImage.Dispose();
                    newImage.Dispose();
                    progressBar.Value = processedFilesCount;
                    await Task.Delay(70);
                    //progressBar.PerformStep();

                }

                MessageBox.Show("Image conversion completed.", "Message");
                progressBar.Value = 0;

            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error");
            }

        }











        /*public static Bitmap White2Transparent(Image img)
        {
            int w = img.Width;
            int h = img.Height;

            Bitmap bitmap = new Bitmap(w, h);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(img, 0, 0, w, h);
            }

            BitmapData bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int bytesPerPixel = 4;
            int stride = bitmapData.Stride;
            IntPtr scan0 = bitmapData.Scan0;

            unsafe
            {
                byte* pixel = (byte*)(void*)scan0;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int offset = y * stride + x * bytesPerPixel;

                        byte r = pixel[offset + 2];
                        byte g = pixel[offset + 1];
                        byte b = pixel[offset];


                        byte tolerance = 12;
                        if (r >= 255 - tolerance && g >= 255 - tolerance && b >= 255 - tolerance)
                        {
                            pixel[offset + 3] = 0;
                        }
                    }
                }
            }

            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }*/

        private void isPng_Checked(object sender, RoutedEventArgs e)
        {
            clrPicker.Visibility = Visibility.Hidden;

        }

        private void isPng_Unchecked(object sender, RoutedEventArgs e)
        {
            clrPicker.Visibility = Visibility.Visible;
        }

        private void selectedResolution_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}