using System;

namespace DecisionTreeLab.Nodes
{
    public class FuncNode : ActionNode, IFuncNode
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
