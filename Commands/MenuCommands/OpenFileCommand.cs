using System;
using System.IO;
using AutoMapper;
using Microsoft.Win32;
using QuestEditor.Utils;
using QuestEditor.Vms;
using QuestEditor.Xlsx;

namespace QuestEditor.Commands.MenuCommands
{
    /// <summary>
    ///     Команда для открытия файла.
    /// </summary>
    public class OpenFileCommand : TypedBaseCommand<EditorVM>
    {
        public TypeQuest TypeQuest;

        public OpenFileCommand()
        {
            TypeQuest = TypeQuest.PartyQuest;
        }

        protected override void Execute(EditorVM editorVm)
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = "*",
                Filter = FileUtils.FILTER_EXTENSION_FILES
            };

            dialog.ShowDialog();
            var path = dialog.FileName;

            if (!File.Exists(path))
                return;

            var stream = File.Open(path, FileMode.Open);
            using (var parser = new XlsxParser(stream))
            {
                RawQuest rawQuest;
                switch (TypeQuest)
                {
                    case TypeQuest.PartyQuest:
                        rawQuest = parser.ParseParty();
                        break;
                    case TypeQuest.EventQuest:
                        rawQuest = parser.ParseEvent();
                        break;
                    case TypeQuest.VideoQuest:
                        rawQuest = parser.ParseVideo();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var questVm = Injector.Get<IMapper>().Map<RawQuestVM>(rawQuest);
                questVm.TypeQuest = TypeQuest;
                questVm.FilePath = path;
                editorVm.RawQuestVm = questVm;
            }
        }
    }
}