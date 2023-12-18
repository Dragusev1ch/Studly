using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studly.BLL.Infrastructure.Exceptions
{
    public class NullDataException : Exception
    {
        public string MessageForUser { get; set; }

        public NullDataException(string exceptionMessage, string messageForUser) : base(exceptionMessage)
        {
            MessageForUser = messageForUser;
        }
    }
}
