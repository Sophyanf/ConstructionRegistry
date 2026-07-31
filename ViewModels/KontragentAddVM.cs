using ConstructionRegistry.Enums;
using ConstructionRegistry.Models;
using ConstructionRegistry.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConstructionRegistry.ViewModels
{
    public class KontragentAddVM : BaseViewModel
    {
        public ActionCommand AddNewKontragent { get; set; }

        private String kontragentName;
        public String KontragentName
        {
            get => kontragentName;
            set => UpdateValue(ref kontragentName, value);
        }

        private String kontragentShortName;
        public String KontragentShortName
        {
            get => kontragentShortName;
            set => UpdateValue(ref kontragentShortName, value);

        }

        private String kontragentINN;
        public String KontragenINN
        {
            get => kontragentINN; 
            set => UpdateValue(ref kontragentINN, value);
         
        }

        private Adress kontragentAdress;
        public Adress KontragentAdress
        {
            get => kontragentAdress;
            set => UpdateValue(ref kontragentAdress, value);
        }

        private bool rateNDS;
        public bool RateNDS
        {
            get => rateNDS;
            set => UpdateValue(ref rateNDS, value);
        }

        private async void AddKontragentAsync()
        {
            Kontragent kontragent = new Kontragent()
            {
                    KontragentName = this.kontragentName,
                    KontragentShortName = this.KontragentShortName,
                    KontragentAdress = this.kontragentAdress,
                    KontragentINN = this.kontragentINN,
                    NDSRate = this.rateNDS
            };

            if (await dataObjAdd.AddKontragentAsync(kontragent) == false)
            {
                MessageBox.Show("Ошибка!!! Проверьте категорию");
            }
            else Application.Current.Windows.OfType<Window>().SingleOrDefault(y => y.IsActive).Close();
        }

        public KontragentAddVM () // конструктор
        {
            AddNewKontragent = new ActionCommand(x => AddKontragentAsync());
        }
    }
}
