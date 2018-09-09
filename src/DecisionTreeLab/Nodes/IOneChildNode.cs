namespace DecisionTreeLab.Nodes
{
    public interface IOneChildNode : INode
    {
        Node Child { get; set; }
    }
}