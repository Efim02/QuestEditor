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

        /// <summary>
        ///     Создать узел, указав родителя.
        /// </summary>
        /// <param name="parent">Родительский узел.</param>
        /// <param name="rawStepVm">Данные.</param>
        public NodeStepVM(NodeStepVM parent, RawStepVM rawStepVm)
        {
            Nodes = new ObservableCollection<NodeStepVM>();
            Parent = parent;
            RawStepVM = rawStepVm;
        }

        public NodeStepVM Parent { get; set; }

        public RawStepVM RawStepVM { get; set; }

        public ObservableCollection<NodeStepVM> Nodes { get; set; }
    }
}