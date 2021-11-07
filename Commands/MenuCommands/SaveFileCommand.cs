using System.IO;
using QuestEditor.Utils;
using QuestEditor.Vms;

namespace QuestEditor.Commands.MenuCommands
{
    public class SaveFileCommand : TypedBaseCommand<EditorVM>
    {
        protected override void Execute(EditorVM editorVM)
        {
            if (editorVM.RawQuestVm == null)
                return;

            if (File.Exists(editorVM.RawQuestVm.FilePath))
            {
                FileUtils.SaveFile(editorVM.RawQuestVm);
                return;
            }

            FileUtils.SelectPathSaving(editorVM.RawQuestVm);
        }
    }
}