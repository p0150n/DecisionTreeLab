using System.Collections.Generic;

namespace DecisionTreeLab
{
    public class TreeProcessResult
    {
        public TreeProcessResult()
        {
            this.ExecutionPathNodeIds = new List<string>();
        }

        public List<string> ExecutionPathNodeIds { get; }
    }
}
