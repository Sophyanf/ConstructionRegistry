using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Models
{
    public class ContractCustomer
    {
        public int Id { get; set; }
        public string ContractCustomerNumber { get; set; }
        public DateTime ContractCustomerData { get; set; }
        public Kontragent Kontragent { get; set; }
        public ICollection<ConstructionObject> ConstructionObjects { get; set; }
    }
}
