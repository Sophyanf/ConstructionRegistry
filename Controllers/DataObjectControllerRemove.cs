using ConstructionRegistry.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ConstructionRegistry.Controllers
{
    public class DataObjectControllerRemove
    {
        private readonly AppDbContext _context;
        protected DataObjectControllerGet dataObjGet = DataObjectControllerGet.Instance;
        /*  public static string manufData = "manufacturer";
          public static string categoryData = "categoryBakery";
          private DataObjectController data = DataObjectController.Instance;*/
        public static DataObjectControllerRemove Instance { get => DataObjectControllerCreate.instance; }
        private DataObjectControllerRemove()
        {
            _context = new AppDbContext();
        }
        private class DataObjectControllerCreate
        {
            static DataObjectControllerCreate() { }
            internal static readonly DataObjectControllerRemove instance = new DataObjectControllerRemove();
        }




       
        public bool RemoveObject(ConstructionObject obj)  // Удаление проекта
        {
            try
            {    
                ConstructionObject result = _context.ConstructionObjects.FirstOrDefault(o => o.ID == obj.ID);
                _context.ConstructionObjects.Remove(result);
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddPersonAsync(ResponsiblPerson obj, Kontragent kontragent)  // Добавление ответственных
        {
            try
            {
                _context.Kontragents.Include("ResponsiblPersons").FirstOrDefault(k => k.ID == kontragent.ID).ResponsiblPersons.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddKontragentAsync(Kontragent obj)  // Добавление контрагента
        {
            try
            {
                _context.Kontragents.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}


