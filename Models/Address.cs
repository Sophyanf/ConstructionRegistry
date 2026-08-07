using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Models
{
    public class Address
    {
        public int ID { get; set; }
        public String? Region { get; set; }
        public String? Locality { get; set; }
        public String? Street { get; set; }
        public String? House { get; set; }
        public String? Building { get; set; }
        public String AddressName { get; set; }
        public DateTime dateOfChenge { get; set; }
        public ICollection<ConstructionObject> ConstructionObjects { get; set; } = new List<ConstructionObject>();
    }
}
