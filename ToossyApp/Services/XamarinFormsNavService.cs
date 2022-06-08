using System;
using System.ComponentModel;
using System.Threading.Tasks;
using ToossyApp.ViewModels;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace ToossyApp.Services
{
    public class XamarinFormsNavService : INavService
    {
        public INavigation XamarinFormsNav { get; set; }

        readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _map.Add(viewModel, view);
        }

        public async Task ClearBackStack()
        {
            if (XamarinFormsNav.NavigationStack.Count <= 1)
                return;

            for (var i = 0; i < XamarinFormsNav.NavigationStack.Count - 1; i++)
                XamarinFormsNav.RemovePage(XamarinFormsNav.NavigationStack[i]);
        }

        public async Task GoBack()
        {
            if (CanGoback) {
                await XamarinFormsNav.PopAsync(true);
            };

            OnCanGoBackChanged();
        }

        public async Task GoToRootPage()
        {
            if (CanGoback)
            {
                await XamarinFormsNav.PopToRootAsync(true);
            };

            OnCanGoBackChanged();
        }

        public bool CanGoback
        {
            get
            {
                return XamarinFormsNav.NavigationStack != null && XamarinFormsNav.NavigationStack.Count > 0;
            }
        }

        void OnCanGoBackChanged()
        {
            var handler = CanGoBackChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs("CanGoBack"));
        }

        public event PropertyChangedEventHandler CanGoBackChanged;

        public async Task NavigateTo<TVM>() where TVM : BaseViewModel
        {
            try
            {
                await NavigateToView(typeof(TVM));

                if (XamarinFormsNav.NavigationStack.Last().BindingContext is BaseViewModel)
                    await ((BaseViewModel)(XamarinFormsNav.NavigationStack.Last().BindingContext)).Init();
            }
            catch(Exception ex)
            {

            }
        }

        public async Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel
        {
            try
            {
                await NavigateToView(typeof(TVM));

                if (XamarinFormsNav.NavigationStack.Last().BindingContext is BaseViewModel<TParameter>)
                    await ((BaseViewModel<TParameter>)(XamarinFormsNav.NavigationStack.Last().BindingContext)).Init(parameter);
            }
            catch(Exception ex)
            {

            }
        }

        async Task NavigateToView(Type viewModelType)
        {
            try
            {
                Type viewType;

                if (!_map.TryGetValue(viewModelType, out viewType))
                    throw new ArgumentException("No view found in View Mapping for " + viewModelType.FullName + ".");

                var constructor = viewType.GetTypeInfo().DeclaredConstructors.FirstOrDefault(dc => dc.GetParameters().Count() <= 0);

                var view = constructor.Invoke(null) as Page;

                var vm = ((App)Application.Current).Kernel.GetService(viewModelType);

                view.BindingContext = vm;

                await XamarinFormsNav.PushAsync(view, true);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task NavigateToUri(Uri uri)
        {
            if (uri == null)
                throw new ArgumentException("Invalid URI");

            Device.OpenUri(uri);
        }

        public async Task RemoveLastView()
        {
            if (XamarinFormsNav.NavigationStack.Any())
            {
                var lastView = XamarinFormsNav.NavigationStack[XamarinFormsNav.NavigationStack.Count - 2];
                XamarinFormsNav.RemovePage(lastView);
            }
        }
    }
}
