using Newtonsoft.Json;

namespace DecisionTreeLab.Nodes
{
    [JsonConverter(typeof(NullObjectJsonConverter))]
    public class ActionNode : OneChildNode, IActionNode, IExpressionNode, IOneChildNode, INode
    {
        public override NodeKind Kind => NodeKind.Action;

        public string Expression { get; set; }
    }
}
