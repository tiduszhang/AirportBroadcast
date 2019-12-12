using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Common.Dto
{
 

    public class LanguageForChoose
    {
        
        public LanguageForChoose(int v, string name)
        {
            this.Id = v;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }

}
