using System;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

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
        private void Bat_Builder(object sender, RoutedEventArgs e)
        {
            if (K_size.Text != "" && FPK.Text != "" && PPK.Text != "" && Loop_num.Text != "" && Final_Folder.Text != "" && Temp_Folder.Text != "" && Memory.Text != "" && Thread.Text != "" && Bucket.Text != "")
            {
                DateTime dt = DateTime.Now;
                String dt_str = dt.Year + "年" + dt.Month + "月" + dt.Day + "日" + dt.Hour + "點" + dt.Minute + "分" + dt.Second + "秒";
                var enviromentPath = System.Environment.GetEnvironmentVariable("LocalAppdata");
                foreach (string fname in System.IO.Directory.GetFileSystemEntries(enviromentPath + @"\chia-blockchain"))
                {
                    if (fname.Contains("app"))
                    {
                        StreamWriter sw = new StreamWriter(fname + @"\resources\app.asar.unpacked\daemon\plot" + dt_str + ".bat");
                        sw.Write("chia plots create -k {0} ", K_size.Text);
                        if (Fingerprint.Text != "")
                        {
                            sw.Write("-a {0} ", Fingerprint.Text);
                        }
                        sw.Write("-f {0} -p {1} -n {2} -d {3} -t {4} ", FPK.Text, PPK.Text, Loop_num.Text, Final_Folder.Text, Temp_Folder.Text);
                        if (Temp_Folder_2.Text != "")
                        {
                            sw.Write("-2 {0} ", Temp_Folder_2.Text);
                        }
                        sw.WriteLine("-b {0} -r {1} -u {2}", Memory.Text, Thread.Text, Bucket.Text);
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
                System.Windows.MessageBox.Show("請輸入完整內容");
            }
        }

        String[] Parameter_Save()
        {
            String[] Temp = new String[] { K_size.Text, Fingerprint.Text, FPK.Text, PPK.Text, Loop_num.Text, Final_Folder.Text, Temp_Folder.Text, Temp_Folder_2.Text, Memory.Text, Thread.Text, Bucket.Text };
            return Temp;
        }

        private void Import_setting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileDialog = new OpenFileDialog();
                DialogResult result = fileDialog.ShowDialog();
                StreamReader sr = new StreamReader(@fileDialog.FileName);
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
                sr.Close();
            }
            catch
            {
                System.Windows.MessageBox.Show("請選擇有效檔案");
            }

        }

        private void Outport_setting_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            var Outport_path = dialog.SelectedPath;
            String[] parameter_str = Parameter_Save();
            DateTime dt = DateTime.Now;
            String dt_str = dt.Year + "年" + dt.Month + "月" + dt.Day + "日" + dt.Hour + "點" + dt.Minute + "分" + dt.Second + "秒";
            StreamWriter sw = new StreamWriter(@Outport_path.ToString() + @"\參數設定" + dt_str + ".txt");
            for (int i = 0; i < parameter_str.Length; i++)
            {
                sw.WriteLine(parameter_str[i]);
            }
            sw.Close();
            Process.Start("explorer.exe", "/select," + @Outport_path.ToString() + @"\參數設定" + dt_str + ".txt");
        }
    }
}
