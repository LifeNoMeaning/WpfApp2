using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using WpfApp2.Db;
using WpfApp2.Models;
using WpfApp2.Views;
using System.Linq;
using System.Windows;

namespace WpfApp2.ViewModel
{
    /// <summary>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            localDb = new LocalDb();
            QueryCommand = new RelayCommand(Query);
            ResetCommand = new RelayCommand(() =>
            {
                Search = string.Empty;
                this.Query();

            });
            EditCommand = new RelayCommand<int>(t => Edit(t));
            DelCommand = new RelayCommand<int>(t => Del(t));
            AddCommand = new RelayCommand(Add);

        }

        LocalDb localDb;

        private string search = string.Empty;
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        // 动态数据集，可在添加、删除后发出通知
        private ObservableCollection<Student> gridModelList;  

        public ObservableCollection<Student> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }

        #region Command

        public RelayCommand QueryCommand { get; set; }

        public RelayCommand ResetCommand { get; set; }

        public RelayCommand<int> EditCommand { get; set; }
        public RelayCommand<int> DelCommand { get; set; }


        public RelayCommand AddCommand { get; set; }

        #endregion

        public void Query()
        {
            var models = localDb.GetStudentsByName(Search);
            GridModelList = new ObservableCollection<Student>();
            if (models != null)
            {
                models.ForEach(arg =>
                {
                    GridModelList.Add(arg);
                });
            }
        }

        public void Add()
        {
            Student newStu = new Student()
            {
                Id = localDb.GetNextId(),
                Name = string.Empty
            };
            UserView view = new UserView(newStu);
            var r = view.ShowDialog();
            if (r.Value)
            {
                localDb.AddStudent(newStu);
            }
            this.Query();
        }

        public void Edit(int id)
        {
            var model = localDb.GetStudentById(id);
            if (model != null)
            {
                UserView view = new UserView(model);
                var r = view.ShowDialog();
                if (r.Value)
                {
                    var newModel = GridModelList.FirstOrDefault(t => t.Id == model.Id);
                    if(newModel != null)
                    {
                        newModel.Name = model.Name;
                    }
                }
            }
        }

        public void Del(int id)
        {
            var model = localDb.GetStudentById(id);
            if(model != null)
            {
                var r = MessageBox.Show($"确认删除当前用户：{model.Name}", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
                if(r == MessageBoxResult.OK)
                {
                    localDb.DelStudent(model.Id);
                }
                this.Query();
            }


        }
    }
}