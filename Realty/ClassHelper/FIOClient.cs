using Realty.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realty.EF
{
    public partial class Client
    {

        public string FIO { get => $"{Surname} {FirstName}"; }
    }
}
