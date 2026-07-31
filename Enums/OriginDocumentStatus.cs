using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Enums
{
    public enum OriginDocumentStatus
    {
        Yes = 1,          // Да (оригиналы получены)
        No = 2,           // Нет (оригиналы не получены)
        OnCorrection = 3  // На исправлении
    }
}
