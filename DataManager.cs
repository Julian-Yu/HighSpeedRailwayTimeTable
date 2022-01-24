using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSpeedRailwayTimeTable
{
    internal class DataManager
    {
        /// <summary>
        /// 线路字典
        /// 键与线路编号Line.line_no相同
        /// </summary>
        public Dictionary<int, Line> LineDict = new Dictionary<int, Line>();
        /// <summary>
        /// 列车字典
        /// 键与线路编号Train.train_no相同
        /// </summary>
        public Dictionary<int, Train> TrainDict = new Dictionary<int, Train>();
        /// <summary>
        /// 列车字典
        /// 键与线路编号Train.train_no相同
        /// </summary>
        public Dictionary<int, Train> UpTrainDict = new Dictionary<int, Train>();
        /// <summary>
        /// 列车字典
        /// 键与线路编号Train.train_no相同
        /// </summary>
        public Dictionary<int, Train> DownTrainDict = new Dictionary<int, Train>();
        /// <summary>
        /// 从外部文件中读取线路字典
        /// </summary>
        public void ReadLineDict()
        {

        }
        /// <summary>
        /// 从外部文件中读取列车字典
        /// </summary>
        public void ReadTrainDict()
        {

        }
    }
}
