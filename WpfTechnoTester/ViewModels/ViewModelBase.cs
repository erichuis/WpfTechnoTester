using System.Collections;
using System.ComponentModel;
using System.Xml.Linq;
using WpfTechnoTester.ViewModels.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfTechnoTester.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;
    public class ViewModelBase : ObservableObject, INotifyDataErrorInfo
    {
        public ViewModelBase() { }
        public ViewModelBase(string displayName)
        {
            DisplayName = displayName;
        }

        private readonly Dictionary<string, List<string>> _errors = new();

        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors.TryGetValue(propertyName??string.Empty, out var errors))
            {
                return errors;
            }
            return string.Empty;
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public string? DisplayName { get; set; }

        public bool HasErrors => _errors.Count > 0;


        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

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
            RemoveError(propertyName);
            RaiseCanExecuteChange();
        }

        private void RemoveError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors[propertyName].Clear();
            }
            _errors.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
