using System.Collections.Generic;

namespace ConstructionRegistry.Enums
{
    public static class StatusOfObjectDisplay
    {
        private static readonly Dictionary<StatusOfObject, string> _map =
    new Dictionary<StatusOfObject, string>()
        {
            { StatusOfObject.ApplicationOnly, "Только заявка" },
            { StatusOfObject.InWork, "В работе" },
            { StatusOfObject.TransferredToCustomer, "Передан заказчику" },
            { StatusOfObject.Correction, "Корректировка" },
            { StatusOfObject.Resubmitted, "Передан повторно" },
            { StatusOfObject.Accepted, "Принят" },
            { StatusOfObject.Paid, "Оплачен" }
        };

        /// <summary>
        /// Возвращает человекочитаемое название статуса.
        /// Если статус не найден в словаре (ошибка конфигурации), вернёт имя константы enum.
        /// </summary>
        public static string Get(StatusOfObject status) =>
            _map.TryGetValue(status, out var value) ? value : status.ToString();
    }
}