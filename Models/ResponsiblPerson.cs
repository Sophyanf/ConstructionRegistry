using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Models
{
    public class ResponsiblPerson : IDataObject
    {
        public int ID { get; set; }
        public string PersonFIO { get; set; }
        public string PersonPost { get; set; }
        public string PersonDocument  { get; set; }
        public Kontragent PersonKontragent { get; set; } 
        public ICollection<ConstructionObject> ConstructionObjects { get; set; } 
    }
}
