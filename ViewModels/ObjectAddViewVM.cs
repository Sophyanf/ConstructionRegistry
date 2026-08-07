using ConstructionRegistry.Enums;
using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Application = System.Windows.Application;

namespace ConstructionRegistry.ViewModels
{
    public class ObjectAddViewVM : BaseViewModel
    {
        private readonly IConstructionObjectService _objectService;
        private readonly IKontragentService _kontragentService;
        private readonly IContractCustomerService _contractService;
        private readonly IResponsiblPersonService _personService;

        public ActionCommand AddNewConctrObject { get; }

        #region Properties

        private string _objName = string.Empty;
        public string ObjName
        {
            get => _objName;
            set => UpdateValue(ref _objName, value);
        }

        private StatusOfObject _status = StatusOfObject.ApplicationOnly;
        public StatusOfObject Status
        {
            get => _status;
            set => UpdateValue(ref _status, value);
        }

        private double _costOfObject;
        public double CostOfObject
        {
            get => _costOfObject;
            set => UpdateValue(ref _costOfObject, value);
        }

        private DateTime _startDate = DateTime.Now.AddDays(30);
        public DateTime StartDate
        {
            get => _startDate;
            set => UpdateValue(ref _startDate, value);
        }

        private DateTime _endDate = DateTime.Now.AddDays(30);
        public DateTime EndDate
        {
            get => _endDate;
            set => UpdateValue(ref _endDate, value);
        }

        private Kontragent? _constructionOrganizationSub;
        public Kontragent? ConstructionOrganizationSub
        {
            get => _constructionOrganizationSub;
            set => UpdateValue(ref _constructionOrganizationSub, value);
        }

        private double _subContractCustomeringCoefficients;
        public double SubContractCustomeringCoefficients
        {
            get => _subContractCustomeringCoefficients;
            set => UpdateValue(ref _subContractCustomeringCoefficients, value);
        }

        private string _comment = string.Empty;
        public string Comment
        {
            get => _comment;
            set => UpdateValue(ref _comment, value);
        }

        private OriginDocumentStatus _originDocuments;
        public OriginDocumentStatus OriginDocuments
        {
            get => _originDocuments;
            set => UpdateValue(ref _originDocuments, value);
        }

        private OriginDocumentStatus _originDocumentsSub;
        public OriginDocumentStatus OriginDocumentsSub
        {
            get => _originDocumentsSub;
            set => UpdateValue(ref _originDocumentsSub, value);
        }

        private ResponsiblPerson? _responsiblPerson;
        public ResponsiblPerson? ResponsiblPerson
        {
            get => _responsiblPerson;
            set => UpdateValue(ref _responsiblPerson, value);
        }

        // Эти свойства обычно приходят из BaseViewModel, но можно продублировать сигнатуру
        public ObservableCollection<Kontragent> Kontragents { get; set; } = new();
        public ObservableCollection<ContractCustomer> ContractCustomersList { get; set; } = new();

        public Kontragent? Customer { get; set; } // выбранный заказчик
        public ContractCustomer? SelectContractCustomer { get; set; } // выбранный договор

        #endregion

        public ObjectAddViewVM(
            IConstructionObjectService objectService,
            IKontragentService kontragentService,
            IContractCustomerService contractService,
            IResponsiblPersonService personService)
        {
            _objectService = objectService;
            _kontragentService = kontragentService;
            _contractService = contractService;
            _personService = personService;

            AddNewConctrObject = new ActionCommand(_ => AddNewObjectAsync());

            LoadKontragentsAsync();
            LoadContractCustomersListAsync();
        }

        private async void LoadKontragentsAsync()
        {
            var list = await _kontragentService.GetAllAsync();
            Kontragents = new ObservableCollection<Kontragent>(list);
        }

        private async void LoadContractCustomersListAsync()
        {
            // Если Customer уже выбран, можно фильтровать по нему
            if (Customer != null)
            {
                var list = await _contractService.GetByKontragentIdAsync(Customer.ID);
                ContractCustomersList = new ObservableCollection<ContractCustomer>(list);
            }
        }

        private double CalculateSpendingOfObject(double costOfObject)
        {
            double cost = costOfObject * _subContractCustomeringCoefficients;

            if (!Customer?.NDSRate ?? true)
            {
                if (_endDate.Year < 2026)
                    cost = costOfObject * 100.0 / 120.0 * _subContractCustomeringCoefficients;
                else
                    cost = costOfObject * 100.0 / 122.0 * _subContractCustomeringCoefficients;
            }
            return cost;
        }

        private async void AddNewObjectAsync()
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

            // Получаем основную организацию по ИНН (лучше вынести в сервис или конфиг)
            var constructionOrg = await _kontragentService.GetByInnAsync("5321171110");
            if (constructionOrg == null)
            {
                MessageBox.Show("Не найдена основная строительная организация (ИНН 5321171110).");
                return;
            }

            var newObject = new ConstructionObject
            {
                ObjectName = ObjName,
                ConstructionOrganization = constructionOrg,
                ConstructionOrganizationSub = ConstructionOrganizationSub,
                Customer = Customer,
                DateOfApplication = _startDate,
                EndDate = _endDate,
                CostOfObject = _costOfObject,
                SpendingOfObject = CalculateSpendingOfObject(_costOfObject),
                CustomerOrgRespPerson = _responsiblPerson,
                Status = _status,
                Comment = _comment,
                OriginDocuments = _originDocuments,
                OriginDocumentsSub = _originDocumentsSub,
                ContractCustomer = SelectContractCustomer,
                SubContractCustomeringCoefficients = _subContractCustomeringCoefficients,
            };

            try
            {
                await _objectService.AddAsync(newObject);
                MessageBox.Show("Объект успешно создан!");
                CloseCurrentWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении объекта: {ex.Message}");
            }
        }

        private void CloseCurrentWindow()
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            activeWindow?.Close();
        }
    }
}
