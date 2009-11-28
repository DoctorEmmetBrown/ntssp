using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayEngine.Display2D;

namespace Data
{
    public class Level
    {
        private List<Formation> mFormations = new List<Formation>();

        public List<Formation> Formations
        {
            get { return mFormations; }
        }

        private Background mBackground = null; 

    }
}
