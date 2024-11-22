using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechnoTester.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase() { }
        public ViewModelBase(string displayName)
        {
            DisplayName = displayName;
        }

        public string? DisplayName { get; set; }

        // In ViewModelBase.cs 
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            if(PropertyChanged != null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        //[Conditional("DEBUG")]
        //[DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real, 
            // public, instance property on this object. 
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                throw new Exception(msg);
                //else
                //    Debug.Fail(msg);
            }
        }
    }
}
