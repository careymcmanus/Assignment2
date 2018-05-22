using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class DPLL: Engine
    {

        public DPLL()
        {

        }

        public override void Solve()
        {
            LogicalExpression exp = new LogicalExpression("(a&b)<=>c");
            exp = ConvertToCNF(exp);
            exp.printInfo();
        }

        public LogicalExpression ConvertToCNF(LogicalExpression expression)
        {
            if (expression.Symbol != null)
            {
                return expression;
            }
            else if (expression.Connective == "<=>")
            {
                LogicalExpression newExpression = new LogicalExpression();
                newExpression.Connective = "&";
                LogicalExpression child1 = new LogicalExpression(expression.Children[0].OriginalString);
                LogicalExpression child2 = new LogicalExpression(expression.Children[1].OriginalString);

                List<LogicalExpression> children = new List<LogicalExpression>();
                children.Add(child1);
                children.Add(child2);
                newExpression.Children = children;
                Console.WriteLine("Remove Bi-Implication: ");
                newExpression.printInfo();
                return newExpression;
            }
            else if (expression.Connective == "=>")
            {
                LogicalExpression newExpression = new LogicalExpression();
                newExpression.Connective = "\\/";
                LogicalExpression child1 = ConvertToCNF(expression.Children[0]);
                LogicalExpression child2 = ConvertToCNF(expression.Children[1]);
                LogicalExpression negateChild1 = new LogicalExpression();
                negateChild1.Connective = "~";
                negateChild1.Children.Add(child1);
                newExpression.Children.Add(negateChild1);
                newExpression.Children.Add(child2);
                Console.WriteLine("Remove Implication: ");
                newExpression.printInfo();
                return newExpression;
            }
            else if ( expression.Connective == "~")
            {
                if (expression.Children[0].Symbol != null)
                {
                    return expression;
                }
                else
                {
                    if (expression.Children[0].Connective == "\\/")
                    {
                        LogicalExpression newExpression = new LogicalExpression();
                        newExpression.Connective = "&";
                        LogicalExpression child1 = new LogicalExpression();
                        child1.Connective = "~";
                        child1.Children.Add(expression.Children[0].Children[0]);
                        LogicalExpression child2 = new LogicalExpression();
                        child2.Connective = "~";
                        child2.Children.Add(expression.Children[0].Children[1]);
                        newExpression.Children.Add(child1);
                        newExpression.Children.Add(child2);
                        return newExpression;

                    }
                    else if (expression.Children[0].Connective == "&")
                    {
                        LogicalExpression newExpression = new LogicalExpression();
                        newExpression.Connective = "\\/";
                        LogicalExpression child1 = new LogicalExpression();
                        child1.Connective = "~";
                        child1.Children.Add(expression.Children[0].Children[0]);
                        LogicalExpression child2 = new LogicalExpression();
                        child2.Connective = "~";
                        child2.Children.Add(expression.Children[0].Children[1]);
                        newExpression.Children.Add(child1);
                        newExpression.Children.Add(child2);
                        return newExpression;

                    }
                }
            }
            return expression;

            }
        }
    }

