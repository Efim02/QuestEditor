using System.IO;
using QuestEditor.Utils;
using QuestEditor.Vms;

namespace QuestEditor.Commands.MenuCommands
{
    public class SaveFileCommand : TypedBaseCommand<EditorVM>
    {
        protected override void Execute(EditorVM editorVm)
        {
            if (editorVm.RawQuestVm == null)
                return;

            if (File.Exists(editorVm.RawQuestVm.FilePath))
            {
                FileUtils.SaveFile(editorVm.RawQuestVm);
                return;
            }

            FileUtils.SelectPathSaving(editorVm.RawQuestVm);
        }
    }
}