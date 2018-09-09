namespace DecisionTreeLab.Nodes
{
    public interface IConditionNode : IExpressionNode
    {
        Node False { get; set; }

        Node True { get; set; }
    }
}