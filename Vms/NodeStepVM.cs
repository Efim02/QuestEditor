using System.Collections.ObjectModel;

namespace QuestEditor.Vms
{
    /// <summary>
    ///     Узел для иерархии.
    /// </summary>
    public class NodeStepVM : ViewModelBase
    {
        public NodeStepVM()
        {
            Nodes = new ObservableCollection<NodeStepVM>();
        }

        public NodeStepVM(RawQuestVM rawQuestVm, RawStepVM rawStepVm)
        {
            Nodes = new ObservableCollection<NodeStepVM>();
            Parent = rawQuestVm;
            RawStepVM = rawStepVm;
        }

        public RawQuestVM Parent { get; set; }

        public RawStepVM RawStepVM { get; set; }

        public ObservableCollection<NodeStepVM> Nodes { get; set; }
    }
}