using App.Lib.Serices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Path = System.IO.Path;


namespace Base64Coder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg;*.png)|*.jpg;*.png";

            dlg.ShowDialog();
            if (!string.IsNullOrEmpty(dlg.FileName)) 
            {
                Task.Run(() => {
                    Dispatcher.Invoke(async () => { 

                        //  Encode image
                        string res = await EncodeString(dlg.FileName);
                        res = res.Trim(' ');
                        this.txtCode.Text = res;
                        this.imgMain.Source = new BitmapImage(new Uri(dlg.FileName));


                        // Decode and save Image
                        string Extension = "";
                        Extension = dlg.FileName.EndsWith(".jpg") ? ".jpg" : ".png";
                        Bitmap bmp = await DecodeString(res);
                        string dir = Path.Combine(Directory.GetCurrentDirectory(), "img");
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        string endPath = Path.Combine(dir, Path.GetRandomFileName() + Extension);
                        
                        bmp.Save(endPath);
                    });

                    
                });
            }
        }

        private async Task<string> EncodeString(string path) 
        {
            return await Task.Run(() => { string base64string = ConvertImageService.ConvertToBase64(path);
                return base64string;
            });
        }

        private async Task<Bitmap> DecodeString(string base64) 
        {
            return await Task.Run(() => {
                return ConvertImageService.ConvertToBitmap(base64);
            });
        }
    }
}
