using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Algorithms.CspProblemSolver
{
    public abstract class ACspProblem
    {
        public int MaxVariableValue { get; protected set; }
        public int VariablesCount { get; protected set; }

        public bool Predicate(int elementIndex, List<int> variables)
        {
            if(GetCountOfBrokenConstraints(elementIndex, variables) > 0)
            {
                return false;
            }
            return true;
        }

        public abstract int GetCountOfBrokenConstraints(int elementIndex, List<int> variables);
    }
}
