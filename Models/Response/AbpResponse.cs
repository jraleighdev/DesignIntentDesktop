using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignIntentDesktop.Models.Response
{
    public class AbpResponse<T> where T : class
    {
        public T Result { get; set; }
    }
}
