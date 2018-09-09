namespace DecisionTreeLab.Nodes
{
    public interface INode
    {
        string Id { get; set; }

        NodeKind Kind { get; }

        string Name { get; set; }
    }
}