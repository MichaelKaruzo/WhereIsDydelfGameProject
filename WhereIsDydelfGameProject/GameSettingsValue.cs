using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsDydelfGameProject
{
    //Default Values, subject to change by User in SettingsControlPanel
    public class GameSet
    {
        public int Width { get; set; } = 5;
        public int Height { get; set; } = 5;
        public int Dydelfs { get; set; } = 3;
        public int Crocs { get; set; } = 1;
        public int Time { get; set; } = 30;
    }
}
