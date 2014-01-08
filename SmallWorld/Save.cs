using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld
{
    public class Save
    {
        public String[] name
        {
            get;
            set;
        }

        public bool[] exist
        {
            get;
            set;
        }

        public static readonly Save Instance = new Save();

        private Save() 
        {
            name = new String[3];
            exist = new bool[3]{false, false, false};
        }
    }
}
