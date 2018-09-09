using System.Threading.Tasks;
using DecisionTreeLab.Nodes;

namespace DecisionTreeLab
{
    public interface ITreeProcessor
    {
        Task<TreeProcessResult> Process<TContext>(Node node, TContext context);
    }
}