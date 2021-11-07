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

        public string BackgroundSoundName { get; set; }

        public string Backwashes { get; set; }

        public string Challenges { get; set; }

        /// <summary>
        ///     Длительность кадра для новелл.
        /// </summary>
        public float? Duration { get; set; }

        public string Gossips { get; set; }

        public string ImageName { get; set; }

        public Dictionary<Languages, string> LanguageToDecisionDict { get; set; }

        public Dictionary<Languages, string> LanguageToDescriptionDict
        {
            get => _languageToDescriptionDict?.ToDictionary(entry => entry.Key, entry => entry.Value);
            set => _languageToDescriptionDict = value;
        }

        public string MusicName { get; set; }

        public string Number { get; set; }

        public int RowNumber { get; set; }

        public string SingleSoundName { get; set; }
    }
}