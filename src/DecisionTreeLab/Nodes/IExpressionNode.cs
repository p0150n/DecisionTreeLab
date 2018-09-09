namespace DecisionTreeLab.Nodes
{
    public interface IExpressionNode : INode
    {
        string Expression { get; set; }
    }
}