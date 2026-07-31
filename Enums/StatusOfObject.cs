using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Enums
{
    public enum StatusOfObject
    {
        ApplicationOnly = 0,
        InWork = 1,
        TransferredToCustomer = 2,
        Correction = 3,
        Resubmitted = 4,
        Accepted = 5,
        Paid = 6
    }
}
