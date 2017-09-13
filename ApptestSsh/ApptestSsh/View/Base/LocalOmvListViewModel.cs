using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;
using Doods.StdFramework.Views.PopupPages;
using Omv.Rpc.StdClient.Clients;
using Omv.Rpc.StdClient.Datas;
using Omv.Rpc.StdClient.Services;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;

namespace ApptestSsh.Core.View.Base
{
    public class LocalOmvListViewModel<T> : LocalListViewModel<T> where T : IName
    {
        public LocalOmvListViewModel(ILogger logger) : base(logger)
        {
        }

        protected async Task ApplyChanges(ISshService ssh, bool showProgress)
        {
            if (showProgress)
            {
                var progressTask = ShowProgress();
            }
            var cmd = ConfigService.CreateApplyChangesBgCommand();
            var res = await new OmvRpcQuery<string>(ssh, cmd).RunAsync(Token);

            if (res != null)
            {
                await IsRunning(ssh, res);
            }
            if (_progressContentView != null)
            {
                _inputAlertDialogBase.PageClosedTaskCompletionSource
                    .SetResult(ProgressContentViewState.Finished);
            }
        }

        private int _waitRunning = 1000;
        private async Task IsRunning(ISshService ssh, string res)
        {
            var cmd = ExecService.CreateIsRunningCommand(res);
            var data = await new OmvRpcQuery<IsRunningData>(ssh, cmd).RunAsync(Token);
            if (data != null)
            {
                if (data.Running)
                {
                    Task.Delay(_waitRunning).Wait();
                    await IsRunning(ssh, data.Filename);
                    _waitRunning += 1000;
                    if (_waitRunning > 5000)
                        _waitRunning = 1000;
                }
                else
                {
                    _waitRunning = 1000;
                }
            }
        }

        private ProgressContentView _progressContentView;
        private InputAlertDialogBase<ProgressContentViewState> _inputAlertDialogBase;
        private async Task ShowProgress()
        {
            // create the TextInputView
            _progressContentView = new ProgressContentView("Apply changes");
            // create the Transparent Popup Page
            // of type string since we need a string return
            _inputAlertDialogBase = new InputAlertDialogBase<ProgressContentViewState>(_progressContentView);

            // subscribe to the TextInputView's Button click event
            _progressContentView.CloseButtonEventHandler +=
                (sender, obj) =>
                {
                    if (sender is ProgressContentView view)
                    {
                        // update the page completion source
                        _inputAlertDialogBase.PageClosedTaskCompletionSource
                            .SetResult(ProgressContentViewState.Closed);
                    }
                };

            // Push the page to Navigation Stack
            await NavigationService.Navigation.PushPopupAsync(_inputAlertDialogBase);

            // await for the user to enter the text input
            var result = await _inputAlertDialogBase.PageClosedTask;

            // Pop the page from Navigation Stack
            await NavigationService.Navigation.PopPopupAsync();
            _progressContentView = null;
            _inputAlertDialogBase = null;
            // return user inserted text value
            //return result;

        }
    }
}