using System.Threading.Tasks;

namespace DecisionTreeLab
{
    public interface IExpressionExecutor
    {
        Task<TReturnType> Run<TContextType, TReturnType>(string scriptText, TContextType context, params object[] variables);

        Task Run<TContextType>(string scriptText, TContextType context, params object[] variables);
    }
}