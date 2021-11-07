using System.Windows;
using QuestEditor.Vms;

namespace QuestEditor.Commands.MenuCommands
{
    public class QuitCommand : TypedBaseCommand<EditorVM>
    {
        protected override void Execute(EditorVM editorVm)
        {
            Application.Current.MainWindow?.Close();
        }
    }
}