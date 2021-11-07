using QuestEditor.Vms;

namespace QuestEditor.Commands.NodeCommands
{
    /// <summary>
    ///     Добавить под узел, в текущий родительский узел.
    /// </summary>
    public class NodeParentAddCommand : TypedBaseCommand<NodeStepVM>
    {
        protected override void Execute(NodeStepVM parent)
        {
            var newNode = new NodeStepVM(parent, new RawStepVM());
            newNode.RawStepVM.Number = $"{parent.Nodes.Count + 1}";
            parent.Nodes.Add(newNode);
        }
    }
}