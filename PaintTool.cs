using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

namespace HighSpeedRailwayTimeTable
{
    internal class PaintTool
    {
        DataManager dm = new DataManager();
        public List<float> TimeX = new List<float>();
        public List<float> staY = new List<float>();


        /// <summary>
        /// 运行图基本框架
        /// </summary>
        /// <param name="WinWidth">运行图窗口宽度</param>
        /// <param name="WinHeight">运行图上边界坐标</param>
        /// <param name="TotalMile">支线总里程</param>
        /// <param name="LineDict">线路字典</param>
        /// <param name="gs">画图区</param>
        /// <param name="line">画图线路</param>
        public void TimetableFrame(double WinWidth, double WinHeight, double TotalMile, Dictionary<int, Line> LineDict, Graphics gs, Line line)
        {
            Font font = new Font("宋体", 8f);
            Brush brush = new SolidBrush(Color.Green);
            StringFormat SF = new StringFormat();
            StringFormat SF1 = new StringFormat();
            SF.Alignment = StringAlignment.Far;
            SF1.Alignment = StringAlignment.Center;
            float Left = 55;    //运行图左边留白
            float Right = 10;   //运行图右边留白
            float Up = 15;      //运行图上边留白
            float Down = 15;    //运行图下边留白
            PointF p1 = new PointF();
            PointF p2 = new PointF();
            Pen pp1 = new Pen(Color.Green, 1);
            Pen pp2 = new Pen(Color.Green, 2);
            Pen pp3 = new Pen(Color.Green, 1);
            pp3.DashStyle = DashStyle.Custom;
            pp3.DashPattern = new float[] { 10f, 5f };
            double Width = WinWidth - (Left + Right);
            double Height = WinHeight - (Up + Down);
            p1.X = (float)(Left);
            p1.Y = (float)(Up);
            p2.X = (float)(Left);
            p2.Y = (float)(Up + Height);
            float add = (float)(Width / 1440);
            float xx = 0;
            int Hour = 0;
            /// 时间线
            for (int j = 0; j <= 1440; j++)
            {
                if (j % 10 == 0)
                {
                    if (j % 60 == 0)
                    {
                        gs.DrawLine(pp2, p1, p2);
                        TimeX.Add(p1.X);
                        p1.X = (float)(p1.X + add);
                        p2.X = (float)(p2.X + add);
                        gs.DrawString(Convert.ToString(Hour), font, brush, p2.X, p2.Y + 5, SF1);//在这添加插入时间语句
                        gs.DrawString(Convert.ToString(Hour), font, brush, p1.X, p1.Y - 15, SF1);//在这添加插入时间语句
                        Hour++;


                    }
                    else if (j % 60 != 0 && j % 30 == 0)
                    {
                        gs.DrawLine(pp3, p1, p2);
                        TimeX.Add(p1.X);
                        p1.X = (float)(p1.X + add);
                        p2.X = (float)(p2.X + add);
                    }
                    else
                    {
                        gs.DrawLine(pp1, p1, p2);
                        TimeX.Add(p1.X);
                        p1.X = (float)(p1.X + add);
                        p2.X = (float)(p2.X + add);
                    }
                }
                else
                {
                    TimeX.Add(p1.X);
                    p1.X = (float)(p1.X + add);
                    p2.X = (float)(p2.X + add);
                }
            }
            xx = p1.X - add;
            /// 车站线
            pp1 = new Pen(Color.Green, 1);
            pp2 = new Pen(Color.Green, 2);
            p1.X = Left;
            p2.X = xx;
            int n = LineDict[line.line_no].line_stations.Count();   // 获得该线路车站的个数
            for (int k = 0; k < n; k++)
            {
                if (LineDict[line.line_no].line_stations[k].station_junction)
                {
                    p1.Y = (float)(Up + Height * LineDict[line.line_no].line_stations[k].station_miles[line] / TotalMile);
                    p2.Y = (float)(Up + Height * LineDict[line.line_no].line_stations[k].station_miles[line] / TotalMile);
                    gs.DrawLine(pp2, p1, p2);
                    gs.DrawString(LineDict[line.line_no].line_stations[k].station_name, font, brush, p1.X - 5, p1.Y - 5, SF);//在这插入车站标签语句
                    staY.Add(p1.Y);
                }
                else
                {
                    p1.Y = (float)(Up + Height * LineDict[line.line_no].line_stations[k].station_miles[line] / TotalMile);
                    p2.Y = (float)(Up + Height * LineDict[line.line_no].line_stations[k].station_miles[line] / TotalMile);
                    gs.DrawLine(pp1, p1, p2);
                    gs.DrawString(LineDict[line.line_no].line_stations[k].station_name, font, brush, p1.X - 5, p1.Y - 5, SF);//在这插入车站标签语句
                    staY.Add(p1.Y);
                }
            }


            staY = new List<float>();
        }

        /// <summary>
        /// 列车运行线绘制
        /// </summary>
        /// <param name="gs">画图区</param>
        /// <param name="TrainDict">列车字典</param>
        /// <param name="LineDict">线路字典</param>
        /// <param name="line">画图线路</param>
        /// <param name="date">画图日期</param>
        public void TrainLine(Graphics gs, Dictionary<int, Train> TrainDict, Dictionary<int, Line> LineDict, Line line, DateOnly date)
        {
            Pen pp = new Pen(Color.Red, (float)1.2);
            PointF p1 = new PointF();
            PointF p2 = new PointF();
            foreach (Train train in TrainDict.Values)
            {
                if (train.train_date == date) 
                {
                    List<Station> station_list = LineDict[line.line_no].line_stations.Values.ToList();
                    List<Station> train_station = train.timetable.Keys.ToList();
                    int a = train.timetable.Count;
                    for (int i = 0; i < a - 1; i++)
                    {
                        if ((station_list.IndexOf(train_station[i]) != -1)
                            && (station_list.IndexOf(train_station[i + 1]) != -1))
                        {
                            int index1 = station_list.IndexOf(train_station[i]);
                            int index2 = station_list.IndexOf(train_station[i + 1]);
                            int i1 = train.timetable[station_list[index1]][1];
                            int i2 = train.timetable[station_list[index2]][0];
                            p1.X = TimeX[i1];
                            p2.X = TimeX[i2];
                            p1.Y = staY[index1];
                            p2.Y = staY[index2];
                            gs.DrawLine(pp, p1, p2);

                        }
                    }
                    for (int i = 1; i < a - 1; i++)
                    {
                        if (station_list.IndexOf(train.timetable.Keys.ToList()[i]) != -1
                            && train_station[i] != station_list[0]
                            && train_station[i] != station_list[station_list.Count - 1]
                            && train_station[i] != train_station[0]
                            && train_station[i] != train_station[train_station.Count - 1])
                        {
                            int index1 = station_list.IndexOf(train_station[i]);
                            int i1 = train.timetable[station_list[index1]][1];
                            int i2 = train.timetable[station_list[index1]][0];
                            p1.X = TimeX[i1];
                            p2.X = TimeX[i2];
                            p1.Y = staY[index1];
                            p2.Y = staY[index1];
                            gs.DrawLine(pp, p1, p2);
                        }
                    }
                }
            }
        }
    }
}
