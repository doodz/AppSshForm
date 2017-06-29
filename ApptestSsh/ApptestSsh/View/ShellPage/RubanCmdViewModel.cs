using System.Windows.Input;
using Doods.StdFramework;
using Doods.StdLibSsh;
using Xamarin.Forms;

namespace ApptestSsh.Core.View.ShellPage
{
    public class RubanCmdViewModel : ObservableObject
    {
        public ICommand CmdEnter { get; }
        
        public RubanCmdViewModel()
        {
          CmdEnter = new Command(CmdKey);
        }

        public string CmdStr { get; private set; }

        private void CmdKey(object p)
        {
            if (p is string s)
            {
                CmdStr = getCmd(s);
                SetPropertyChanged(nameof(CmdStr));
            }
                
        }


        private string getCmd(string cmd)
        {

            switch (cmd.ToLower())
            {
                case "enter":
                    return KeyboardKeys.Enter;
                case "left":
                    return KeyboardKeys.Arrow_Left;
                case "right":
                    return KeyboardKeys.Arrow_Right;
                case "tab":
                    return KeyboardKeys.Tab;
                case "up":
                    return KeyboardKeys.Arrow_Up;
                default:
                    return string.Empty;
            }
        }
    }
}
