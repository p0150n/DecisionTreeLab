namespace DecisionTreeLab.Nodes
{
    public abstract class OneChildNode : Node, IOneChildNode
    {
        public Node Child { get; set; } 
    }
}
