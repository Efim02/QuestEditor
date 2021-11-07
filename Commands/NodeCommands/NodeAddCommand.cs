using QuestEditor.Utils;
using QuestEditor.Vms;

namespace QuestEditor.Commands.NodeCommands
{
    /// <summary>
    ///     Добавить под узел, в текущий узел.
    /// </summary>
    public class NodeAddCommand : TypedBaseCommand<NodeStepVM>
    {
        protected override void Execute(NodeStepVM node)
        {
            var newNode = new NodeStepVM(node, new RawStepVM());
            HierarchyVmUtils.SetNumberForNewNode(node, newNode);
            node.Nodes.Add(newNode);
        }
    }
}