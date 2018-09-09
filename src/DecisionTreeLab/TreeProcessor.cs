using DecisionTreeLab.Nodes;
using System;
using System.Threading.Tasks;

namespace DecisionTreeLab
{
    public class TreeProcessor
    {
        private readonly ExpressionExecutor expressionExecutor;

        public TreeProcessor(ExpressionExecutor expressionExecutor)
        {
            this.expressionExecutor = expressionExecutor;
        }

        public Task<TreeProcessResult> Process<TContext>(Node node, TContext context)
            => this.Process(node, context, new TreeProcessResult());

        private Task<TreeProcessResult> Process<TContext>(
            Node node,
            TContext context,
            TreeProcessResult resultStore,
            params object[] arguments)
        {
            if (node == null)
            {
                return Task.FromResult(resultStore);
            }

            resultStore.ExecutionPathNodeIds.Add(node.Id);

            switch (node.Kind)
            {
                case NodeKind.Action:
                    return this.ProcessAction((ActionNode)node, context, resultStore, arguments);
                case NodeKind.Func:
                    return this.ProcessFunc((FuncNode)node, context, resultStore, arguments);
                case NodeKind.Condition:
                    return this.ProcessCondition((ConditionNode)node, context, resultStore, arguments);
                default:
                    throw new NotSupportedException($"Node of kind {node.Kind} is not supported.");
            }
        }

        private async Task<TreeProcessResult> ProcessAction<TContext>(
            ActionNode node,
            TContext context,
            TreeProcessResult resultStore,
            object[] arguments)
        {
            await this.expressionExecutor.Run(node.Expression, context, arguments)
                .ConfigureAwait(false);
            return await this.Process(node.Child, context, resultStore)
                .ConfigureAwait(false);
        }

        private async Task<TreeProcessResult> ProcessFunc<TContext>(
            FuncNode node,
            TContext context,
            TreeProcessResult resultStore,
            object[] arguments)
        {
            object result =
                await this.expressionExecutor.Run<TContext, object>(node.Expression, context, arguments)
                .ConfigureAwait(false);
            return await this.Process(node.Child, context, resultStore, result)
                .ConfigureAwait(false);
        }

        private async Task<TreeProcessResult> ProcessCondition<TContext>(
            ConditionNode node,
            TContext context,
            TreeProcessResult resultStore,
            object[] arguments)
        {
            bool result =
                await this.expressionExecutor.Run<TContext, bool>(node.Expression, context, arguments)
                .ConfigureAwait(false);

            if (result)
            {
                return await this.Process(node.True, context, resultStore)
                    .ConfigureAwait(false);
            }
            else
            {
                return await this.Process(node.False, context, resultStore)
                    .ConfigureAwait(false);
            }
        }
    }
}
