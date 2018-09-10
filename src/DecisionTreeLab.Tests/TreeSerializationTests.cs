using DecisionTreeLab.Nodes;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DecisionTreeLab.Tests
{
    [TestFixture]
    public class TreeSerializationTests
    {
        [Test]
        public void ActionNode_ShouldBe_SerializedAndDeserialized_Correctly()
        {
            var actionNode = new ActionNode()
            {
                Id = "ID",
                Name = "Action node",
                Expression = "SomeExpression"
            };

            string serializedActionNode = JsonConvert.SerializeObject(actionNode, Formatting.Indented);
            Node deserializedActionNode = JsonConvert.DeserializeObject<Node>(serializedActionNode);
            deserializedActionNode.Should().BeEquivalentTo(actionNode);
        }

        [Test]
        public void FuncNode_ShouldBe_SerializedAndDeserialized_Correctly()
        {
            var funcNode = new FuncNode()
            {
                Id = "ID",
                Name = "Function node",
                Expression = "SomeExpression",
                ReturnTypeName = typeof(string).AssemblyQualifiedName
            };

            string serializedFuncNode = JsonConvert.SerializeObject(funcNode, Formatting.Indented);
            Node deserializedFuncNode = JsonConvert.DeserializeObject<Node>(serializedFuncNode);

            deserializedFuncNode.Should().BeEquivalentTo(funcNode);
        }

        [Test]
        public void ConditionNode_ShouldBe_SerializedAndDeserialized_Correctly()
        {
            var conditionNode = new ConditionNode()
            {
                Id = "ID",
                Name = "Condition node",
                Expression = "SomeExpression",
            };

            string serializedConditionNode = JsonConvert.SerializeObject(conditionNode, Formatting.Indented);
            Node deserializedConditionNode = JsonConvert.DeserializeObject<Node>(serializedConditionNode);

            deserializedConditionNode.Should().BeEquivalentTo(conditionNode);
        }

        [Test]
        public void Tree_ShouldBe_SerializedAndDeserialized_Correctly()
        {
            var tree = new ConditionNode()
            {
                Id = "ID",
                Name = "Condition node",
                Expression = "SomeExpression",
                True = new FuncNode()
                {
                    Id = "ID1",
                    Name = "Function node",
                    Expression = "SomeExpression1",
                    ReturnTypeName = typeof(string).AssemblyQualifiedName,
                    Child = new ActionNode()
                    {
                        Id = "ID3",
                        Name = "Action node1",
                        Expression = "SomeExpression3"
                    }
                },
                False = new ActionNode()
                {
                    Id = "ID2",
                    Name = "Action node",
                    Expression = "SomeExpression2",
                    Child = new ActionNode()
                    {
                        Id = "ID4",
                        Name = "Action node2",
                        Expression = "SomeExpression4"
                    }
                }
            };

            string serializedTree = JsonConvert.SerializeObject(tree, Formatting.Indented);
            Node deserializedTree = JsonConvert.DeserializeObject<Node>(serializedTree);

            deserializedTree.Should().BeEquivalentTo(tree);
        }
    }
}
