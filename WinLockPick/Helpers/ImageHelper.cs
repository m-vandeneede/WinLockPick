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
            string spotlightPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\localstate\assets"); // <- Unreliable
            var defaultWinPath = new DirectoryInfo(@"C:\Windows\Web\Screen");

            if(!Directory.Exists(defaultWinPath.FullName)) return new Bitmap(@"C:\Windows\Web\Wallpaper\Windows\img0.jpg");

            //if (Directory.Exists(spotlightPath) && Directory.GetFiles(spotlightPath).Any())
            //    return new Bitmap(new DirectoryInfo(spotlightPath).GetFiles().OrderByDescending(f => f.CreationTime).ToArray()[0].FullName);
            //else return new Bitmap(defaultWinPath);

            var rndImg = new Random().Next(0, defaultWinPath.GetFiles().ToArray().Length - 1);
            return new Bitmap(defaultWinPath.GetFiles()[rndImg].FullName);
        }
        public static Bitmap GetAccountImage()
        {
            //Special thanks to Keldnorman for this snippet

            RegistryKey AccountPictureReg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\AccountPicture", true);
            string AccountPictureFilename = AccountPictureReg.GetValue("SourceId").ToString();
            AccountPictureReg.Close();
            var accPicture = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft\\Windows\\AccountPictures\\" + AccountPictureFilename + ".accountpicture-ms");

            return AccountImageHelper.GetImage(accPicture);
        }
    }
}
