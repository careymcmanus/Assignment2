using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    /*
     * Simple Knowledge base that contains a List of Horn clauses 
     */
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

        /*
         * Function for extracting the symbols that are contained 
         * within the knowledge base. 
         */
        public List<string> getSymbols()
        {
            List<string> symbolList = new List<string>();
            foreach (Clause c in _clauses)
            {
                //if there is a premise in clause
                if (c.Premise != null)
                {
                    foreach (string s in c.Premise)
                    {
                        //checks is symbol list already has the symbol
                        if (!symbolList.Contains(s))
                        {
                            symbolList.Add(s);
                        }
                    }
                }
                checks if symbol list already contains the conclusion
                if (!symbolList.Contains(c.Conclusion))
                {
                    symbolList.Add(c.Conclusion);
                }
            }
            return symbolList;
        }

        /*
         * a Function that returns a list of known facts from the
         * knowledge base
         */
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
