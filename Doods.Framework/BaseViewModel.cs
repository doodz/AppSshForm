namespace Doods.Framework
{
    /// <summary>
    /// Base view model.
    /// </summary>
    public class BaseViewModel : ObservableObject
    {
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

        bool _isBusy;

        /// <summary>
        /// Obtient ou définit une valeur indiquant si cette instance est occupée.
        /// </summary>
        /// <value><c>true</c> Si cette instance est occupée; autrement,<c>false</c>.</value>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (SetProperty(ref _isBusy, value))
                    IsNotBusy = !_isBusy;
            }
        }

        bool _isNotBusy = true;

        /// <summary>
        /// Obtient ou définit une valeur indiquant si cette instance n'est pas occupée.
        /// </summary>
        /// <value><c>True</c> si cette instance n'est pas occupée; autrement, <c>false</c>.</value>
        public bool IsNotBusy
        {
            get { return _isNotBusy; }
            set
            {
                if (SetProperty(ref _isNotBusy, value))
                    IsBusy = !_isNotBusy;
            }
        }

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
    }
}
