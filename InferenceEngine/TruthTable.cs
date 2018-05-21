using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class TruthTable : Engine
    {
        private int _modelCount = 0;
        private KnowledgeBase _knowledgeBase;
        private string _query;

        public TruthTable(KnowledgeBase kb, string query)
        {
            _knowledgeBase = kb;
            _query = query;
        }

        private List<string> createSymbolList(KnowledgeBase kb, string alpha)
        {
            List<string> symbolList = new List<string>();
            symbolList.Add(alpha);
            foreach (Clause c in kb.Clauses)
            {
                if (c.Premise != null)
                {
                    foreach (string s in c.Premise)
                    {
                        if (!symbolList.Contains(s))
                        {
                            symbolList.Add(s);
                        }
                    }
                }
                if (!symbolList.Contains(c.Conclusion))
                {
                    symbolList.Add(c.Conclusion);
                }
            }
            return symbolList;
        }

        private Dictionary<string, bool> extend(string key, bool value, Dictionary<string, bool> model)
        {
            Dictionary<String, bool> newModel = new Dictionary<string, bool>(model);
         
            newModel.Add(key, value);
            return newModel;
        }

        public override void Solve()
        {
            bool isTrue = TT_Entails(_knowledgeBase, _query);
            Console.WriteLine("Is True: " + isTrue + " model count: " + _modelCount);
        }

        public bool TT_Entails(KnowledgeBase kb, string alpha)
        {
            List<string> Symbol = createSymbolList(kb, alpha);
            Dictionary<string, bool> model = new Dictionary<string, bool>();
            return TT_Check_All(kb, alpha, Symbol, model);
        }

        private bool TT_Check_All(KnowledgeBase kb, string alpha, List<string> symbols, Dictionary<string, bool> model)
        {

            
            Console.WriteLine(String.Join(";", symbols.ToArray()));
           
            if (symbols.Count == 0)
            {
                model.ToList().ForEach(x => Console.WriteLine(x.Key + ": " + x.Value));
                if (PL_true(kb, model))
                {                   
                    if (PL_true(alpha, model))
                    {
                        _modelCount++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else { return true; }

            }
            else
            {
                model.ToList().ForEach(x => Console.WriteLine(x.Key + ": " + x.Value));
                string P = symbols[0];
                Console.WriteLine("symbol: " + P);
                symbols.RemoveAt(0);
                return TT_Check_All(kb, alpha, new List<string>(symbols), extend(P, true, model)) && TT_Check_All(kb, alpha, new List<string>(symbols), extend(P, false, model));
            }
        }

        public bool PL_true(string query, Dictionary<string, bool> model)
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

        public bool PL_true(KnowledgeBase kb, Dictionary<string, bool> model)
        {
            bool finalResult = true;
            Console.WriteLine("new model: ----------- ");
            foreach (Clause c in kb.Clauses)
            {
                bool clauseResult = true;
                if (c.Premise == null)
                {
                    clauseResult = PL_true(c.Conclusion, model);
                    //Console.WriteLine(c.Conclusion);
                    //Console.WriteLine("result: " + clauseResult +  ", conclusion: " + PL_true(c.Conclusion, model));
                }
                else
                {
                    bool premiseIsTrue = true;
                    foreach (string symbol in c.Premise)
                    {
                        premiseIsTrue = premiseIsTrue && PL_true(symbol, model);
                        //Console.WriteLine(symbol + ": " + PL_true(symbol, model));
                        
                    }
                    //Console.WriteLine(c.Conclusion + ": " + PL_true(c.Conclusion, model));
                    
                   
                    clauseResult = !(premiseIsTrue && !PL_true(c.Conclusion, model));
                        
                   
                    //Console.WriteLine( String.Join("&", c.Premise.ToArray())+ "=>" + c.Conclusion);
                    //Console.WriteLine("result: " + clauseResult + ", "+ String.Join(" & ", c.Premise.ToArray())+": "+ premiseIsTrue + ", conclusion: " + PL_true(c.Conclusion, model));
                    
                }
                finalResult = finalResult & clauseResult;
            }
            Console.WriteLine("model: "+ finalResult);
            return finalResult;
      
        }
    }
}
