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
                OnPropertyChanged();

                _rawQuestVm.UpdateHierarchy();
            }
        }

        public OpenFileCommand OpenPartyQuestCommand { get; }
        public OpenFileCommand OpenEventQuestCommand { get; }
        public OpenFileCommand OpenVideoQuestCommand { get; }

        public QuitCommand QuitCommand { get; }

        public SaveAsFileCommand SaveAsFileCommand { get; }

        public SaveFileCommand SaveFileCommand { get; }
        public CreateFileCommand CreateFileCommand { get; }
    }
}