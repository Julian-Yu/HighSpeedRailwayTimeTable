using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSpeedRailwayTimeTable
{
    internal class Line
    {
        /// <summary>
        /// 线路编号
        /// </summary>
        public int line_no = 0;
        /// <summary>
        /// 线路名
        /// </summary>
        public string line_name = "";
        /// <summary>
        /// 线路总里程长度
        /// </summary>
        public double line_mile = 0;
        /// <summary>
        /// 线路车站列表
        /// </summary>
        public Dictionary<int, Station> line_stations = new Dictionary<int, Station>();
    }
}
