namespace DecisionTreeLab.Nodes
{
    public abstract class OneChildNode : Node, IOneChildNode, INode
    {
        public Node Child { get; set; } 
    }
}
