using ApptestSsh.Core.DataBase;
using Doods.StdLibSsh.Interfaces;

namespace ApptestSsh.Core
{
    public interface ISshService : IClientSsh
    {

        Host Host { get; set; }
        void Initialise();
        bool IsInitialised { get; }
    }
}
