using DanishMovies.Models;
using I18NPortable;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class InfoListViewModel : BaseViewModel
    {
        private readonly SearchTypes _infoType;

        public InfoListViewModel(IEnumerable<BaseInfo> baseInfoList, SearchTypes infoType)
        {
            BaseInfoList.Clear();
            foreach (var bil in baseInfoList)
            {
                BaseInfoList.Add(bil);
            }

            _infoType = infoType;
            switch (_infoType)
            {
                case SearchTypes.na:
                case SearchTypes.Movie:
                    Title = "Movies".Translate();
                    break;
                case SearchTypes.Person:
                    Title = "Actors".Translate();
                    break;
                case SearchTypes.Company:
                    Title = "Companies".Translate();
                    break;
                default:
                    break;
            }
        }

        public InfoListViewModel()
        {

        }

        public ObservableCollection<BaseInfo> BaseInfoList { get; set; } = 
            new ObservableCollection<BaseInfo>();

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ICommand _homeCommand;
        public ICommand HomeCommand => _homeCommand ?? (_homeCommand =
            new Command(async () => await Navigation.GoBack(false, true)));

        private ICommand _itemSelectedCommand;
        public ICommand ItemSelectedCommand
        {
            get
            {
                return _itemSelectedCommand ?? (_itemSelectedCommand =
                    new Command<BaseInfo>(async (info) =>
                    {
                        if (info == null) return;

                        switch (_infoType)
                        {
                            case SearchTypes.na:
                            case SearchTypes.Movie:
                                await Navigation.NavigateTo(
                                    "MoviePage", 
                                    new MovieViewModel(info.Id));
                                break;
                            case SearchTypes.Person:
                                await Navigation.NavigateTo(
                                    "PersonPage",
                                    new PersonViewModel(info.Id));
                                break;
                            case SearchTypes.Company:
                                await Navigation.NavigateTo(
                                    "CompanyPage",
                                    new CompanyViewModel(info.Id));
                                break;
                            default:
                                break;
                        }
                    }));
            }
        }
    }
}
