using System;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace WpfApp2
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (K_size.Text != "" && Fingerprint.Text != "" && FPK.Text != "" && PPK.Text != "" && Loop_num.Text != "" && Final_Folder.Text != "" && Temp_Folder.Text != "" && Temp_Folder_2.Text != "" && Memory.Text != "" && Thread.Text != "" && Bucket.Text != "")
            {
                DateTime dt = DateTime.Now;
                String dt_str = dt.Year + "年" + dt.Month + "月" + dt.Day + "日" + dt.Hour + "點" + dt.Minute + "分" + dt.Second + "秒";
                var enviromentPath = System.Environment.GetEnvironmentVariable("LocalAppdata");
                foreach (string fname in System.IO.Directory.GetFileSystemEntries(enviromentPath + @"\chia-blockchain"))
                {
                    if (fname.Contains("app"))
                    {
                        StreamWriter sw = new StreamWriter(fname + @"\resources\app.asar.unpacked\daemon\plot" + dt_str + ".bat");

                        sw.WriteLine("chia plots create -k {0} -a {1} -f {2} -p {3} -n {4} -d {5} -t {6} -2 {7} -b {8} -r {9} -u {10}", K_size.Text, Fingerprint.Text, FPK.Text, PPK.Text, Loop_num.Text, Final_Folder.Text, Temp_Folder.Text, Temp_Folder_2.Text, Memory.Text, Thread.Text, Bucket.Text);
                        sw.WriteLine("pause");
                        sw.Close();
                        K_size.Text = "";
                        Fingerprint.Text = "";
                        FPK.Text = "";
                        PPK.Text = "";
                        Loop_num.Text = "";
                        Final_Folder.Text = "";
                        Temp_Folder.Text = "";
                        Temp_Folder_2.Text = "";
                        Memory.Text = "";
                        Thread.Text = "";
                        Bucket.Text = "";
                        Process.Start("explorer.exe", "/select," + fname + @"\resources\app.asar.unpacked\daemon\plot" + dt_str + ".bat");
                    }
                }
            }
            else
            {
                MessageBox.Show("請輸入完整內容");
            }
        }

        String[] Parameter_Save()
        {
            String[] Temp = new String[] { K_size.Text, Fingerprint.Text, FPK.Text, PPK.Text, Loop_num.Text, Final_Folder.Text, Temp_Folder.Text, Temp_Folder_2.Text, Memory.Text, Thread.Text, Bucket.Text };
            return Temp;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Outport_txt.Text != "")
            {
                String[] parameter_str = Parameter_Save();
                DateTime dt = DateTime.Now;
                String dt_str = dt.Year + "年" + dt.Month + "月" + dt.Day + "日" + dt.Hour + "點" + dt.Minute + "分" + dt.Second + "秒";
                StreamWriter sw = new StreamWriter(@Outport_txt.Text + @"\參數設定" + dt_str + ".txt");
                for (int i = 0; i < parameter_str.Length; i++)
                {
                    sw.WriteLine(parameter_str[i]);
                }
                sw.Close();
                Process.Start("explorer.exe", "/select," + @Outport_txt.Text + @"\參數設定" + dt_str + ".txt");
            }
            else
            {
                MessageBox.Show("請輸入完整內容");
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(@Import_txt.Text);
                K_size.Text = sr.ReadLine();
                Fingerprint.Text = sr.ReadLine();
                FPK.Text = sr.ReadLine();
                PPK.Text = sr.ReadLine();
                Loop_num.Text = sr.ReadLine();
                Final_Folder.Text = sr.ReadLine();
                Temp_Folder.Text = sr.ReadLine();
                Temp_Folder_2.Text = sr.ReadLine();
                Memory.Text = sr.ReadLine();
                Thread.Text = sr.ReadLine();
                Bucket.Text = sr.ReadLine();
                Import_txt.Text = "";
                sr.Close();
            }
            catch
            {
                MessageBox.Show("請選擇有效檔案");
            }

        }
    }
}
