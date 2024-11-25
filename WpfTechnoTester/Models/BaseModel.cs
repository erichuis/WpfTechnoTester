namespace WpfTechnoTester.Models
{
    public class BaseModel
    {
        private readonly List<string> _failMessages = [];
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
            return !_failMessages.Any();
        }

        public void Reset()
        {
            _failMessages.Clear();
        }

        public List<string> FailMessages()
        {
            return _failMessages;
        }
    }
}
