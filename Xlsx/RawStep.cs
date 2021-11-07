using System.Collections.Generic;
using System.Linq;

namespace QuestEditor.Xlsx
{
    public class RawStep
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

        /// <summary>
        ///     Для шага событияю
        /// </summary>
        public static RawStep GetEventRawStep(XlsxParser xlsxParser, int stepRowIndex, string questName)
        {
            var number = xlsxParser.GetCell(stepRowIndex, 1);

            if (string.IsNullOrEmpty(number))
                return null;

            return new RawStep
            {
                RowNumber = stepRowIndex,
                Number = number,
                LanguageToDecisionDict = xlsxParser.GetCellDict(stepRowIndex, 2),
                LanguageToDescriptionDict = xlsxParser.GetCellDict(stepRowIndex, 3),
                Challenges = xlsxParser.GetCell(stepRowIndex, 4),
                Backwashes = xlsxParser.GetCell(stepRowIndex, 5),
                Gossips = xlsxParser.GetCell(stepRowIndex, 6),
                ImageName = xlsxParser.GetCell(stepRowIndex, 7),
                MusicName = xlsxParser.GetCell(stepRowIndex, 8),
                BackgroundSoundName = xlsxParser.GetCell(stepRowIndex, 9),
                SingleSoundName = xlsxParser.GetCell(stepRowIndex, 10),
                Duration = xlsxParser.GetCellFloat(stepRowIndex, 11, questName)
            };
        }

        /// <summary>
        ///     Для шага событийного квеста.
        /// </summary>
        public static void SetEventRawStep(XlsxParser xlsxParser, RawStep rawStep, int stepRowIndex)
        {
            xlsxParser.SetCell(stepRowIndex, 1, rawStep.Number);
            xlsxParser.SetCellDict(stepRowIndex, 2, rawStep.LanguageToDecisionDict);
            xlsxParser.SetCellDict(stepRowIndex, 3, rawStep.LanguageToDescriptionDict);

            xlsxParser.SetCell(stepRowIndex, 4, rawStep.Challenges);
            xlsxParser.SetCell(stepRowIndex, 5, rawStep.Backwashes);
            xlsxParser.SetCell(stepRowIndex, 6, rawStep.Gossips);
            xlsxParser.SetCell(stepRowIndex, 7, rawStep.ImageName);
            xlsxParser.SetCell(stepRowIndex, 8, rawStep.MusicName);
            xlsxParser.SetCell(stepRowIndex, 9, rawStep.BackgroundSoundName);
            xlsxParser.SetCell(stepRowIndex, 10, rawStep.SingleSoundName);
            xlsxParser.SetCell(stepRowIndex, 11, rawStep.Duration);
        }

        /// <summary>
        ///     Для шага партийных квестов и новелл.
        /// </summary>
        internal static RawStep GetRawStep(XlsxParser xlsxParser, int stepRowIndex, string questName)
        {
            var number = xlsxParser.GetCell(stepRowIndex, 1);

            if (string.IsNullOrEmpty(number))
                return null;

            return new RawStep
            {
                RowNumber = stepRowIndex,
                Number = number,
                LanguageToDecisionDict = xlsxParser.GetCellDict(stepRowIndex, 2),
                LanguageToDescriptionDict = xlsxParser.GetCellDict(stepRowIndex, 3),
                Challenges = xlsxParser.GetCell(stepRowIndex, 4),
                Backwashes = xlsxParser.GetCell(stepRowIndex, 5),
                Gossips = xlsxParser.GetCell(stepRowIndex, 6),
                ImageName = xlsxParser.GetCell(stepRowIndex, 7),
                MusicName = xlsxParser.GetCell(stepRowIndex, 8),
                BackgroundSoundName = xlsxParser.GetCell(stepRowIndex, 9),
                SingleSoundName = xlsxParser.GetCell(stepRowIndex, 10),
                Duration = xlsxParser.GetCellFloat(stepRowIndex, 11, questName)
            };
        }

        /// <summary>
        ///     Для шага партийного квеста.
        /// </summary>
        public static void SetPartyRawStep(XlsxParser xlsxParser, RawStep rawStep, int stepRowIndex)
        {
            xlsxParser.SetCell(stepRowIndex, 1, rawStep.Number);
            xlsxParser.SetCellDict(stepRowIndex, 2, rawStep.LanguageToDecisionDict);
            xlsxParser.SetCellDict(stepRowIndex, 3, rawStep.LanguageToDescriptionDict);

            xlsxParser.SetCell(stepRowIndex, 4, rawStep.Challenges);
            xlsxParser.SetCell(stepRowIndex, 5, rawStep.Backwashes);
            xlsxParser.SetCell(stepRowIndex, 6, rawStep.Gossips);
            xlsxParser.SetCell(stepRowIndex, 7, rawStep.ImageName);
            xlsxParser.SetCell(stepRowIndex, 8, rawStep.MusicName);
            xlsxParser.SetCell(stepRowIndex, 9, rawStep.BackgroundSoundName);
            xlsxParser.SetCell(stepRowIndex, 10, rawStep.SingleSoundName);
            xlsxParser.SetCell(stepRowIndex, 11, rawStep.Duration);
        }

        /// <summary>
        ///     Для шага видео квестов и новелл.
        /// </summary>
        internal static RawStep GetVideoRawStep(XlsxParser xlsxParser, int stepRowIndex)
        {
            var number = xlsxParser.GetCell(stepRowIndex, 1);

            if (string.IsNullOrEmpty(number))
                return null;

            return new RawStep
            {
                RowNumber = stepRowIndex,
                Number = number,
                LanguageToDecisionDict = xlsxParser.GetCellDict(stepRowIndex, 2),
                Challenges = xlsxParser.GetCell(stepRowIndex, 3),
                Backwashes = xlsxParser.GetCell(stepRowIndex, 4),
                ImageName = xlsxParser.GetCell(stepRowIndex, 5)
            };
        }

        /// <summary>
        ///     Для шага видео квеста.
        /// </summary>
        public static void SetVideoRawStep(XlsxParser xlsxParser, RawStep rawStep, int stepRowIndex)
        {
            xlsxParser.SetCell(stepRowIndex, 1, rawStep.Number);
            xlsxParser.SetCellDict(stepRowIndex, 2, rawStep.LanguageToDecisionDict);

            xlsxParser.SetCell(stepRowIndex, 3, rawStep.Challenges);
            xlsxParser.SetCell(stepRowIndex, 4, rawStep.Backwashes);
            xlsxParser.SetCell(stepRowIndex, 5, rawStep.ImageName);
        }
    }
}