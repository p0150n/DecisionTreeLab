namespace DecisionTreeLab.Nodes
{
    public class ActionNode : NodeWithOneChild
    {
        public override NodeKind Kind => NodeKind.Action;

        public string Expression { get; set; }
    }
}
