using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechnoTester.Models
{
    public class BaseModel
    {
        private List<string> _failMessages = [];
        public void CheckIsValid(Func<bool> check, string failMessage)
        {
            if(check == null) throw new ArgumentNullException(nameof(check));

            var message = string.Empty;
            
            if (string.IsNullOrEmpty(failMessage))
                 message = "This object is not valid";
            else
            {
                message = failMessage;
            }

            if(!check.Invoke())
            {
                _failMessages.Add(message);
            }
            
        }

        public bool IsValid()
        {
            return _failMessages.Any();
        }

        public List<string> FailMessages()
        {
            return _failMessages;
        }
    }
}
