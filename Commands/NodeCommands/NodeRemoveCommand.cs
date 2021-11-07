using QuestEditor.Utils;
using QuestEditor.Vms;

namespace QuestEditor.Commands.NodeCommands
{
    /// <summary>
    ///     Удалить текущий узел.
    /// </summary>
    public class NodeRemoveCommand : TypedBaseCommand<NodeStepVM>
    {
        protected override void Execute(NodeStepVM node)
        {
            var parent = node.Parent;
            parent.Nodes.Remove(node);
            HierarchyVmUtils.UpdateNumbersInNode(parent);
        }
    }
}