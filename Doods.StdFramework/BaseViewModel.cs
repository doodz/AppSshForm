using System;
using System.Threading;
using System.Threading.Tasks;
using Doods.StdFramework;
using Doods.StdFramework.Interfaces;
using Doods.StdFramework.Mvvm;

namespace Doods.StdFramework
{
    /// <summary>
    /// Base view model.
    /// </summary>
    public class BaseViewModel : ObservableObject
    {
        private bool _isLoad;
        private int _busyCount;


        protected int BusyCount
        {
            get { return _busyCount; }
            set
            {
                SetProperty(ref _busyCount, value);
                SetPropertyChanged(nameof(IsBusy));
            }
        }
        private ViewModelState _viewModelState;
        public ViewModelState ViewModelState
        {
            get { return _viewModelState; }
            set { SetProperty(ref _viewModelState, value); }
        }
        private CancellationTokenSource _cts = new CancellationTokenSource();
        protected ILogger Logger { get; }
        protected CancellationToken Token => _cts.Token;

        protected BaseViewModel(ILogger logger)
        {
            Logger = logger;

        }

        string _title = string.Empty;

        /// <summary>
        /// Obtient ou définit le titre.
        /// </summary>
        /// <value>LE titre.</value>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        string subtitle = string.Empty;

        /// <summary>
        ///Obtient ou définit le sous-titre.
        /// </summary>
        /// <value>Le sous-titre..</value>
        public string Subtitle
        {
            get { return subtitle; }
            set { SetProperty(ref subtitle, value); }
        }

        string icon = string.Empty;

        /// <summary>
        ///Obtient ou définit l'icône.
        /// </summary>
        /// <value>L'icône.</value>
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }



        /// <summary>
        /// Obtient ou définit une valeur indiquant si cette instance est occupée.
        /// </summary>
        /// <value><c>true</c> Si cette instance est occupée; autrement,<c>false</c>.</value>
        public bool IsBusy => BusyCount > 0;




        /// <summary>
        /// Obtient ou définit une valeur indiquant si cette instance n'est pas occupée.
        /// </summary>
        /// <value><c>True</c> si cette instance n'est pas occupée; autrement, <c>false</c>.</value>
        public bool IsNotBusy => !IsBusy;
       

        bool canLoadMore = true;

        /// <summary>
        /// Obtient ou définit une valeur indiquant si cette instance peut charger plus.
        /// </summary>
        /// <value><c>true</c> si cette instance peut charger plus; autrement, <c>false</c>.</value>
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { SetProperty(ref canLoadMore, value); }
        }


        string _header = string.Empty;

        /// <summary>
        /// Obtient ou définit l'en-tête.
        /// </summary>
        /// <value>L'en-tête.</value>
        public string Header
        {
            get { return _header; }
            set { SetProperty(ref _header, value); }
        }

        string _footer = string.Empty;

        /// <summary>
        /// Obtient ou définit le pied de page.
        /// </summary>
        /// <value>Le pied de page.</value>
        public string Footer
        {
            get { return _footer; }
            set { SetProperty(ref _footer, value); }
        }

        public async Task OnAppearing()
        {
            if (_cts.IsCancellationRequested) _cts = new CancellationTokenSource();

            if (!_isLoad)
            {
                _isLoad = true;
                await StartLoading();
            }

            await OnInternalAppearing();
        }

        public async Task StartLoading()
        {
            ViewModelState = ViewModelState.Loading;
            BusyCount++;

            try
            {
                var delayTask = Task.Delay(TimeSpan.FromMilliseconds(50));
                var executeTask = ExecuteAsync(token => Load(), true);

                await Task.WhenAll(delayTask, executeTask);

                if (ViewModelState == ViewModelState.Loading)
                    ViewModelState = ViewModelState.Loaded;

            }
            catch (Exception e)
            {
                ViewModelState = ViewModelState.Failed;
                Logger.Error(e);
                throw;
            }
            finally
            {
                OnLoadingCallBack();
                    BusyCount--;
            }
        }
        protected async Task ExecuteAsync(Func<CancellationToken, Task> action, bool safeExecution = false)
        {
            try
            {
                Token.ThrowIfCancellationRequested();
                await action(Token);
            }
            catch (AggregateException e)
            {
                var innerException = e.InnerException;
                while (innerException.InnerException != null)
                    innerException = innerException.InnerException;

                //TODO : LOG + HokeyApp
                if (!safeExecution) throw;
            }
            catch (Exception e)
            {
                //TODO : LOG + HokeyApp
                if (!safeExecution) throw;
            }
        }
        protected virtual Task Load()
        {
            return Task.FromResult(0);
        }
        protected virtual Task OnInternalAppearing()
        {
            return Task.FromResult(0);
        }
        private void OnLoadingCallBack()
        {
            OnInternalLoadingCallBack();
        }

        protected virtual void OnInternalLoadingCallBack()
        {
            //NP
        }
        public Task OnDisappearing()
        {
            CancelExecutions();

            return OnInternalDisappearing();
        }

        protected virtual Task OnInternalDisappearing()
        {
            return Task.FromResult(0);
        }

        public void CancelExecutions()
        {
            if (Token.CanBeCanceled && !_cts.IsCancellationRequested)
                _cts.Cancel();
        }
    }
}
