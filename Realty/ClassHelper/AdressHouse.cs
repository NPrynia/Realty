using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realty.EF
{
    public partial class House
    {
        public string adressHouse { get => $"{Street.City.Name}, {Street.Name}, {NumberHouse}"; }
    }
}
