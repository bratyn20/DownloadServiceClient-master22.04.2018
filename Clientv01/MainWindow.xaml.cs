﻿using System;
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
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Clientv01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            


            //  MessageBox.Show(result[0].ToString());
         //   MessageBox.Show(filename[0]);
            //string filename = Directory.GetFiles(@appPath + "Update").Select(x => System.IO.Path.GetFileNameWithoutExtension(x));
            //MessageBox.Show(Y);

            // CurrentProject.GetDirrectory();
            //  MessageBox.Show(appPath + "Update");

            // string[] files1 = Directory.GetFiles(@"c:\0001");

            //MessageBox.Show(files1[0]);

            var ServiceUpdate = new Service.ServiceUpdateClient("BasicHttpBinding_IServiceUpdate");
            

            try
            {
                String[] list = ServiceUpdate.getFileinfo();




                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                Directory.CreateDirectory(appPath + "Update");
                //string[] filename = Directory.GetFiles(appPath + "Update");

                List<string> result = new List<string>();
                for (int i = 0; i < list.Length; i++)
                {
                    result.Add(System.IO.Path.GetFileName(list[i]));
                }


                txtblock.Text = "";

                for (int i = 0; i < list.Length; i++)
                {
                    // MessageBox.Show(list[i]);
                    // int l = list[0].Length;

                    txtblock.Text += "-->>" + list[i] + "\n";
                    //  MessageBox.Show(list[i].Substring(0, 3) + "0001" + list[i].Substring(7, list[i].Length - 7 ));
                }



                for (int i = 0; i < list.Length; i++)
                {
                    int lenght = ServiceUpdate.LenghtFile(list[i]);
                    Stream file = ServiceUpdate.getFile(list[i]);
                    byte[] bytes_file_w = new byte[lenght];

                    var offset = 0;
                    var actualRead = 0;

                    do
                    {
                        actualRead = file.Read(bytes_file_w, offset, lenght - offset);
                        offset += actualRead;
                    } while (actualRead > 0);


                    //string path = @list[i].Substring(0, 3) + "0001" + list[i].Substring(7, list[i].Length - 7);
                    //string path = @"c:\0001\test.txt";
                    string path = @appPath + "Update\\" + result[i];
                    FileStream file_w = File.Open(path, FileMode.Create);
                    file_w.Write(bytes_file_w, 0, bytes_file_w.Length);

                    // File.WriteAllBytes(@"c:\\MyDownloadedBooks\\" + "test.txt", bytes_file_w);
                    file_w.Close();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Сервис не отвечает:");
                txtblock.Text = ex.Message;
            }
            ServiceUpdate.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            Directory.CreateDirectory(appPath + "Update");
            string[] filename = Directory.GetFiles(appPath + "Update");
            //var dir1 = new DirectoryInfo(@"C:\Folder1\F1");
            var dir2 = new DirectoryInfo(@"C:\f2");

            try
            {
                foreach (Process proc in Process.GetProcessesByName("GeneralCLIENT"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //System.IO.Directory.Move(dir1.FullName, dir2.FullName);

            //   var enumerateDir2 = dir2.EnumerateFileSystemInfos().ToList();

            // var directories = enumerateDir2.OfType<DirectoryInfo>().ToList();
            // var files = enumerateDir2.OfType<FileInfo>().ToList();
            //System.IO.Directory.GetFileSystemEntries()

            /*  for(int i=0; i<files.Count; i++)
               {
                   File.Copy(files[i].FullName, @"C:\f10\\" + files[i].Name);
               }*/

            // string way1 = dir2.FullName;
            string way1 = @appPath + "Update";
            string way2 = @"C:\f10";

       //    for(int i = 0; i < directories.Count; i++)
         //   {
                // Directory(directories[i].FullName, @"C:\f10\\" + directories[i].Name);
                CopyDirectory.CopyDir(way1,way2);
          //  }
        }

      /*  void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + System.IO.Path.GetFileName(s1);
                File.Copy(s1, s2 ,true);
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                CopyDir(s, ToDir + "\\" + System.IO.Path.GetFileName(s));
            }
        }*/

    }
}
