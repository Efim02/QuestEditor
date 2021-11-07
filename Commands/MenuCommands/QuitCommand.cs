using System;
using QuestEditor.Vms;
using Application = System.Windows.Application;

namespace QuestEditor.Commands.MenuCommands
{
    public class QuitCommand : TypedBaseCommand<EditorVM>
    {
        protected override void Execute(EditorVM editorVM)
        {
           Application.Current.MainWindow.Close();
        }
    }
}