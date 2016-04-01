using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo_to_Clipboard
{
    class AccessToken
    {
        private string _token = string.Empty;
        private DateTime _generationTime;

        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                _generationTime = DateTime.Now;
            }
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(_token)) return false;

                return (DateTime.Now - _generationTime).TotalMinutes < 60;
            }
        }
    }
}
