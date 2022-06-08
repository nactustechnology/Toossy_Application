using ToossyApp.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ToossyApp.Services
{
    public interface INavService
    {
        bool CanGoback { get; }
        Task GoBack();
        Task GoToRootPage();
        Task NavigateTo<TVM>() where TVM : BaseViewModel;
        Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel;
        Task RemoveLastView();
        Task ClearBackStack();
        Task NavigateToUri(Uri uri);

        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
