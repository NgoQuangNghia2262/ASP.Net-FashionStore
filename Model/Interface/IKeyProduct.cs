using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interface
{
    public interface IKeyProduct : IKey
    {
        string name { get; set; }
        string size { get; set; }
        string color { get; set; }
    }
}
