using ConstructionRegistry.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConstructionRegistry.ViewModels
{
    public class ResponsiblPersonVM : BaseViewModel
    {
        public ActionCommand AddNewReponsiblPersen { get; set; }

        private String personFIO;
        public String PersonFIO
        {
            get { return personFIO; }
            set
            {
                personFIO = value;
                OnPropertyChanged();
            }
        }

        private String personPost;
        public String PersonPost
        {
            get { return personPost; }
            set
            {
                personPost = value;
                OnPropertyChanged();
            }
        }

        private String personDocument;
        public String PersonDocument
        {
            get { return personDocument; }
            set
            {
                personDocument = value;
                OnPropertyChanged();
            }
        }

        private String personFunctions;
        public String PersonFunctions
        {
            get { return personFunctions; }
            set
            {
                personFunctions = value;
                OnPropertyChanged();
            }
        }
            
        private async void AddNewPersonAsync()
        {
            ThisPerson = new ResponsiblPerson() {


                PersonFIO = this.personFIO,
                PersonPost = this.personPost,
                PersonDocument = this.personDocument,
            };


            bool rez = false;
            await Task.Run(async () =>
            {
                rez = await dataObjAdd.AddPersonAsync(ThisPerson, SelectKontragent);
                MessageBox.Show("Запись");
            });
            if (rez == false)
            {
                MessageBox.Show("Ошибка!!! Проверьте объект");
                return;
            }
            else Application.Current.Windows.OfType<Window>().SingleOrDefault(y => y.IsActive).Close();
        }

        public ResponsiblPersonVM() // Конструктор
        {
            Kontragents = new ObservableCollection<Kontragent>();
            LoadKontragents();
            AddNewReponsiblPersen = new ActionCommand(x => AddNewPersonAsync());
        }
    }
}

