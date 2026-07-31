using ConstructionRegistry.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRegistry.Controllers
{
    public class DataObjectControllerGet
    {
        private readonly AppDbContext _context;
        /*  public static string manufData = "manufacturer";
          public static string categoryData = "categoryBakery";
          private DataObjectController data = DataObjectController.Instance;*/
        public static DataObjectControllerGet Instance { get => DataObjectControllerCreate.instance; }
        private DataObjectControllerGet()
        {
            _context = new AppDbContext();
        }
        private class DataObjectControllerCreate
        {
            static DataObjectControllerCreate() { }
            internal static readonly DataObjectControllerGet instance = new DataObjectControllerGet();
        }

       

        public List<Kontragent> GetListKontragent()          //Список контрагентов
        {
            IEnumerable<Kontragent> result;
            result = _context.Kontragents.ToList();
            return (List<Kontragent>)result;
        }

        public Kontragent GetKontragent(String INN)          //Контрагент по значению поля
        {
            Kontragent result;
            result = _context.Kontragents.FirstOrDefault(k => k.KontragentINN == INN);
            return result;
        }

        public Kontragent GetObjectKontragent(IDataObject dataObject)
        {
            Kontragent result = null;

            if (dataObject is ConstructionObject co)
            {
                result = _context.Kontragents
                    .FirstOrDefault(k => k.ConstructionObjects.Any(coInner => coInner.ID == co.ID));
            }
            else if (dataObject is ResponsiblPerson rp)
            {
                result = _context.Kontragents
                    .FirstOrDefault(k => k.ResponsiblPersons.Any(rpInner => rpInner.ID == rp.ID));
            }

            return result;
        }


        public List<ConstructionObject> GetListConstructionObjects()          //Список объектов
        {
            IEnumerable<ConstructionObject> result;

            result = _context.ConstructionObjects.ToList();
            foreach (var item in result)
            {
                item.Customer = GetObjectKontragent(item);
            }
            return (List<ConstructionObject>)result;
        }

        public List<ResponsiblPerson> GetListPersons()          //Список ответственных лиц
        {
            IEnumerable<ResponsiblPerson> result;

            result = _context.ResponsiblPersons.ToList();
            foreach (var item in result)
            {
                item.PersonKontragent = GetObjectKontragent(item);
            }
            return (List<ResponsiblPerson>)result;
        }



        
    }
}
