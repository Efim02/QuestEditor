using QuestEditor.Commands.MenuCommands;
using QuestEditor.Xlsx;

namespace QuestEditor.Vms
{
    /// <summary>
    ///     VM редактора квеста.
    /// </summary>
    public class EditorVM : ViewModelBase
    {
        private RawQuestVM _rawQuestVm;

        /// <summary>
        ///     Открыты настройки квеста.
        /// </summary>
        private bool _settingsQuestIsOpen;

        public EditorVM()
        {
            OpenPartyQuestCommand = new OpenFileCommand();
            OpenEventQuestCommand = new OpenFileCommand
            {
                TypeQuest = TypeQuest.EventQuest
            };
            OpenVideoQuestCommand = new OpenFileCommand
            {
                TypeQuest = TypeQuest.VideoQuest
            };
            QuitCommand = new QuitCommand();
            SaveAsFileCommand = new SaveAsFileCommand();
            SaveFileCommand = new SaveFileCommand();
            CreateFileCommand = new CreateFileCommand();
            SettingsQuestCommand = new SettingsQuestCommand();
        }

        /// <summary>
        ///     Текущий квест.
        /// </summary>
        public RawQuestVM RawQuestVm
        {
            get => _rawQuestVm;
            set
            {
                _rawQuestVm = value;
                _rawQuestVm.ParentEditorVm = this;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasOpenQuest));

                _rawQuestVm.UpdateHierarchy();
            }
        }

        /// <inheritdoc cref="_settingsQuestIsOpen" />
        public bool SettingsQuestIsOpen
        {
            get => _settingsQuestIsOpen;
            set
            {
                _settingsQuestIsOpen = value;
                OnPropertyChanged();
            }
        }

        public bool HasOpenQuest => RawQuestVm is null;

        public SettingsQuestCommand SettingsQuestCommand { get; }
        public OpenFileCommand OpenPartyQuestCommand { get; }
        public OpenFileCommand OpenEventQuestCommand { get; }
        public OpenFileCommand OpenVideoQuestCommand { get; }

        public QuitCommand QuitCommand { get; }

        public SaveAsFileCommand SaveAsFileCommand { get; }

        public SaveFileCommand SaveFileCommand { get; }
        public CreateFileCommand CreateFileCommand { get; }
    }
}