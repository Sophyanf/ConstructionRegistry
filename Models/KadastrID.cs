using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Models
{
    public class KadastrID
    {
        public int ID { get; set; }
        public String KadastrNum { get; set; } = null;
        public ICollection<ConstructionObject> ConstructionObjects { get; set; } = new List<ConstructionObject>();

       
    }
}
