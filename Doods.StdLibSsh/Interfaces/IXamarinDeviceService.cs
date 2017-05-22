using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doods.StdLibSsh.Interfaces
{
    public interface IXamarinDeviceService
    {
        Task Call(string tel);

        Task Email(string adresse);

        Task NavigateToAdresse(string adresse);

        Task NavigateToPosition(double latitude, double longitude);
    }
}
