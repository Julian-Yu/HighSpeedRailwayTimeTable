using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HighSpeedRailwayTimeTable
{
    public partial class MainForm : Form
    {
        /// <summary>
        ///运行图总宽度
        /// </summary>
        static int TD_Width = 2000;
        /// <summary>
        ///运行图总高度
        /// </summary>
        static int TD_Height = 1200;
        /// <summary>
        ///DataManager对象，用于读取文件
        /// </summary>
        DataManager dm;
        /// <summary>
        ///PaintTool对象，用于绘图
        /// </summary>
        PaintTool pt = new PaintTool();
        /// <summary>
        ///bmp，运行图绘制的底图
        /// </summary>
        public Bitmap bmp = new Bitmap(TD_Width, TD_Height);
        /// <summary>
        /// 主程序
        /// </summary>
        public MainForm()
        {
            pictureBox1 = new PictureBox();
            pictureBox1.Size = new Size(TD_Width, TD_Height);
            /// 获取线路下拉菜单
            {
                DataTable line_table = new DataTable();
                line_table.Columns.Add("line_no");
                line_table.Columns.Add("line_name");
                foreach (Line line in dm.LineDict.Values)
                {
                    DataRow dataRow = line_table.NewRow();
                    dataRow["line_no"] = line.line_no;
                    dataRow["line_name"] = line.line_name;
                    line_table.Rows.Add(dataRow);
                }
                comboBox1.DataSource = line_table;
                comboBox1.DisplayMember = "line_name";
                comboBox1.ValueMember = "line_no";
            }

            foreach (Train train in dm.TrainDict.Values)
            {
                if (comboBox2.Items.IndexOf(train.train_date) == -1)
                {
                    comboBox2.Items.Add(train.train_date.ToString());
                }
            }
            {
                comboBox3.Items.Add("空白运行图");
                comboBox3.Items.Add("上行运行图");
                comboBox3.Items.Add("下行运行图");
                comboBox3.Items.Add("上下行运行图");
            }
            InitializeComponent();
        }

        /// <summary>
        ///绘制运行线
        /// </summary>
        public void DrawPicture()
        {
            pictureBox1.Size = new Size(TD_Width, TD_Height);
            Graphics gs;
            gs = Graphics.FromImage(bmp);
            List<double> staMile = new List<double>();
            pictureBox1.BackgroundImage = null;
            gs.Clear(this.pictureBox1.BackColor);
            if (this.comboBox3.SelectedIndex == 3)
            {
                int i = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                DateOnly date = DateOnly.FromDateTime(Convert.ToDateTime(comboBox2.SelectedItem.ToString()));
                pt.TimetableFrame(this.bmp.Width, this.bmp.Height, dm.LineDict[i].line_mile, dm.LineDict, gs, dm.LineDict[i]);
                pt.TrainLine(gs, dm.UpTrainDict, dm.LineDict, dm.LineDict[i], date);
                pt.TrainLine(gs, dm.DownTrainDict, dm.LineDict, dm.LineDict[i], date);
            }
            else if (this.comboBox3.SelectedIndex == 1)
            {
                int i = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                DateOnly date = DateOnly.FromDateTime(Convert.ToDateTime(comboBox2.SelectedItem.ToString()));
                pt.TimetableFrame(this.bmp.Width, this.bmp.Height, dm.LineDict[i].line_mile, dm.LineDict, gs, dm.LineDict[i]);
                pt.TrainLine(gs, dm.UpTrainDict, dm.LineDict, dm.LineDict[i], date);
            }
            else if (this.comboBox3.SelectedIndex == 2)
            {
                int i = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                DateOnly date = DateOnly.FromDateTime(Convert.ToDateTime(comboBox2.SelectedItem.ToString()));
                pt.TimetableFrame(this.bmp.Width, this.bmp.Height, dm.LineDict[i].line_mile, dm.LineDict, gs, dm.LineDict[i]);
                pt.TrainLine(gs, dm.DownTrainDict, dm.LineDict, dm.LineDict[i], date);
            }
            else
            {
                int i = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                DateOnly date = DateOnly.FromDateTime(Convert.ToDateTime(comboBox2.SelectedItem.ToString()));
                pt.TimetableFrame(this.bmp.Width, this.bmp.Height, dm.LineDict[i].line_mile, dm.LineDict, gs, dm.LineDict[i]);
            }
            this.pictureBox1.BackgroundImage = bmp;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "保存图片";
            dialog.Filter = @"jpeg|*.jpg|bmp|*.bmp|png|*.png";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string Name = dialog.FileName.ToString();
                if (Name != "" && Name != null)
                {
                    string filename = Name.Substring(Name.LastIndexOf(".") + 1).ToString();
                    System.Drawing.Imaging.ImageFormat imgformat = null;


                    if (filename != "")
                    {
                        switch (filename)
                        {
                            case "jpg":
                                imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            case "bmp":
                                imgformat = System.Drawing.Imaging.ImageFormat.Bmp;
                                break;
                            case "png":
                                imgformat = System.Drawing.Imaging.ImageFormat.Png;
                                break;
                            default:
                                imgformat = System.Drawing.Imaging.ImageFormat.Png;
                                break;
                        }
                        try
                        {
                            DrawPicture();
                            Bitmap bit = new Bitmap(pictureBox1.BackgroundImage);
                            MessageBox.Show(Name);
                            pictureBox1.BackgroundImage.Save(Name, imgformat);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawPicture();
        }
        /// <summary>
        /// 放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (TD_Width < 4000 && TD_Height < 2400)
            {
                TD_Width += 20;
                TD_Height += 12;
            }
            bmp = new Bitmap(TD_Width, TD_Height);
            pt.TimeX = new List<float>();
            pt.staY = new List<float>();
            DrawPicture();
        }
        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (TD_Width > 500 && TD_Height > 300)
            {
                TD_Width -= 20;
                TD_Height -= 12;
            }
            bmp = new Bitmap(TD_Width, TD_Height);
            pt.TimeX = new List<float>();
            pt.staY = new List<float>();
            DrawPicture();
        }
        /// <summary>
        /// 向上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 向下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 向左
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 向右
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}