using ConstructionRegistry.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Models
{
    public class ConstructionObject : IDataObject
    {
        public int ID { get; set; }    
        public string ObjectName { get; set; } 

        public Adress? ObjectAddress { get; set; }     //связь 1:1
                                                       // Внешние ключи
        public int ConstructionOrganizationId { get; set; }
        public int CustomerId { get; set; }
        public int? ConstructionOrganizationSubId { get; set; }
        // Навигационные свойства (остаются)
        public Kontragent ConstructionOrganization { get; set; } //Подрядчик НГМ //связь 1:1
        public Kontragent? ConstructionOrganizationSub { get; set; } //Субподрядчик //связь 1:1
        public Kontragent Customer { get; set; } //Заказчик //связь 1:1
        public DateTime DateOfApplication { get; set; } //связь 1:1
        public DateTime? EndDate { get; set; } //связь 1:1
        public double? CostOfObject { get; set; } //цена объекта
        public double? SpendingOfObject { get; set; }  //сумма субподряда (затраты)
        public KadastrID? KadastrID { get; set; }  //может быть null //связь 1:1
        public  TypeOfObject? TypeOfObject { get; set; }  //может быть null //связь 1:1
        public ResponsiblPerson? CustomerOrgRespPerson { get; set; }    //связь 1(CustomerOrgRespPerson):много(ResponsiblPerson)// подписант
        public StatusOfObject Status { get; set; } = StatusOfObject.InWork; // enum
        public string? Comment { get; set; }
        public OriginDocumentStatus OriginDocuments { get; set; }
        public OriginDocumentStatus OriginDocumentsSub { get; set;}
        public int? PaymentInvoice { get; set; } // счет на оплату
        public int? Invoice { get; set; } // счет-фактура
        public ContractCustomer ContractCustomer { get; set; }
        public double SubContractCustomeringCoefficients { get; set; }

    }
}
