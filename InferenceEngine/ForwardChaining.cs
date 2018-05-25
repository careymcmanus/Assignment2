using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    /*
     * Class for Forward Chaining Algorithm 
     * Contains a knowledge base
     * Contains a Query to determine from knowledge base
     * Contains a Dictionary that maps true false values to symbols
     */
    public class ForwardChaining : Engine
    {
        private KnowledgeBase _knowledgeBase;
        private string _query;
        private Dictionary<string, bool> _inferred;

        /*
         * Constructor for Forward Chaining Algorithm
         * @param kb - a knowledge base
         * @param Query - A string for the query to ask
         */
        public ForwardChaining(KnowledgeBase kb, string Query)
        {
            _knowledgeBase = kb;
            _query = Query;
        }

        /*
         * Function called to run the algorithm 
         * Prints true or false depending on answer 
         * Prints the list of symbols that were inferred while
         * Searching for goal. 
         */
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

        /*
         * Initializes a Queue of strings that are the symbols
         * to check. Determines whether a clause in the knowledge 
         * base is known by checking if there is no premise. If this
         * is case adds clause to agenda. 
         */
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

        /*
         * Initializes a dictionary that is used to determine
         * when a clause has been resolved. 
         * Initializes count for a clause with the number
         * of items in its premise.
         */
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

        /*
         * Initializes a Dictionary that determines whether
         * a symbol has been inferred.
         * Initializes all symbols to be false.
         */
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

        /*
         * Main Algorithm for Forward Chaining
         * While the agenda has items in it takes the first item
         * checks if the item is the query. 
         * Else checks if item has already been inferred
         * If not then checks whether any clauses contain the item
         * and reduces that clauses count if it does. if the clauses
         * count reaches 0 then the conclusion is added to agenda.
         * When agenda is empty returns false as query not found.
         * @var count
         * @var inferred
         * @var agenda
         */
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
