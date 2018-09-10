using Newtonsoft.Json;

namespace DecisionTreeLab.Nodes
{
    [JsonConverter(typeof(NullObjectJsonConverter))]
    public class ConditionNode : Node, IConditionNode, IExpressionNode, INode
    {
        public override NodeKind Kind => NodeKind.Condition;

        public string Expression { get; set; }

        public Node True { get; set; }

        public Node False { get; set; }
    }
}
