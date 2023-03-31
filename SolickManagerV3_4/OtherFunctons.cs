using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolickManagerV3_4
{
    public class OtherFunctons
    {
        private static OtherFunctons other { get; set; }

        public static OtherFunctons Instance
        {
            get
            {
                if (other == null)
                    other = new OtherFunctons();
                return other;
            }
            set => other = value;
        }

        public DateOnly DateOnlyNow() {
            string dataTime = DateTime.Now.ToString();

            string[] dataAndTime = dataTime.Split(' ');

            DateOnly data = DateOnly.Parse(dataAndTime[0]);

            return data;
        }
    }
}
