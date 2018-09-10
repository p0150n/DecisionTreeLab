using System;

namespace DecisionTreeLab.Nodes
{
    public interface IFuncNode : IActionNode, IExpressionNode, IOneChildNode, INode
    {
        Type ReturnType { get; set; }

        string ReturnTypeName { get; set; }
    }
}