using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
    public class AdvancedKnowledgeBase
    {
        private List<LogicalExpression> _expressions;

        public AdvancedKnowledgeBase(List<LogicalExpression> expressions)
        {
            _expressions = expressions;
        }

        public List<LogicalExpression> Expressions
        {
            get { return _expressions; }
        }
    }
}
