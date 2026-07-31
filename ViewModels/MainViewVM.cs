using ConstructionRegistry.Controllers;
using ConstructionRegistry.Models;
using ConstructionRegistry.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Application = System.Windows.Application;
using Window = System.Windows.Window;

namespace ConstructionRegistry.ViewModels
{
   public class MainViewVM : BaseViewModel
    {
     
        public ActionCommand AddNewObjectDB { get; set; }
        public ActionCommand AddPersonDB { get; set; }
        public ActionCommand RemoveObjectDB { get; set; }

        public MainViewVM()
        {
            AddNewObjectDB = new ActionCommand(x => AddNewObject());
            AddPersonDB = new ActionCommand(x => AddPerson());
            RemoveObjectDB = new ActionCommand(x => RemoveObject());
            ObjectsList = new ObservableCollection<ConstructionObject>();
            LoadObjects();
            PersonsList = new ObservableCollection<ResponsiblPerson>();
            LoadPersens();
        }

        private void RemoveObject()
        {
            dataObjRemove.RemoveObject(SelectObject);
            MessageBox.Show(SelectObject.ObjectName);
            //windowToOpen.Closing += (o, args) => { };
        }

        private void AddPerson()
        {
            AddResponsiblPerson person = new AddResponsiblPerson();
            person.ShowDialog();
        }

        private void AddNewObject()
        {
            AddObjectView objectView = new AddObjectView();
            objectView.ShowDialog();
        }
    }
}
