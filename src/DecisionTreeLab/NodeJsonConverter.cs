using DecisionTreeLab.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DecisionTreeLab
{
    public class NodeJsonConverter : JsonConverter
    {
        private static readonly Type NODE_TYPE = typeof(Node);

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Not supported
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            JObject nodeJObject = JObject.Load(reader);
            NodeKind kind = nodeJObject.Property(nameof(Node.Kind)).ToObject<NodeKind>();

            switch (kind)
            {
                case NodeKind.Action:
                    return nodeJObject.ToObject<ActionNode>();
                case NodeKind.Func:
                    return nodeJObject.ToObject<FuncNode>();
                case NodeKind.Condition:
                    return nodeJObject.ToObject<ConditionNode>();
                default:
                    throw new NotSupportedException($"Node of kind {kind} is not supported for deserialization.");
            }
        }
        public override bool CanConvert(Type objectType)
        {
            return NODE_TYPE == objectType;
        }
    }
}
