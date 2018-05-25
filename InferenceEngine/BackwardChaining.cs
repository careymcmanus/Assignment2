using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    /*
     * Backward Chaining Algorithm Class
     * Contains a Knowledge Base
     * Contains a Query string
     * Contains a List of known facts
     * Contains a List of checked symbols
     */
    public class BackwardChaining : Engine
    {
        private KnowledgeBase _knowledgeBase;
        private string _query;
        private List<string> _facts;
        private List<string> _checked = new List<string>();

        /*
         * Constructor for the Backward Chaining algorithm 
         * @Param kb - A Knowledge Base
         * @Param query - Query to check
         * gets a list of facts from the knowledge base
         */
        public BackwardChaining(KnowledgeBase kb, string query)
        {
            _knowledgeBase = kb;
            _query = query;
            _facts = kb.getFacts();
        }

        /*
         * Initializes an agenda stack to contain the initial query
         */
        private Stack<string> initAgenda(string query)
        {
            Stack<string> agenda = new Stack<string>();
            agenda.Push(query);
            return agenda;
        }

        /*
         * Solve function calls the backward chaining algorithm
         * Prints answer to screen.
         */
        public override void Solve()
        {
            string facts = String.Join("; ", _facts.ToArray());
            Console.WriteLine("Query: " + _query);
            Console.WriteLine("facts: " + facts);
            bool result = PL_BC_Entails(_query);
            _checked.Reverse();
            string entailed = String.Join(",", _checked.ToArray());
            string yesOrNo;
            if (result)
            {
                yesOrNo = "Yes";
            }
            else
            {
                yesOrNo = "No";
            }
            Console.WriteLine(yesOrNo+ ": "+ entailed);
        }

        public bool PL_BC_Entails(string query)
        {
            Stack<string> agenda = initAgenda(query);
            
            string searching;
            while (agenda.Count != 0)
            {
                searching = agenda.Pop();
                searching = searching.Trim();
                
                _checked.Add(searching);
               
                if (!_facts.Contains(searching))
                {
                    List<Clause> containsQuery = new List<Clause>();
                    
                    foreach (Clause c in _knowledgeBase.Clauses)
                    {
                        if (c.Conclusion.Contains(searching)){
                            containsQuery.Add(c);
                        }
                    }
                    if (containsQuery.Count == 0)
                    {
                        
                        return false;
                    }
                    else
                    {
                        foreach (Clause c in containsQuery)
                        {   
                            
                            foreach (string s in c.Premise)
                            {
                                
                                if (!_checked.Contains(s))
                                {
                                    
                                    agenda.Push(s);
                                }
                            }
                        }
                    }
                    
                }
            }

            return true;
            
            
        }
        
    }
}
