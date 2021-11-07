using QuestEditor.Vms;

namespace QuestEditor.Commands.MenuCommands
{
    /// <summary>
    ///     Открыть\закрыть настройки квеста.
    /// </summary>
    public class SettingsQuestCommand : TypedBaseCommand<EditorVM>
    {
        protected override void Execute(EditorVM editorVM)
        {
            editorVM.SettingsQuestIsOpen = !editorVM.SettingsQuestIsOpen;
        }
    }
}