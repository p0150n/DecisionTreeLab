using Newtonsoft.Json;

namespace DecisionTreeLab.Nodes
{
    [JsonConverter(typeof(NodeJsonConverter))]
    public abstract class Node : INode
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public abstract NodeKind Kind { get; }
    }
}
