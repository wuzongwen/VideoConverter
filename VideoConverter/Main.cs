using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VideoConverter
{
    public partial class Main : MetroForm
    {

        string path = AppDomain.CurrentDomain.BaseDirectory;//获取程序路径
        int filelen = 0;//已转换文件数
        string Time;//视频时长
        int index = 0;//索引
        string thisFile = string.Empty;//当前转码视频

        public Main()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;//解决线程间操作无效
            // 初始化ListView 
            listView1.FullRowSelect = true;// file:要选择就是一行 
            listView1.View = View.Details;// file:定义列表显示的方式 
            listView1.Scrollable = true;// file:需要时候显示滚动条 
            listView1.MultiSelect = false;// 不可以多行选择 
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            // 针对文件建立与之适应显示表头 
            listView1.Columns.Add("文件名", 161, HorizontalAlignment.Left);
            listView1.Columns.Add("文件地址", 326, HorizontalAlignment.Left);
            listView1.Columns.Add("文件大小", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("状态", 58, HorizontalAlignment.Left);
            listView1.CheckBoxes = true;
            listView1.Visible = true;

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                comboBox2.Items.Add(i + 1);
            }

            comboBox1.SelectedIndex = 1;//设置默认清晰度为高
            comboBox2.SelectedIndex = 0;//设置默认格式为MP4
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListView lv = listView1;
            if (lv.Items.Count < 1)
            {
                MessageBox.Show("请添加视频文件", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (label3.Text == "请选择输出路径" || label3.Text == "")
            {
                MessageBox.Show("请选择输出路径", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int listfile = 0;
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[3].Text == "已完成")
                {
                    listfile++;
                }
            }
            if (listfile >= lv.Items.Count)
            {
                MessageBox.Show("列表中视频已全部转换完成,请重新选择需要转码的文件", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 禁用按钮
            this.button1.Enabled = false;

            // 实例化业务对象

            //百分比
            ValueChanged += new ValueChangedEventHandler(workder_ValueChanged);
            // 使用异步方式调用长时间的方法
            Action handler = new Action(VideoConverter);
            handler.BeginInvoke(
                new AsyncCallback(this.AsyncCallback),
                handler
                );
        }


        //更新文件状态
        public void setState(int str)
        {
            listView1.Items[str].SubItems[3].Text = "已完成";
        }

        //当前文件转码进度
        public void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                string nowtime = "";
                if (e.Data != null)
                {
                    if (e.Data.ToString().Contains("Duration"))
                    {
                        int length = e.Data.ToString().IndexOf("Duration");

                        //获取视频长度
                        Time = e.Data.ToString().Substring(e.Data.ToString().IndexOf(':') + 2, e.Data.ToString().IndexOf(':') + 1);
                    }
                    if (e.Data.ToString().Contains("frame="))
                    {
                        int timesl = 0;
                        int timesr = 0;
                        //视频总时长转毫秒
                        string[] times = Time.Split(':');
                        for (int i = 0; i < times.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    timesl += int.Parse(times[i]) * 360000;
                                    break;
                                case 1:
                                    timesl += int.Parse(times[i]) * 6000;
                                    break;
                                case 2:
                                    timesl += (int.Parse(times[i].Split('.')[0]) * 100) + int.Parse(times[i].Split('.')[1]);
                                    break;
                            }
                        }

                        //当前已转码视频长度转毫秒
                        nowtime = e.Data.ToString().Split('=')[5].Substring(0, 11);
                        string[] times2 = nowtime.Split(':');
                        for (int i = 0; i < times2.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    timesr += int.Parse(times2[i]) * 360000;
                                    break;
                                case 1:
                                    timesr += int.Parse(times2[i]) * 6000;
                                    break;
                                case 2:
                                    timesr += (int.Parse(times2[i].Split('.')[0]) * 100) + int.Parse(times2[i].Split('.')[1]);
                                    break;
                            }
                        }
                        float db = (float)timesr / timesl;
                        listView1.Items[index].SubItems[3].Text = (Math.Round(db, 2)) * 100 + "%";
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                throw;
            }

        }

        private static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            WriteLog(e.Data);
        }

        //添加视频文件
        private void button2_Click(object sender, EventArgs e)
        {
            //初始化一个OpenFileDialog类
            OpenFileDialog ofd = new OpenFileDialog();

            //打开的对话框可以选择多个文件  
            ofd.Multiselect = true;

            //判断用户是否正确的选择了文件
            ofd.Filter = "视频|*.mp4;*.flv;*.rmvb;*.avi;*.wmv;*.mpg;*.MOV";


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] videoArr;//选择多个视频路径保存到数组
                videoArr = ofd.FileNames;

                //将文件加入任务列表
                for (int i = 0; i < videoArr.Length; i++)
                {
                    FileInfo fileinfo = new FileInfo(videoArr[i]);
                    ListViewItem li = new ListViewItem();
                    li.SubItems.Clear();
                    li.SubItems[0].Text = fileinfo.Name;
                    li.SubItems.Add(fileinfo.FullName);
                    li.SubItems.Add(fileinfo.Length / 1024 + "kb");
                    li.SubItems.Add("待转换");
                    listView1.Items.Add(li);
                }
            }
        }

        //移除选中项
        private void button3_Click(object sender, EventArgs e)
        {
            ListView lv = listView1;
            int len = lv.CheckedItems.Count;
            int lvlen = lv.CheckedItems.Count - 1;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    int index = lv.CheckedItems[lvlen].Index;//获取索引
                    if (lv.Items[index].SubItems[3].Text == "已完成" || lv.Items[index].SubItems[3].Text == "待转换")
                    {
                        lv.Items.Remove(lv.Items[index]);
                        lvlen--;
                    }
                    else
                    {
                        MessageBox.Show("当前状态不可移除");
                    }
                }
            }
            else
            {
                MessageBox.Show("未选中任何行", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Decimal num = 100 / getFilecount();//每个视频所占百分比
        }


        //选择文件路径
        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            if (path.ShowDialog() == DialogResult.OK)
            {
                this.label3.Text = path.SelectedPath;
            }
        }

        // 进度发生变化之后的回调方法
        private void workder_ValueChanged(object sender, ValueEventArgs e)
        {
            System.Windows.Forms.MethodInvoker invoker = () =>
            {
                this.progressBar1.Value = e.Value;
            };

            if (this.progressBar1.InvokeRequired)
            {
                this.progressBar1.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }


        // 结束异步操作
        private void AsyncCallback(IAsyncResult ar)
        {
            // 标准的处理步骤
            Action handler = ar.AsyncState as Action;
            handler.EndInvoke(ar);

            MessageBox.Show("转码完成！文件数:" + filelen);
            //打开资源管理器
            Process.Start("explorer.exe", label3.Text);
            //打开并选中文件
            //System.Diagnostics.Process.Start("Explorer.exe", "/select," + label3.Text+"文件名");

            System.Windows.Forms.MethodInvoker invoker = () =>
            {
                // 重新启用按钮
                this.button1.Enabled = true;
            };

            if (this.InvokeRequired)
            {
                this.Invoke(invoker);
            }
            else
            {
                invoker();
            }
            this.progressBar1.Value = 0;
        }

        // 定义事件的参数类
        public class ValueEventArgs
            : EventArgs
        {
            public int Value { set; get; }
        }

        // 定义事件使用的委托
        public delegate void ValueChangedEventHandler(object sender, ValueEventArgs e);

        public event ValueChangedEventHandler ValueChanged;

        // 触发事件的方法
        public void OnValueChanged(ValueEventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        //获取待转换文件个数
        public int getFilecount()
        {
            ListView lv = listView1;
            int count = 0;
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[3].Text != "已完成")
                {
                    count++;
                }
            }
            return count;
        }

        public void VideoConverter()
        {
            this.progressBar1.Visible = true;
            filelen = 0;
            ListView lv = listView1;//列表视频路径保存到数组
            Decimal num = 100 / getFilecount();//每个视频所占百分比
            string numstr = Math.Ceiling(num).ToString();
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[3].Text != "已完成")
                {
                    listView1.Items[i].SubItems[3].Text = "0%";
                    Process p = new Process();

                    p.StartInfo.FileName = path + "ffmpeg";

                    p.StartInfo.UseShellExecute = false;
                    string srcFileName = "";
                    string newFileName = "";

                    srcFileName = "\"" + lv.Items[i].SubItems[1].Text + "\"";
                    newFileName = lv.Items[i].SubItems[0].Text.Split('.')[0];

                    string filetime = DateTime.Now.ToString("yyyyMMddhhmmss");
                    thisFile = label3.Text + "\\" + newFileName + filetime + ".mp4";

                    newFileName = "\"" + label3.Text + "\\" + newFileName + filetime + ".mp4";
                    

                    string ysl = "";
                    switch (comboBox1.SelectedItem.ToString())
                    {
                        case "低":
                            ysl = "40";
                            break;
                        case "普通":
                            ysl = "35";
                            break;
                        case "一般":
                            ysl = "30";
                            break;
                        case "高":
                            ysl = "25";
                            break;
                        case "极高":
                            ysl = "20";
                            break;
                    }
                    //-preset：指定编码的配置。x264编码算法有很多可供配置的参数，
                    //不同的参数值会导致编码的速度大相径庭，甚至可能影响质量。
                    //为了免去用户了解算法，然后手工配置参数的麻烦。x264提供了一些预设值，
                    //而这些预设值可以通过preset指定。这些预设值有包括：
                    //ultrafast，superfast，veryfast，faster，fast，medium，slow，slower，veryslow和placebo。
                    //ultrafast编码速度最快，但压缩率低，生成的文件更大，placebo则正好相反。x264所取的默认值为medium。
                    //需要说明的是，preset主要是影响编码的速度，并不会很大的影响编码出来的结果的质量。
                    //-crf：这是最重要的一个选项，用于指定输出视频的质量，取值范围是0-51，默认值为23，数字越小输出视频的质量越高。
                    //这个选项会直接影响到输出视频的码率。一般来说，压制480p我会用20左右，压制720p我会用16-18，1080p我没尝试过。
                    //个人觉得，一般情况下没有必要低于16。最好的办法是大家可以多尝试几个值，每个都压几分钟，看看最后的输出质量和文件大小，自己再按需选择。
                    p.StartInfo.Arguments = "-i " + srcFileName + " -y -vcodec h264 -threads " + comboBox2.SelectedItem + " -crf " + ysl + " " + newFileName + "\"";   //执行参数

                    p.StartInfo.UseShellExecute = false;  ////不使用系统外壳程序启动进程
                    p.StartInfo.CreateNoWindow = true;  //不显示dos程序窗口

                    p.StartInfo.RedirectStandardInput = true;

                    p.StartInfo.RedirectStandardOutput = true;

                    p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中

                    p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

                    p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);

                    p.StartInfo.UseShellExecute = false;

                    p.Start();

                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    p.BeginErrorReadLine();//开始异步读取

                    p.WaitForExit();//阻塞等待进程结束

                    p.Close();//关闭进程


                    p.Dispose();//释放资源
                    setState(i);//更新文件状态
                    filelen += 1;//已转换文件数+1
                    //更新进度条
                    index++;
                    if ((num * index) < 100)
                    {
                        ValueEventArgs e = new ValueEventArgs() { Value = int.Parse(numstr) * index };
                        this.OnValueChanged(e);
                    }
                    else
                    {
                        ValueEventArgs e = new ValueEventArgs() { Value = 100 };
                        this.OnValueChanged(e);
                    }
                }
            }
        }

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="strLog">日志内容</param>
        public static void WriteLog(string strLog)
        {
            string sFilePath = AppDomain.CurrentDomain.BaseDirectory + "Log/" + DateTime.Now.ToString("yyyyMM");
            string sFileName = "log" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + strLog);
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 关闭ffmpeg进程
        /// </summary>
        private void Closed_ffmpeg()
        {
            Process[] pro = Process.GetProcesses();//获取已开启的所有进程

            //遍历所有查找到的进程

            for (int i = 0; i < pro.Length; i++)
            {
                //判断此进程是否是要查找的进程
                if (pro[i].ProcessName.ToString().ToLower() == "ffmpeg")
                {
                    pro[i].Kill();//结束进程
                }
            }
        }

        /// <summary>
        /// 跟随窗体尺寸变化
        /// </summary>
        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = 160;
            listView1.Columns[1].Width = 308;
            listView1.Columns[2].Width = 70;
            listView1.Columns[3].Width = 58;
        }

        // 事件: 改变列宽的时候
        private void ColumnWidthChange(object sender, ColumnWidthChangingEventArgs e)
        {
            // 如果调整的不是第一列,就不管了 
            if (e.ColumnIndex > 0) return;
            // 取消掉正在调整的事件 
            e.Cancel = true;
            // 把新宽度恢复到之前的宽度 
            e.NewWidth = this.listView1.Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// 关闭程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ListView lv = listView1;
            int count = 0;
            for (int i = 0; i < lv.Items.Count; i++)
            {
                if (lv.Items[i].SubItems[3].Text.Contains("%"))
                {
                    count++;
                }
            }
            if (count > 0)
            {
                DialogResult dr = MessageBox.Show("是否停止转码", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    //关闭进程
                    Closed_ffmpeg();

                    //删除文件
                    if (!string.IsNullOrEmpty(thisFile))
                    {
                        Thread.Sleep(5);
                        File.Delete(thisFile);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}