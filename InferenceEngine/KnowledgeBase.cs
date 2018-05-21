using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class KnowledgeBase
    {
        private List<Clause> _clauses;

        public KnowledgeBase(List<Clause> clauses)
        {
            _clauses = clauses;
        }

        public List<Clause> Clauses
        {
            get { return _clauses; }
        }

        public List<string> getSymbols()
        {
            List<string> symbolList = new List<string>();
            foreach (Clause c in _clauses)
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

        public List<string> getFacts()
        {
            List<string> symbolList = new List<string>();
            foreach (Clause c in _clauses)
            {
                if (c.Premise is null)
                {
                    symbolList.Add(c.Conclusion);
                }
            }
            return symbolList;
        }
       
    }
}
