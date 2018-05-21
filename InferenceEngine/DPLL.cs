using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class DPLL:Engine
    {
        private AdvancedKnowledgeBase _kb;
        private string _query;
        public DPLL()
        {
           
        }

        public override void Solve()
        {
            throw new NotImplementedException();
        }

        private void ConvertToCNF()
        {

        }
    }
}
