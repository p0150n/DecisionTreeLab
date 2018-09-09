using System;

namespace DecisionTreeLab.Nodes
{
    public interface IFuncNode : IActionNode
    {
        Type ReturnType { get; set; }

        string ReturnTypeName { get; set; }
    }
}