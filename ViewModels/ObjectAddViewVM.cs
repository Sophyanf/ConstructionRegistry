using ConstructionRegistry.Controllers;
using ConstructionRegistry.Enums;
using ConstructionRegistry.Models;
using ConstructionRegistry.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Application = System.Windows.Application;
using Window = System.Windows.Window;

namespace ConstructionRegistry.ViewModels   // <-- только один раз
{
    public class ObjectAddViewVM : BaseViewModel
        {
        #region Properties

            // --- ObjectName ---
            private string objName = string.Empty;
            public string ObjName
            {
                get => objName;
                set => UpdateValue(ref objName, value);
            }

            // --- Status ---
            private StatusOfObject status = StatusOfObject.ApplicationOnly;
            public StatusOfObject Status 
            {
                get => status;
                set => UpdateValue(ref status, value);
            }

            // --- CostOfObject ---
            private double сostOfObject;
                public double CostOfObject
            {
                    get => сostOfObject;
                    set => UpdateValue(ref сostOfObject, value);
                }

            // --- DateOfApplication ---
            private DateTime startDate = DateTime.Now.AddDays(30);
            public DateTime StartDate
        {
                get => startDate;
                set => UpdateValue(ref startDate, value);
            }

            // --- EndDate ---
            private DateTime endDate = DateTime.Now.AddDays(30);
            public DateTime EndDate
            {
                get => endDate;
                set => UpdateValue(ref endDate, value);
            }

            // --- constructionOrganizationSub ---
            private Kontragent? constructionOrganizationSub;
            public Kontragent? ConstructionOrganizationSub
            {
                get => constructionOrganizationSub;
                set => UpdateValue(ref constructionOrganizationSub, value);
            }

            private ObservableCollection<Contract> contractsList = new ObservableCollection<Contract>();
            public ObservableCollection<Contract> ContractsList
            {
                get => contractsList;
                set => UpdateValue(ref contractsList, value);
            }

            // Выбранный контракт (то, что пойдёт в ConstructionObject)
            private Contract? contract;
            public Contract? Contract
            {
                get => contract;
                set => UpdateValue(ref contract, value);
            }

            private double subcontractingCoefficients;
            public double SubcontractingCoefficients
               {
                get => subcontractingCoefficients;
                set => UpdateValue(ref subcontractingCoefficients, value);
                }

            private String comment;
            public String Comment
            {
                get => comment;
                set => UpdateValue(ref comment, value);
            }

        private OriginDocumentStatus originDocuments;
        public OriginDocumentStatus OriginDocuments
        {
            get => originDocuments;
            set => UpdateValue(ref originDocuments, value);
        }

        private OriginDocumentStatus originDocumentsSub;
        public OriginDocumentStatus OriginDocumentsSub
        {
            get => originDocumentsSub;
            set => UpdateValue(ref originDocumentsSub, value);
        }

        private ResponsiblPerson responsiblPerson;
        public ResponsiblPerson ResponsiblPerson
        {
            get => responsiblPerson;
            set => UpdateValue(ref responsiblPerson, value);
        }

        #endregion

        #region Commands

        public ActionCommand AddNewConctrObject { get; set; }

            public ObjectAddViewVM()
            {
                AddNewConctrObject = new ActionCommand(x => AddNewObjectAsync());

                Kontragents = new ObservableCollection<Kontragent>();
                LoadKontragents();

                ContractsList = new ObservableCollection<Contract>();
                LoadContracts();

            }

            #endregion

        private double SpendingOfObject (double costOfObject)
        {
            double cost = costOfObject * subcontractingCoefficients;
            if (!Customer.NDSRate)
            {
                if (endDate.Year < 2026) cost = costOfObject * 100/120 * subcontractingCoefficients;
                else cost = costOfObject * 100 / 122 * subcontractingCoefficients;
            }
            return cost;
        }

            private void LoadContracts()
            {
                // Здесь ты подключаешь свою логику загрузки (EF6 / EF Core / репозиторий)
                // Пример для EF6 (т.к. ты работал с EF6):
                using (var ctx = new AppDbContext())
                {
                    var items = ctx.Contracts
                        .OrderBy(c => c.ContractNumber)
                        .ToList();

                    ContractsList.Clear();
                    foreach (var c in items)
                    {
                        ContractsList.Add(c);
                    }
                }
            }

            private async Task AddNewObjectAsync()
            {
                if (string.IsNullOrWhiteSpace(ObjName))
                {
                    MessageBox.Show("Введите наименование объекта.");
                    return;
                }

                if (Customer == null)
                {
                    MessageBox.Show("Выберите заказчика из списка.");
                    return;
                }

            var newObject = new ConstructionObject
            {
                ObjectName = ObjName,
                //ObjectAdress из ИИ
                ConstructionOrganization = dataObjGet.GetKontragent("5321171110"),
                ConstructionOrganizationSub = ConstructionOrganizationSub,
                Customer = Customer,
                DateOfApplication = startDate,
                EndDate = EndDate,
                CostOfObject = сostOfObject,
                SpendingOfObject = SpendingOfObject(сostOfObject),
                //KadastrID = из ИИ
                //TypeOfObject = из ИИ
                CustomerOrgRespPerson = responsiblPerson,
                Status = Status,
                Comment = comment,
                OriginDocuments = OriginDocuments,
                OriginDocumentsSub = OriginDocumentsSub,
                //PaymentInvoice = из ИИ 1C
                //Invoice = из ИИ 1C
                Contract = Contract,
                SubcontractingCoefficients = SubcontractingCoefficients,
            };

                try
                {
                    bool result = await Task.Run(() => dataObjAdd.AddObjectAsync(newObject, Customer));
                    if (result)
                    {
                        MessageBox.Show("Объект успешно создан!");
                        CloseCurrentWindow();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось сохранить объект. Проверьте логи.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }

            private void CloseCurrentWindow()
            {
                var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
                if (activeWindow != null)
                {
                    activeWindow.Close();
                }
            }
        }
    }


