using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interface
{
    public interface IKeyAccount : IKey
    {
        string username { get; set; }
    }
}
