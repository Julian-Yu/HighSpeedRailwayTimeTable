using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSpeedRailwayTimeTable
{
    internal class Train
    {
        /// <summary>
        /// 列车编号（唯一性）
        /// </summary>
        public int train_no = 0;
        /// <summary>
        /// 列车车次
        /// </summary>
        public string train_num = "G000";
        /// <summary>
        /// 列车方向
        /// </summary>
        public string train_dir = "up";
        /// <summary>
        /// 列车从始发站出发时的日期
        /// </summary>
        public DateOnly train_date = DateOnly.MinValue;
        /// <summary>
        /// 计划运行图
        /// </summary>
        public Dictionary<Station, List<int>> timetable = new Dictionary<Station, List<int>>();
    }
}
