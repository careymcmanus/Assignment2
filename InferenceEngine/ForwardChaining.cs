using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class ForwardChaining : Engine
    {
        private KnowledgeBase _knowledgeBase;
        private string _query;
        private Dictionary<string, bool> _inferred;

        public ForwardChaining(KnowledgeBase kb, string Query)
        {
            _knowledgeBase = kb;
            _query = Query;
        }

        public override void Solve()
        {
            
            bool isTrue = PL_FC_Entails();
            string path = "";
            string yesOrNo;
            if (isTrue)
            {
                foreach (KeyValuePair<string, bool> entry in _inferred)
                {
                    if (entry.Value)
                    {
                        path = path + entry.Key + ",";
                    }
                }
            }
            if (isTrue)
            {
                yesOrNo = "Yes";
            }
            else
            {
                yesOrNo = "No";
            }
            Console.WriteLine(yesOrNo + ": " + path);
            
        }

        private Queue<string> initAgenda()
        {
            Queue<string> agenda = new Queue<string>();
            foreach (Clause c in _knowledgeBase.Clauses)
            {
                if (c.Premise == null)
                {
                    agenda.Enqueue(c.Conclusion);
                }

            }
            return agenda;
        }

        private Dictionary<Clause, int> initCount()
        {
            Dictionary<Clause, int> count = new Dictionary<Clause, int>();
            
            foreach (Clause c in _knowledgeBase.Clauses)
            {
                
                if (c.Premise != null)
                {
                    count[c] = c.Premise.Count;
                }
                
            }
            return count;
        }

        private Dictionary<string, bool> initInferred()
        {
            Dictionary<string, bool> inferred = new Dictionary<string, bool>();
            List<string> symbols = _knowledgeBase.getSymbols();
            foreach (string symbol in symbols)
            {
                inferred[symbol] = false;
            }
            return inferred;
        }

        public bool PL_FC_Entails()
        {
            Dictionary<Clause, int> count = initCount();
            Dictionary<string, bool> inferred = initInferred();
            Queue<string> agenda = initAgenda();
            
            while (agenda.Count != 0)
            {
                string symbol = agenda.Dequeue();
                
                if (symbol == _query)
                {
                    _inferred = inferred;
                    _inferred[symbol] = true;
                    return true;
                }

                if (inferred[symbol] == false)
                {
                    inferred[symbol] = true;
                    foreach (Clause c in _knowledgeBase.Clauses)
                    {
                        if (c.Premise != null)
                        {
                            if (c.Premise.Contains(symbol))
                            {
                                count[c]--;
                                if (count[c] == 0)
                                {
                                    agenda.Enqueue(c.Conclusion);
                                }
                            }
                        }
                        
                    }
                }
            }
            return false;
        }
    }
}
