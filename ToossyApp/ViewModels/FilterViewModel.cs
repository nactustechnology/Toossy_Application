using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToossyApp.Services;
using Akavache;
using ToossyApp.Models;
using ToossyApp.Controls;
using Xamarin.Forms;
using System.Reactive.Linq;

namespace ToossyApp.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        List<CheckItem> _listeTheme = new List<CheckItem>();
        public List<CheckItem> ListeTheme
        {
            get { return _listeTheme; }
            set
            {
                _listeTheme = value;
                OnPropertyChanged();
            }
        }

        List<CheckItem> _listeDistance = new List<CheckItem>();
        public List<CheckItem> ListeDistance
        {
            get { return _listeDistance; }
            set
            {
                _listeDistance = value;
                OnPropertyChanged();
            }
        }


        List<CheckItem> _listeDuree = new List<CheckItem>();
        public List<CheckItem> ListeDuree
        {
            get { return _listeDuree; }
            set
            {
                _listeDuree = value;
                OnPropertyChanged();
            }
        }


        List<CheckItem> _listeTypeParcours = new List<CheckItem>();
        public List<CheckItem> ListeTypeParcours
        {
            get { return _listeTypeParcours; }
            set
            {
                _listeTypeParcours = value;
                OnPropertyChanged();
            }
        }


        List<CheckItem> _listeNote = new List<CheckItem>();
        public List<CheckItem> ListeNote
        {
            get { return _listeNote; }
            set
            {
                _listeNote = value;
                OnPropertyChanged();
            }
        }


        List<CheckItem> _listeGratuitTarif = new List<CheckItem>();
        public List<CheckItem> ListeGratuitTarif
        {
            get { return _listeGratuitTarif; }
            set
            {
                _listeGratuitTarif = value;
                OnPropertyChanged();
            }
        }


        List<CheckItem> _listeLangue = new List<CheckItem>();
        public List<CheckItem> ListeLangue
        {
            get { return _listeLangue; }
            set
            {
                _listeLangue = value;
                OnPropertyChanged();
            }
        }


        public FilterViewModel(INavService navService, IBlobCache cache) : base(navService)
        {
            //Dictionnary to get le lists' items
            //__________________THEME LIST____________________________________________
            var themeDictionary = ThemeDictionary.getDictionary();

            populateList(_listeTheme, themeDictionary);

            //__________________DISTANCE LIST_________________________________________
            var distanceDictionary = DistanceDictionary.getDictionary();
            
            populateList(_listeDistance, distanceDictionary);

            //__________________DUREE LIST____________________________________________
            var dureeDictionary = DureeDictionary.getDictionary();

            populateList(_listeDuree, dureeDictionary);

            //__________________TYPEPARCOURS LIST___________________________________
            var typeParcoursDictionary = TypeParcoursDictionary.getDictionary();
                
            populateList(_listeTypeParcours, typeParcoursDictionary);

            //_______________________NOTE LIST________________________________________
            var noteDictionnary = NoteDictionnary.getDictionary();
                
            populateList(_listeNote, noteDictionnary);

            //_______________________GRATUIT-TARIF LIST_______________________________
            var gratuitTarifDictionnary = GratuitTarifDictionary.getDictionary();
                
            populateList(_listeGratuitTarif, gratuitTarifDictionnary);

            //_______________________LANGUE LIST______________________________________
            var langueDictionary = LangueDictionary.getDictionary();
                
            populateList(_listeLangue, langueDictionary);
        }

        //_____________________________FILTRE DEFINITION_____________________________________________
        //******************************************************************************************

        string[] _filtreTheme;
        public string[] FiltreTheme
        {
            get { return _filtreTheme; }
            set
            {
                _filtreTheme = value;
                OnPropertyChanged();
            }
        }

        string _detailTheme;
        public string DetailTheme
        {
            get { return _detailTheme; }
            set
            {
                _detailTheme = value;
                OnPropertyChanged();
            }
        }

        string[] _filtreTypeParcours;
        public string[] FiltreTypeParcours
        {
            get { return _filtreTypeParcours; }
            set
            {
                _filtreTypeParcours = value;
                OnPropertyChanged();
            }
        }

        string _detailTypeParcours;
        public string DetailTypeParcours
        {
            get { return _detailTypeParcours; }
            set
            {
                _detailTypeParcours = value;
                OnPropertyChanged();
            }
        }

        string[] _filtreLangue;
        public string[] FiltreLangue
        {
            get { return _filtreLangue; }
            set
            {
                _filtreLangue = value;
                OnPropertyChanged();
            }
        }

        string _detailLangue;
        public string DetailLangue
        {
            get { return _detailLangue; }
            set
            {
                _detailLangue = value;
                OnPropertyChanged();
            }
        }

        string[] _filtreDistance;
        public string[] FiltreDistance
        {
            get { return _filtreDistance; }
            set
            {
                _filtreDistance = value;
                OnPropertyChanged();
            }
        }

        string _detailDistance;
        public string DetailDistance
        {
            get { return _detailDistance; }
            set
            {
                _detailDistance = value;
                OnPropertyChanged();
            }
        }



        string[] _filtreGratuitTarif;
        public string[] FiltreGratuitTarif
        {
            get { return _filtreGratuitTarif; }
            set
            {
                _filtreGratuitTarif = value;
                OnPropertyChanged();
            }
        }

        string _detailGratuitTarif;
        public string DetailGratuitTarif
        {
            get { return _detailGratuitTarif; }
            set
            {
                _detailGratuitTarif = value;
                OnPropertyChanged();
            }
        }

        string[] _filtreDuree;
        public string[] FiltreDuree
        {
            get { return _filtreDuree; }
            set
            {
                _filtreDuree = value;
                OnPropertyChanged();
            }
        }

        string _detailDuree;
        public string DetailDuree
        {
            get { return _detailDuree; }
            set
            {
                _detailDuree = value;
                OnPropertyChanged();
            }
        }

        string[] _filtreNote;
        public string[] FiltreNote
        {
            get { return _filtreNote; }
            set
            {
                _filtreNote = value;
                OnPropertyChanged();
            }
        }

        string _detailNote;
        public string DetailNote
        {
            get { return _detailNote; }
            set
            {
                _detailNote = value;
                OnPropertyChanged();
            }
        }

      

        public override async Task Init()
        {
            ItineraryFilter filtreInitialization = (ItineraryFilter)Application.Current.Properties["ItineraryFilter"];

            FiltreTheme = filtreInitialization.FiltreTheme;
            DetailTheme = String.Join(", ", ListeTheme.Where(item => Array.Exists(FiltreTheme, filtre => filtre == item.Clef)).Select(item => item.Name));

            FiltreDistance = filtreInitialization.FiltreDistance;
            DetailDistance = String.Join(", ", ListeDistance.Where(item => item.Clef == FiltreDistance[0]).Select(item => item.Name));

            FiltreDuree = filtreInitialization.FiltreDuree;
            DetailDuree = String.Join(", ", ListeDuree.Where(item => item.Clef == FiltreDuree[0]).Select(item => item.Name));

            FiltreTypeParcours = filtreInitialization.FiltreTypeParcours;
            DetailTypeParcours = String.Join(", ", ListeTypeParcours.Where(item => Array.Exists(FiltreTypeParcours, filtre => filtre == item.Clef)).Select(item => item.Name));

            FiltreNote = filtreInitialization.FiltreNote;
            DetailNote = String.Join(", ", ListeNote.Where(item => item.Clef == FiltreNote[0]).Select(item => item.Name));

            FiltreGratuitTarif = filtreInitialization.FiltreGratuitTarif;
            DetailGratuitTarif = String.Join(", ", ListeGratuitTarif.Where(item => item.Clef == FiltreGratuitTarif[0]).Select(item => item.Name));

            FiltreLangue = filtreInitialization.FiltreLangue;
            DetailLangue = String.Join(", ", ListeLangue.Where(item => Array.Exists(FiltreLangue, filtre => filtre == item.Clef)).Select(item => item.Name));
        }

        public void saveFilter()
        {
            Application.Current.Properties["ItineraryFilter"] = new ItineraryFilter
            {
                FiltreTheme = FiltreTheme,
                FiltreLangue = FiltreLangue,
                FiltreTypeParcours = FiltreTypeParcours,
                FiltreDistance = FiltreDistance,
                FiltreDuree = FiltreDuree,
                FiltreNote = FiltreNote,
                FiltreGratuitTarif = FiltreGratuitTarif
            };
        }

        void populateList(List<CheckItem> List, Dictionary<string, string> Dictionary)
        {
            foreach (var item in Dictionary)
            {
                List.Add(new CheckItem { Clef = item.Key, Name = item.Value });
            }
        }
    }
}
