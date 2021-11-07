using QuestEditor.Vms;

namespace QuestEditor.Commands.MenuCommands
{
    public class CreateFileCommand : TypedBaseCommand<EditorVM>
    {
        protected override void Execute(EditorVM editorVm)
        {
            editorVm.RawQuestVm = new RawQuestVM();
        }
    }
}