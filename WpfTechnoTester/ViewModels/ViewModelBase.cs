using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTechnoTester.ViewModels.Helpers;

namespace WpfTechnoTester.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        public ViewModelBase() { }
        public ViewModelBase(string displayName)
        {
            DisplayName = displayName;
        }

        public string? DisplayName { get; set; }

        // In ViewModelBase.cs 
        
        internal virtual void RaiseCanExecuteChange()
        {
        }

        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real, 
            // public, instance property on this object. 
            if (propertyName != "Password" && TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                throw new Exception(msg);
                //else
                //    Debug.Fail(msg);
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            base.OnPropertyChanged(propertyName);
            RaiseCanExecuteChange();
        }
    }
}
