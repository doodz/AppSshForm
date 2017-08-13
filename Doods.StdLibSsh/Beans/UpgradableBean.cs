using Doods.StdFramework.Mvvm;

namespace Doods.StdLibSsh.Beans
{
    public class UpgradableBean : IName
    {
        public string Name { get; set; }
        public string NewVersion { get; set; }
        public string HoldHold { get; set; }
        public string FromRepo { get; set; }
        public string Platform { get; set; }
    }
}
