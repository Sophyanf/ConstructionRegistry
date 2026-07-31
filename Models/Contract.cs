using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public DateTime ContractData { get; set; }
        public Kontragent Kontragent { get; set; }
        public ICollection<ConstructionObject> ConstructionObjects { get; set; }
    }
}
