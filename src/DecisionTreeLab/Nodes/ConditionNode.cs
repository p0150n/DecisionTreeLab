namespace DecisionTreeLab.Nodes
{
    public class ConditionNode : Node
    {
        public override NodeKind Kind => NodeKind.Condition;

        public string Expression { get; set; }

        public Node True { get; set; }

        public Node False { get; set; }
    }
}
