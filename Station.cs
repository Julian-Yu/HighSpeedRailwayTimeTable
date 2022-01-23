using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSpeedRailwayTimeTable
{
    internal class Station
    {
        /// <summary>
        /// 车站编号
        /// </summary>
        public int station_no = -1;
        /// <summary>
        /// 车站名
        /// </summary>
        public string station_name = "";
        /// <summary>
        /// 车站是否连接两个及以上线路
        /// </summary>
        public bool station_junction = false;
        /// <summary>
        /// 车站中心线里程
        /// </summary>
        public Dictionary<Line, double> station_miles = new Dictionary<Line, double>();
    }
}
