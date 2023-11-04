using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Models
{
    public class Student: ViewModelBase
    {
        private int id;
        private string name;

        public int Id
        {
            get { return id; }
            set
            {
                id = value; 
                RaisePropertyChanged();  // 通知属性更改
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value; RaisePropertyChanged(); // 通知属性更改
            }
        }
    }
}
