using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class Clause
    {
        private List<string> _premise;
        private string _conclusion;

        public Clause(string conclusion)
        {
            _conclusion = conclusion;
        }

        public Clause(List<string> premise, string conclusion)
        {
            _premise = premise;
            _conclusion = conclusion;
        }

        public List<string> Premise
        {
            get { return _premise; }
        }

        public string Conclusion
        {
            get { return _conclusion; }
        }

    }
}
