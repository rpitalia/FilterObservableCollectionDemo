using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace FilterObservableCollection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<City> cityList = new ObservableCollection<City>();
        private string searchText;

        public ObservableCollection<City> CityList
        {
            get { return cityList; }
            set
            {
                cityList = value;
                OnPropertyChanged("CityList");
            }
        }
        public ICollectionView CityListView
        {
            get { return CollectionViewSource.GetDefaultView(CityList); }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                if (value != searchText)
                {
                    searchText = value;
                    OnPropertyChanged("SearchText");
                    CityListView.Refresh();
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            populateData();
            CityListView.Filter = new Predicate<object>(o => SearchData(o as City));
        }

        private void populateData()
        {
            CityList.Add(new City { ID = 1, Name = "New York" });
            CityList.Add(new City { ID = 2, Name = "Boston" });
            CityList.Add(new City { ID = 3, Name = "Seattle" });
            CityList.Add(new City { ID = 4, Name = "Los Angeles" });
            CityList.Add(new City { ID = 5, Name = "Houston" });
        }

        private bool SearchData(City city)
        {
            return SearchText == null
                || city.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) != -1;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);

        }

        #endregion
    }
}
