using System.Windows;
using WpfApp2.Models;

namespace WpfApp2.Views
{
    /// <summary>
    /// UserView.xaml 的交互逻辑
    /// </summary>
    public partial class UserView : Window
    {
        public UserView(Student stu)
        {
            InitializeComponent();
            this.DataContext = new
            {
                Model = stu
            };
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;
        }
    }
}
