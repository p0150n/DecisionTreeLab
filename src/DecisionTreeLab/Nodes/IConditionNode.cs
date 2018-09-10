namespace DecisionTreeLab.Nodes
{
    public interface IConditionNode : IExpressionNode, INode
    {
        Node False { get; set; }

        Node True { get; set; }
    }
}