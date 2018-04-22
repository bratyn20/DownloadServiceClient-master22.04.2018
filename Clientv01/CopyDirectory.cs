using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientv01
{
    class CopyDirectory
    {
       public static void CopyDir(string FromDir, string ToDir) //На вход методу подаётся путь откуда копируем и куда копируем
        {
            Directory.CreateDirectory(ToDir);
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + System.IO.Path.GetFileName(s1);
                File.Copy(s1, s2, true);
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                CopyDir(s, ToDir + "\\" + System.IO.Path.GetFileName(s));
            }
        }
    }
}
