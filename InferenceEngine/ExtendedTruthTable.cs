using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class ExtendedTruthTable: Engine
    {
        private AdvancedKnowledgeBase _kb;
        private string _query;
        private int _modelCount;

        public ExtendedTruthTable(AdvancedKnowledgeBase kb, string query)
        {
            _kb = kb;
            _query = query;
        }

        public override void Solve()
        {
            createSymbolList(_kb.Expressions, _query);
            bool result = TT_ENTAILS(_kb, _query);
            Console.WriteLine("result: " + result + " model count: " + _modelCount);
        }

        private List<string> createSymbolList(List<LogicalExpression> kb, string query)
        {
            List<string> symbolList = new List<string>();
            List<string> runningList = new List<string>();
            symbolList.Add(query);
            foreach (LogicalExpression exp in kb)
            {
                if (exp.Symbol != null)
                {
                    if (!symbolList.Contains(exp.Symbol))
                    {
                        symbolList.Add(exp.Symbol);
                    }
                   
                }
                else
                {
                    runningList = createSymbolList(exp.Children, null);
                    foreach (string s in runningList)
                    {
                        if (!symbolList.Contains(s))
                        {
                            symbolList.Add(s);
                        }
                    }
                }
            }
            Console.WriteLine("symbol list: "+String.Join(" ",symbolList));

            return symbolList;
        }

        private Dictionary<string, bool> extend(string key, bool val, Dictionary<string, bool> model)
        {
            Dictionary<string, bool> newModel = new Dictionary<string, bool>(model);
            if (key != null)
            {
                newModel.Add(key, val);
            }
            
            return newModel;
        }

        private bool TT_ENTAILS(AdvancedKnowledgeBase kb, string query)
        {
            List<string> Symbol = createSymbolList(kb.Expressions, query);
            Dictionary<string, bool> model = new Dictionary<string, bool>();
            return TT_CHECK_ALL(kb, query, Symbol, model);
        }

        private bool TT_CHECK_ALL(AdvancedKnowledgeBase kb, string query, List<string> symbols, Dictionary<string, bool> model)
        {
            if (symbols.Count == 0)
            {
                if (IS_TRUE(kb.Expressions, model))
                {
                    if (IS_TRUE(query, model))
                    {
                        _modelCount++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                string P = symbols[0];
                symbols.RemoveAt(0);
                return TT_CHECK_ALL(kb, query, new List<string>(symbols), extend(P, true, model)) && TT_CHECK_ALL(kb, query, new List<string>(symbols), extend(P, false, model)); 
            }
        }
        private bool IS_TRUE(string query, Dictionary<string, bool> model)
        {
            if (model.ContainsKey(query))
            {
                return model[query];
            }
            else
            {
                return false;
            }
        }
        private bool IS_TRUE(LogicalExpression expression, Dictionary<string, bool> model)
        {
            bool result = true;
            if (expression.Symbol != null)
            {
                result = IS_TRUE(expression.Symbol, model);
            }
            else
            {
                if (expression.Connective == "&")
                {
                    foreach (LogicalExpression child in expression.Children)
                    {
                        result = result && IS_TRUE(child, model);
                    }
                }
                if (expression.Connective == "\\/")
                {
                    result = false;
                    foreach (LogicalExpression child in expression.Children)
                    {
                        result = result || IS_TRUE(child, model);
                    }
                }
                if (expression.Connective == "=>")
                {
                    if (IS_TRUE(expression.Children[0], model) && !IS_TRUE(expression.Children[1], model))
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
                if (expression.Connective == "<=>")
                {
                    if (IS_TRUE(expression.Children[0],model) == IS_TRUE(expression.Children[1], model))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        private bool IS_TRUE(List<LogicalExpression> kb, Dictionary<string, bool> model)
        {
            bool finalResult = true;
            foreach (LogicalExpression exp in kb)
            {
                finalResult = finalResult && IS_TRUE(exp, model);
            }
            return finalResult;
        }
    }
}
