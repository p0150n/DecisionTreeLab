using DecisionTreeLab.Nodes;
using DecisionTreeLab.Tests.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DecisionTreeLab.Tests
{
    [TestFixture]
    public class LabTests
    {
        [Test]
        public async Task TestExpressionExecutor()
        {
            ExpressionExecutor executor = this.CreateSampleExpressionExecutor();
            Context context = this.GetSampleContext();

            var result = await executor.Run<Context, string>("Context.Order.DisplayName = Variables[0]", context, "New display name");
            await executor.Run<Context>("Context.Order.Void()", context);
            var result3 = await executor.Run<object, object>("Context.ToString()", new object());
        }

        [Test]
        public async Task TestTreeProcessor()
        {
            var tree = new ConditionNode()
            {
                Id = "Is order display name null",
                Name = "Is order display name null",
                Expression = "Context.Order.DisplayName == null"
            };

            ActionNode executeOrderVoidMethod = new ActionNode()
            {
                Id = nameof(executeOrderVoidMethod),
                Name = nameof(executeOrderVoidMethod),
                Expression = "Context.Order.Void()",
            };
            tree.True = executeOrderVoidMethod;

            FuncNode setOrderDisplayName = new FuncNode()
            {
                Id = nameof(setOrderDisplayName),
                Name = nameof(setOrderDisplayName),
                Expression = "Context.Order.DisplayName = \"Another order\"",
                ReturnType = typeof(string)
            };
            tree.False = setOrderDisplayName;
            ConditionNode isOrderDisplayNameEqualsToResult = new ConditionNode()
            {
                Id = nameof(isOrderDisplayNameEqualsToResult),
                Name = nameof(isOrderDisplayNameEqualsToResult),
                Expression = "Context.Order.DisplayName == Variables[0]"
            };
            setOrderDisplayName.Child = isOrderDisplayNameEqualsToResult;

            ActionNode setOrderDisplayNameToNull = new ActionNode()
            {
                Id = nameof(executeOrderVoidMethod),
                Name = nameof(executeOrderVoidMethod),
                Expression = "Context.Order.DisplayName = null",
            };
            isOrderDisplayNameEqualsToResult.True = setOrderDisplayNameToNull;

            ExpressionExecutor executor = this.CreateSampleExpressionExecutor();
            var treeProcessor = new TreeProcessor(executor);

            Context context = this.GetSampleContext();
            var sw = Stopwatch.StartNew();
            TreeProcessResult result = await treeProcessor.Process(tree, context);
            sw.Stop();
            var firstRunTimeSeconds = sw.Elapsed.TotalSeconds;
            sw.Restart();
            for (int i = 0; i < 100_000; i++)
            {
                sw.Stop();
                Context ctx = this.GetSampleContext();
                sw.Start();
                TreeProcessResult result2 = await treeProcessor.Process(tree, ctx);
            }
            var multipleRunsSecondes = sw.Elapsed.TotalSeconds;
            sw.Stop();
        }

        private ExpressionExecutor CreateSampleExpressionExecutor()
        {
            Type contextType = typeof(Context);
            var assemblies = new[] { contextType.Assembly };
            var usings = new[] { contextType.Namespace };
            var executor = new ExpressionExecutor(assemblies, usings);
            return executor;
        }

        private Context GetSampleContext()
        {
            return new Context()
            {
                Order = new Order()
                {
                    DisplayName = "Some order",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { ProductName = "Bike" }
                    }
                }
            };
        }
    }
}
