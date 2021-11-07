using QuestEditor.Vms;

namespace QuestEditor.Commands.MenuCommands
{
    public class CreateFileCommand : TypedBaseCommand<EditorVM>
    {
        protected override void Execute(EditorVM editorVM)
        {
            editorVM.RawQuestVm = new RawQuestVM();
        }
    }
}