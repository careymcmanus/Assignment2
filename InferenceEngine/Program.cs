using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    class Program
    {
        static KnowledgeBase _kb;
        static AdvancedKnowledgeBase _akb;
        static string _query;
        static Engine _engine;

        /*
         * Main entry point for program.
         */
        static void Main(string[] args)
        {
            if (ReadProblem(args[1], args[0]))
            {
                //Determines which algorithm to use for solving problem
                switch (args[0])
                {
                    case ("TT"):
                        _engine = new TruthTable(_kb, _query);
                        break;
                    case ("BC"):
                        _engine = new BackwardChaining(_kb, _query);
                        break;
                    case ("FC"):
                        _engine = new ForwardChaining(_kb, _query);
                        break;
                    case ("GTT"):
                        _engine = new ExtendedTruthTable(_akb, _query);
                        break;
                    case ("DPLL"):
                        _engine = new DPLL();
                        break;
                    default:
                        throw new System.ArgumentException("No Valid Inference Method Given");
                }
            }
            //Runs the selected engine.
            _engine.Solve();
        }

        /*
         * Reads Problem File 
         * @Param filename - string that gives name of file to read from
         * @Param solver - string that tells reader what type of engine is being used. Changes
         *      output if more advanced algorithms used.
         */
        public static bool ReadProblem(string filename, string solver)
        {
            List<string> text = new List<string>();

            //tries to read problem file, if it can't returns false
            try
            {
                StreamReader reader = File.OpenText(filename);
                for (int i = 0; i < 4; i++)
                {
                    text.Add(reader.ReadLine());
                }
                reader.Close();
            }
            catch
            {
                Console.WriteLine("Error: Could not read file");
                return false;
            }
            string[] knowledge = text[1].Split(';');
            knowledge = knowledge.Take(knowledge.Count() - 1).ToArray();
            List<Clause> clauses = new List<Clause>();
            // If basic checking method 
            if (solver != "GTT" || solver != "DPLL")
            {
                foreach (string s in knowledge)
                {
                    if (s.Contains("=>"))
                    {
                        List<string> premiseSymbols = new List<string>();
                        int index = s.IndexOf("=>");
                        string premise = s.Substring(0, index);
                        string conclusion = s.Substring(index + 2);
                        conclusion = conclusion.Trim();
                        string[] splitPremise = { "" };
                        if (premise.Contains("&"))
                        {
                            splitPremise = premise.Split('&');
                        }
                        else
                        {
                            splitPremise[0] = premise;
                        }
                        foreach (string symbol in splitPremise)
                        {
                            string trim = symbol.Trim();
                            premiseSymbols.Add(trim);
                        }
                        clauses.Add(new Clause(premiseSymbols, conclusion));
                    }
                    else
                    {
                        string conclusion = s.Trim();
                        clauses.Add(new Clause(conclusion));
                    }
                }
                _query = text[3];
                _kb = new KnowledgeBase(clauses);


                return true;
            }
            // if solving method more advanced clauses
            else
            {
                List<LogicalExpression> kb = new List<LogicalExpression>(); 
                foreach (string s in knowledge)
                {
                    
                    LogicalExpression exp = new LogicalExpression(s);
                    //exp.printInfo();
                    kb.Add(exp);
                }
                
                _akb = new AdvancedKnowledgeBase(kb);
                _query = text[3];
                return true;
            }
            
            
        }


    }
}

