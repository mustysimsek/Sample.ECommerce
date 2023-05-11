using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ECommerce.Application.Exceptions
{
    public class AppExtension : Exception
    {
        public AppExtension(string Message) : base(Message)
        {

        }
        public AppExtension(): this("Validation error occured")
        {

        }
        public AppExtension(Exception ex) : this(ex.Message)
        {

        }
    }
}
