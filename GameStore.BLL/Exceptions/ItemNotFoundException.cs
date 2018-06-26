using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() : base() { }
        public ItemNotFoundException(string message) : base(message) { }
    }
}
