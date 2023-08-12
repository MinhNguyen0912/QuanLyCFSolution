using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCF.BLL.ViewModels
{
    public class QRResponse
    {
        public string code { get; set; }
        public string desc { get; set; }
        public QRData data { get; set; }
    }
}
