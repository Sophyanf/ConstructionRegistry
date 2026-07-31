using System;
using System.Collections.Generic;
using ConstructionRegistry.Views;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Enums
{
    public static class OriginDocumentStatusDisplay
    {
        private static readonly Dictionary<OriginDocumentStatus, string> Map =
            new Dictionary<OriginDocumentStatus, string>
            {
                { OriginDocumentStatus.Yes, "Да" },
                { OriginDocumentStatus.No, "Нет" },
                { OriginDocumentStatus.OnCorrection, "Ждем замену" }
            };

        public static string Get(OriginDocumentStatus status) =>
            Map.TryGetValue(status, out var value) ? value : status.ToString();
    }
}