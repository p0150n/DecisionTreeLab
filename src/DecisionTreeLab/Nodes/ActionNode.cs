namespace DecisionTreeLab.Nodes
{
    public class ActionNode : OneChildNode, IActionNode
    {
        public override NodeKind Kind => NodeKind.Action;

        public string Expression { get; set; }
    }
}
