using System.Collections.Generic;

namespace QuestEditor.Xlsx
{
    public class RawQuest
    {
        private RawQuest()
        {
            Steps = new List<RawStep>();
        }

        public int ApperDelayInHours { get; set; }

        public string BackgroundSoundName { get; set; }

        public Dictionary<Languages, string> BriefDict { get; set; }

        public string Condition { get; set; }

        public Dictionary<Languages, string> InitialDescriptionDict { get; set; }

        public float? InitialDuration { get; set; }

        public bool IsCyclic { get; set; }

        public string Location { get; set; }

        public string MusicName { get; set; }

        public Dictionary<Languages, string> NameDict { get; set; }

        public string NumberOfParticipants { get; set; }

        public string SingleSoundName { get; set; }

        public string StartImageName { get; set; }

        public List<RawStep> Steps { get; }

        /// <summary>
        ///     Для событий.
        /// </summary>
        public static RawQuest GetRawEventQuest(XlsxParser xlsxParser)
        {
            var name = xlsxParser.GetCellDict(2, 2);
            var delayInHoursStr = xlsxParser.GetCell(2, 4);

            return new RawQuest
            {
                NameDict = name,
                InitialDescriptionDict = xlsxParser.GetCellDict(2, 3),
                ApperDelayInHours = GetAppearDelayInHours(delayInHoursStr, name[0]),
                IsCyclic = CheckIfCyclic(delayInHoursStr, name[0]),
                Condition = xlsxParser.GetCell(2, 5),
                StartImageName = xlsxParser.GetCell(2, 6),
                MusicName = xlsxParser.GetCell(2, 7),
                BackgroundSoundName = xlsxParser.GetCell(2, 8),
                SingleSoundName = xlsxParser.GetCell(2, 9),
                InitialDuration = xlsxParser.GetCellFloat(2, 10, name[0])
            };
        }

        /// <summary>
        ///     Установка строки данных, для событийных квестов и новелл.
        /// </summary>
        public static void SetRawEventQuest(XlsxParser xlsxParser, RawQuest rawQuest)
        {
            xlsxParser.SetCellDict(2, 2, rawQuest.NameDict);
            xlsxParser.SetCell(2, 4, rawQuest.ApperDelayInHours);

            xlsxParser.SetCellDict(2, 3, rawQuest.InitialDescriptionDict);

            xlsxParser.SetCell(2, 5, rawQuest.Condition);
            xlsxParser.SetCell(2, 6, rawQuest.StartImageName);
            xlsxParser.SetCell(2, 7, rawQuest.MusicName);
            xlsxParser.SetCell(2, 8, rawQuest.BackgroundSoundName);
            xlsxParser.SetCell(2, 9, rawQuest.SingleSoundName);
            xlsxParser.SetCell(2, 10, rawQuest.InitialDuration);
        }

        /// <summary>
        ///     Для партийных квестов и новелл.
        /// </summary>
        public static RawQuest GetRawPartyQuest(XlsxParser xlsxParser)
        {
            var name = xlsxParser.GetCellDict(2, 2);
            var delayInHoursStr = xlsxParser.GetCell(2, 6);

            return new RawQuest
            {
                NameDict = name,
                NumberOfParticipants = xlsxParser.GetCell(2, 1),
                BriefDict = xlsxParser.GetCellDict(2, 3),
                Location = xlsxParser.GetCell(2, 4),
                InitialDescriptionDict = xlsxParser.GetCellDict(2, 5),
                ApperDelayInHours = GetAppearDelayInHours(delayInHoursStr, name[0]),
                IsCyclic = CheckIfCyclic(delayInHoursStr, name[0]),
                Condition = xlsxParser.GetCell(2, 7),
                StartImageName = xlsxParser.GetCell(2, 8),
                MusicName = xlsxParser.GetCell(2, 9),
                BackgroundSoundName = xlsxParser.GetCell(2, 10),
                SingleSoundName = xlsxParser.GetCell(2, 11)
            };
        }

        /// <summary>
        ///     Установка строки данных, для партийных квестов и новелл.
        /// </summary>
        public static void SetRawPartyQuest(XlsxParser xlsxParser, RawQuest rawQuest)
        {
            xlsxParser.SetCellDict(2, 2, rawQuest.NameDict);
            xlsxParser.SetCell(2, 6, rawQuest.ApperDelayInHours);

            xlsxParser.SetCell(2, 1, rawQuest.NumberOfParticipants);
            xlsxParser.SetCellDict(2, 3, rawQuest.BriefDict);
            xlsxParser.SetCell(2, 4, rawQuest.Location);
            xlsxParser.SetCellDict(2, 5, rawQuest.InitialDescriptionDict);
            xlsxParser.SetCell(2, 7, rawQuest.Condition);
            xlsxParser.SetCell(2, 8, rawQuest.StartImageName);
            xlsxParser.SetCell(2, 9, rawQuest.MusicName);
            xlsxParser.SetCell(2, 10, rawQuest.BackgroundSoundName);
            xlsxParser.SetCell(2, 11, rawQuest.SingleSoundName);
        }

        public static RawQuest GetRawVideoQuest(XlsxParser xlsxParser)
        {
            var name = xlsxParser.GetCellDict(2, 2);
            var delayInHoursStr = xlsxParser.GetCell(2, 3);

            return new RawQuest
            {
                NameDict = name,
                ApperDelayInHours = GetAppearDelayInHours(delayInHoursStr, name[0]),
                Condition = xlsxParser.GetCell(2, 4)
            };
        }

        /// <summary>
        ///     Установка строки данных, для видео квестов и новелл.
        /// </summary>
        public static void SetRawVideoQuest(XlsxParser xlsxParser, RawQuest rawQuest)
        {
            xlsxParser.SetCellDict(2, 2, rawQuest.NameDict);
            xlsxParser.SetCell(2, 3, rawQuest.ApperDelayInHours);

            xlsxParser.SetCell(2, 4, rawQuest.Condition);
        }

        private static bool CheckIfCyclic(string apperDelayInHoursStr, string rawQuestName)
        {
            if (string.IsNullOrWhiteSpace(apperDelayInHoursStr))
                return false;

            var splitted = apperDelayInHoursStr.Split(' ');

            if (splitted.Length == 1)
                return false;

            // отделяем число
            var cyclicWork = splitted[1];

            // "цикл" с игнорированием первой буквы :-)
            if (cyclicWork.Contains("икл"))
                return true;

            throw new StepParseException($"Не удалось распарсить задержку появления: {apperDelayInHoursStr}", 2,
                rawQuestName);
        }

        private static int GetAppearDelayInHours(string apperDelayInHoursStr, string rawQuestName)
        {
            if (string.IsNullOrWhiteSpace(apperDelayInHoursStr))
                return 0;

            // отделяем число
            var delayStr = apperDelayInHoursStr.Split(' ')[0];

            if (int.TryParse(delayStr, out var delay))
                return delay;

            throw new StepParseException($"Не удалось распарсить задержку появления: {apperDelayInHoursStr}", 2,
                rawQuestName);
        }
    }
}