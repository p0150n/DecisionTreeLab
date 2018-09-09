using System;

namespace DecisionTreeLab.Nodes
{
    public class FuncNode : ActionNode
    {
        public override NodeKind Kind => NodeKind.Func;

        public string ReturnTypeName { get; set; }

        public Type ReturnType
        {
            get => Type.GetType(this.ReturnTypeName);
            set => this.ReturnTypeName = value.AssemblyQualifiedName;
        }
    }
}
