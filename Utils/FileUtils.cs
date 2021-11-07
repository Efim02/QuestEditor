using System.IO;
using AutoMapper;
using Microsoft.Win32;
using QuestEditor.Vms;
using QuestEditor.Xlsx;

namespace QuestEditor.Utils
{
    public static class FileUtils
    {
        public const string FILTER_EXTENSION_FILES = "Xlsx файл (*.xlsx)|*.xlsx|Все файлы (*.*)|*.*";

        /// <summary>
        ///     Выбрать путь по которому будет сохранен файл.
        /// </summary>
        /// <param name="rawQuestVm">RawQuestVm куда запишется путь.</param>
        public static void SelectPathSaving(RawQuestVM rawQuestVm)
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = "*",
                Filter = FILTER_EXTENSION_FILES,
                FileName = rawQuestVm.FilePath
            };
            dialog.ShowDialog();

            if (!Path.IsPathRooted(dialog.FileName))
                return;

            rawQuestVm.FilePath = dialog.FileName;

            SaveFile(rawQuestVm);
        }

        /// <summary>
        ///     Сохранится файл квеста.
        /// </summary>
        /// <param name="rawQuestVm"></param>
        public static void SaveFile(RawQuestVM rawQuestVm)
        {
            var rawQuest = Injector.Get<IMapper>().Map<RawQuest>(rawQuestVm);

            using (var stream = File.Create(rawQuestVm.FilePath))
            using (var parser = new XlsxParser())
            {
                switch (rawQuestVm.TypeQuest)
                {
                    case TypeQuest.PartyQuest:
                        parser.WritePartyQuest(rawQuest);
                        break;
                    case TypeQuest.EventQuest:
                        parser.WriteEventQuest(rawQuest);
                        break;
                    case TypeQuest.VideoQuest:
                        parser.WriteVideoQuest(rawQuest);
                        break;
                }

                parser.Save(stream);
            }
        }
    }
}