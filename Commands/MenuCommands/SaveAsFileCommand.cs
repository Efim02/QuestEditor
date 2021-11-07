using QuestEditor.Utils;
using QuestEditor.Vms;

namespace QuestEditor.Commands.MenuCommands
{
    public class SaveAsFileCommand : TypedBaseCommand<EditorVM>
    {
        protected override void Execute(EditorVM editorVm)
        {
            if (editorVm.RawQuestVm == null)
                return;

            FileUtils.SelectPathSaving(editorVm.RawQuestVm);
        }
    }
}