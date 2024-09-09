using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingModule.DataAccess.Response
{
    public class ResultSet<T> : BaseResponse
    {
        public T Results { get; set; }
    }
}
