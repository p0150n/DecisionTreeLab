using System;
using Newtonsoft.Json;

namespace DecisionTreeLab.Nodes
{
    [JsonConverter(typeof(NullObjectJsonConverter))]
    public class FuncNode : ActionNode, IFuncNode, IActionNode, IExpressionNode, IOneChildNode, INode
    {
        public override NodeKind Kind => NodeKind.Func;

        public string ReturnTypeName { get; set; }

        [JsonIgnore]
        public Type ReturnType
        {
            get => Type.GetType(this.ReturnTypeName);
            set => this.ReturnTypeName = value.AssemblyQualifiedName;
        }
    }
}
