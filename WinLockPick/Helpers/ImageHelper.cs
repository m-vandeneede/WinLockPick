using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Win32;

namespace WinLockPick.Helpers
{
    class ImageHelper
    {
        public static Bitmap GetLockscreenWallpaper()
        {
            string spotlightPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\localstate\assets");
            string defaultWinPath = @"C:\Windows\Web\Wallpaper\Windows\img0.jpg";

            if (Directory.Exists(spotlightPath) && Directory.GetFiles(spotlightPath).Any())
                return new Bitmap(new DirectoryInfo(spotlightPath).GetFiles().OrderByDescending(f => f.CreationTime).ToArray()[0].FullName);  
            else return new Bitmap(defaultWinPath);
        }
        public static Bitmap GetAccountImage()
        {
            //Special thanks to Keldnorman for this snippet

            RegistryKey AccountPictureReg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\AccountPicture", true);
            string AccountPictureFilename = AccountPictureReg.GetValue("SourceId").ToString();
            AccountPictureReg.Close();
            var accPicture = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft\\Windows\\AccountPictures\\" + AccountPictureFilename + ".accountpicture-ms");

            return new Bitmap(accPicture);
        }
        private static Bitmap GetImage448(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            long position = Seek(fs, "JFIF", 100);
            byte[] b = new byte[Convert.ToInt32(fs.Length)];
            fs.Seek(position - 6, SeekOrigin.Begin);
            fs.Read(b, 0, b.Length);
            fs.Close();
            fs.Dispose();
            return GetBitmapImage(b);
        }
        private static long Seek(FileStream fs, string searchString, int startIndex)
        {
            char[] search = searchString.ToCharArray();
            long result = -1, position = 0, stored = startIndex,
            begin = fs.Position;
            int c;
            while ((c = fs.ReadByte()) != -1)
            {
                if ((char)c == search[position])
                {
                    if (stored == -1 && position > 0 && (char)c == search[0])
                    {
                        stored = fs.Position;
                    }
                    if (position + 1 == search.Length)
                    {
                        result = fs.Position - search.Length;
                        fs.Position = result;
                        break;
                    }
                    position++;
                }
                else if (stored > -1)
                {
                    fs.Position = stored + 1;
                    position = 1;
                    stored = -1;
                }
                else
                {
                    position = 0;
                }
            }

            if (result == -1)
            {
                fs.Position = begin;
            }
            return result;
        }

}
}
