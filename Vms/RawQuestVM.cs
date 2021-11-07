using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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

        public string FilePath;

        public TypeQuest TypeQuest;

        public RawQuestVM()
        {
            TypeQuest = TypeQuest.PartyQuest;
            Steps = new List<RawStepVM>();
            Hierarchy = new ObservableCollection<NodeStepVM>();
        }

        public int ApperDelayInHours { get; set; }

        public string BackgroundSoundName { get; set; }

        public Dictionary<Languages, string> BriefDict { get; set; }

        public string Condition { get; set; }

        public Dictionary<Languages, string> InitialDescriptionDict { get; set; }

        public float? InitialDuration { get; set; }

        public bool IsCyclic { get; set; }

        public string Location { get; set; }

        public string MusicName { get; set; }

        public Dictionary<Languages, string> NameDict { get; set; }

        public string NumberOfParticipants { get; set; }

        public string SingleSoundName { get; set; }

        public string StartImageName { get; set; }

        public List<RawStepVM> Steps { get; }

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


        /// <summary>
        ///     Настроить отображение иерархии и текущего шага.
        /// </summary>
        public void UpdateHierarchy()
        {
            Hierarchy.Clear();

            foreach (var step in Steps)
            {
                var node = new NodeStepVM(this, step);
                HierarchyVmUtils.SetNode(node, Hierarchy);
            }
        }
    }
}