using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Exceptions
{
    public class PublisherNotFoundException : Exception
    {
        public PublisherNotFoundException() : base() { }
        public PublisherNotFoundException(string message) : base(message) { }
    }
}
