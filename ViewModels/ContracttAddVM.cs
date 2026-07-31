using ConstructionRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConstructionRegistry.ViewModels
{
    public class ContractAddVM : BaseViewModel
    {
        public ActionCommand AddNewContract { get; set; }

        #region Properties

        private String contractNumber;
        public String ContractNumber
           
            {
                get => contractNumber;
                set => UpdateValue(ref contractNumber, value);
            }


        private DateTime contractData;
        public DateTime ContractData

        {
            get => contractData;
            set => UpdateValue(ref contractData, value);
        }


        #endregion

        private async void AddContractAsync()
        {
            Contract contract = new Contract()
            {
                Kontragent = Customer,
                ContractNumber = contractNumber,
                ContractData = contractData
            };
        }

        public ContractAddVM() // конструктор
        {
            AddNewKontragent = new ActionCommand(x => AddContractAsync());
        }
    }
}
