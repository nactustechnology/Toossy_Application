using System.Threading.Tasks;
using ToossyApp.Services;

namespace ToossyApp.ViewModels
{
    public class TutorialViewModel : BaseViewModel
    {
        public TutorialViewModel(INavService navService) : base(navService)
        {
        }

        public override async Task Init()
        {
            IsLoading = true;
        }
    }
}
