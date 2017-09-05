using Doods.StdFramework.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Doods.StdFramework.Mvvm
{

    public interface IName
    {
        string Name { get; }
        //string Description { get; }
    }
    public class BaseListViewModel<T> : BaseViewModel where T : IName
    {

        protected bool _isBusyList;

        public bool IsBusyList
        {
            get => _isBusyList;
            set => SetProperty(ref _isBusyList, value);
        }

        public ObservableRangeCollection<T> Items { get; }
        public ObservableRangeCollection<Grouping<string, T>> ItemsGrouped { get; }
        public ICommand RefreshDataCommand { get; }

        public BaseListViewModel(ILogger logger) : base(logger)
        {
            Items = new ObservableRangeCollection<T>();

            var sorted = from item in Items
                         orderby item.Name
                         group item by item.Name[0].ToString()
                into itemGroup
                         select new Grouping<string, T>(itemGroup.Key, itemGroup);

            ItemsGrouped = new ObservableRangeCollection<Grouping<string, T>>(sorted);

            RefreshDataCommand = new Command(
                async () => await RefreshData());
        }
        protected override async Task Load()
        {
            await RefreshData();
        }
        protected virtual async Task RefreshData()
        {
            await Task.FromResult(0);
        }
    }
    public class Grouping<TK, T> : ObservableRangeCollection<T>
    {
        public TK Key { get; private set; }

        public Grouping(TK key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}