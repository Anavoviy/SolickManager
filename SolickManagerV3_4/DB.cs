using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolickManagerV3_4
{
    public class DB
    {
        private static SolickManagerContext solickManager { get; set; }

        public static SolickManagerContext Instance
        {
            get
            {
                if (solickManager == null)
                    solickManager = new SolickManagerContext();
                return solickManager;
            }
            set => solickManager = value;
        }

        
    }
}
