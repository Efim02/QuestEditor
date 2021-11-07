using System.Collections.Generic;
using System.Linq;
using QuestEditor.Xlsx;

namespace QuestEditor.Vms
{
    /// <summary>
    ///     VM для шага.
    /// </summary>
    public class RawStepVM : ViewModelBase
    {
        private Dictionary<Languages, string> _languageToDescriptionDict;

        /// <summary>
        ///     Номер шага.
        /// </summary>
        private string _number;

        public string BackgroundSoundName { get; set; }

        public string Backwashes { get; set; }

        public string Challenges { get; set; }

        /// <summary>
        ///     Длительность кадра для новелл.
        /// </summary>
        public float? Duration { get; set; }

        public string Gossips { get; set; }

        public string ImageName { get; set; }

        public string EnglishDecision
        {
            get => LanguageToDecisionDict[Languages.English];
            set => LanguageToDecisionDict[Languages.English] = value;
        }

        public string RussianDecision
        {
            get => LanguageToDecisionDict[Languages.Russian];
            set => LanguageToDecisionDict[Languages.Russian] = value;
        }

        public Dictionary<Languages, string> LanguageToDecisionDict { get; set; }

        public string EnglishDescription
        {
            get => LanguageToDescriptionDict[Languages.English];
            set => LanguageToDescriptionDict[Languages.English] = value;
        }

        public string RussianDescription
        {
            get => LanguageToDescriptionDict[Languages.Russian];
            set => LanguageToDescriptionDict[Languages.Russian] = value;
        }

        public Dictionary<Languages, string> LanguageToDescriptionDict
        {
            get => _languageToDescriptionDict?.ToDictionary(entry => entry.Key, entry => entry.Value);
            set => _languageToDescriptionDict = value;
        }

        public string MusicName { get; set; }

        /// <inheritdoc cref="_number" />
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        public int RowNumber { get; set; }

        public string SingleSoundName { get; set; }
    }
}