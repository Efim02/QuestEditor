using System.Collections.Generic;
using System.Collections.ObjectModel;
using QuestEditor.Commands.MenuCommands;
using QuestEditor.Commands.NodeCommands;
using QuestEditor.Constants;
using QuestEditor.Utils;
using QuestEditor.Xlsx;

namespace QuestEditor.Vms
{
    /// <summary>
    ///     VM квеста.
    /// </summary>
    public class RawQuestVM : ViewModelBase
    {
        /// <summary>
        ///     Выбранный шаг.
        /// </summary>
        private RawStepVM _selectedRawStepVm;

        public RawQuestVM()
        {
            TypeQuest = TypeQuest.PartyQuest;
            Steps = new List<RawStepVM>();
            Hierarchy = new ObservableCollection<NodeStepVM>();
            MainNode = new NodeStepVM
            {
                Nodes = Hierarchy,
                RawStepVM = new RawStepVM {Number = Names.MainNode}
            };
            NodeAddCommand = new NodeAddCommand();
            NodeRemoveCommand = new NodeRemoveCommand();
            NodeParentAddCommand = new NodeParentAddCommand();

            BriefDict = new Dictionary<Languages, string>
            {
                {Languages.English, ""}, {Languages.Russian, ""}
            };
            InitialDescriptionDict = new Dictionary<Languages, string>
            {
                {Languages.English, ""}, {Languages.Russian, ""}
            };
            NameDict = new Dictionary<Languages, string>
            {
                {Languages.English, ""}, {Languages.Russian, ""}
            };
            SettingsQuestCommand = new SettingsQuestCommand();
        }

        public EditorVM ParentEditorVm { get; set; }

        public SettingsQuestCommand SettingsQuestCommand { get; }

        public string FilePath { get; set; }

        public TypeQuest TypeQuest { get; set; }

        public int ApperDelayInHours { get; set; }

        public string BackgroundSoundName { get; set; }

        public string EnglishBrief
        {
            get => BriefDict[Languages.English];
            set => BriefDict[Languages.English] = value;
        }

        public string RussianBrief
        {
            get => BriefDict[Languages.Russian];
            set => BriefDict[Languages.Russian] = value;
        }

        public Dictionary<Languages, string> BriefDict { get; set; }

        public string Condition { get; set; }

        public string EnglishInitialDescription
        {
            get => InitialDescriptionDict[Languages.English];
            set => InitialDescriptionDict[Languages.English] = value;
        }

        public string RussianInitialDescription
        {
            get => InitialDescriptionDict[Languages.Russian];
            set => InitialDescriptionDict[Languages.Russian] = value;
        }

        public Dictionary<Languages, string> InitialDescriptionDict { get; set; }

        public float? InitialDuration { get; set; }

        public bool IsCyclic { get; set; }

        public string Location { get; set; }

        public string MusicName { get; set; }

        public string EnglishName
        {
            get => NameDict[Languages.English];
            set => NameDict[Languages.English] = value;
        }

        public string RussianName
        {
            get => NameDict[Languages.Russian];
            set => NameDict[Languages.Russian] = value;
        }

        public Dictionary<Languages, string> NameDict { get; set; }

        public string NumberOfParticipants { get; set; }

        public string SingleSoundName { get; set; }

        public string StartImageName { get; set; }

        public List<RawStepVM> Steps { get; }

        public NodeStepVM MainNode { get; }

        public ObservableCollection<NodeStepVM> Hierarchy { get; }

        /// <inheritdoc cref="_selectedRawStepVm" />
        public RawStepVM SelectedRawStepVm
        {
            get => _selectedRawStepVm;
            set
            {
                _selectedRawStepVm = value;
                OnPropertyChanged();
            }
        }

        public NodeAddCommand NodeAddCommand { get; }

        public NodeRemoveCommand NodeRemoveCommand { get; }

        public NodeParentAddCommand NodeParentAddCommand { get; }


        /// <summary>
        ///     Настроить отображение иерархии и текущего шага.
        /// </summary>
        public void UpdateHierarchy()
        {
            Hierarchy.Clear();

            foreach (var step in Steps)
            {
                var node = new NodeStepVM(null, step);
                HierarchyVmUtils.SetNode(node, Hierarchy);
            }
        }
    }
}