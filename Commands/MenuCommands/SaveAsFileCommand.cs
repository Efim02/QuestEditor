using QuestEditor.Utils;
using QuestEditor.Vms;

namespace QuestEditor.Commands.MenuCommands
{
    public class SaveAsFileCommand : TypedBaseCommand<EditorVM>
    {
        protected override void Execute(EditorVM editorVM)
        {
            if (editorVM.RawQuestVm == null)
                return;

            FileUtils.SelectPathSaving(editorVM.RawQuestVm);
        }
    }
}