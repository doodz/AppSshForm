using Doods.StdFramework.Mvvm;
using Doods.StdRepository.Base;

namespace ApptestSsh.Core.DataBase
{
    public class CommandSsh : TableBase, IName
    {

        public string Name { get; set; }
        public string CommandString { get; set; }

    }
}
