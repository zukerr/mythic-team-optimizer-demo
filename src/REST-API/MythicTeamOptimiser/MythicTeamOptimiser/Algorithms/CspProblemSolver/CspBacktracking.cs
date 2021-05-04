using MythicTeamOptimiser.Algorithms.Utility;
using MythicTeamOptimiser.Algorithms.ValidTeamCompsFinder;
using MythicTeamOptimiser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Algorithms.CspProblemSolver
{
    public class CspBacktracking
    {
        private ACspProblem inputProblem;
        private List<List<int>> allResults;

        int callCounter = 0;

        public CspBacktracking(ACspProblem inputProblem)
        {
            this.inputProblem = inputProblem;
            allResults = new List<List<int>>();
        }

        public string GetReadableResults()
        {
            allResults = allResults.Distinct(ListEqualityComparer<int>.Default).ToList();
            string result = "";
            foreach(List<int> elem in allResults)
            {
                result += GetReadableResult(elem);
                result += "\n";
            }
            return result;
        }

        public List<List<int>> GetRawResults()
        {
            allResults = allResults.Distinct(ListEqualityComparer<int>.Default).ToList();
            return allResults;
        }

        private string GetReadableResult(List<int> input)
        {
            string result = "";
            foreach (int character in input)
            {
                result += LookupSpec.Instance.GetSpecRoleFromInt(character).ToString() + ", ";
            }
            return result;
        }

        public void BacktrackFromRoot()
        {
            List<int> root = new List<int>();

            if(inputProblem is CspProblemSpecRoles)
            {
                PlayerCharacterModel pcm = (inputProblem as CspProblemSpecRoles).RootPlayer;
                root.Add(LookupSpec.Instance.GetIntFromPlayerCharacterModel(pcm));
            }
            
            Backtrack(root);
        }

        private void Backtrack(List<int> c)
        {
            callCounter++;
            //Console.WriteLine($"Call counter: {callCounter}");

            if(Reject(inputProblem, c))
            {
                return;
            }
            if(Accept(inputProblem, c))
            {
                Output(inputProblem, c);
            }
            List<int> s = First(inputProblem, c);
            while(s != null)
            {
                Backtrack(s);
                s = Next(inputProblem, s);
            }
        }

        private bool Reject(ACspProblem problem, List<int> c)
        {
            bool result = true;
            for(int i = 0; i < c.Count; i++)
            {
                if(result)
                {
                    result = problem.Predicate(i, c);
                }
            }

            //heuristics of variable choice
            if(c.Count > 0)
            {
                //if given player is a dps
                if (c[0] >= 12)
                {
                    if(c.Count > 1)
                    {
                        //if second player is not a tank
                        if (c[1] >= 6)
                        {
                            return true;
                        }
                        else
                        {
                            if(c.Count > 2)
                            {
                                //if third player is not a healer
                                if (c[2] >= 12 || c[2] < 6)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                //if given player is a tank
                else if (c[0] < 6)
                {
                    if(c.Count > 1)
                    {
                        //if second player is not a healer
                        if (c[1] >= 12 || c[1] < 6)
                        {
                            return true;
                        }
                    }
                }
                //if given player is a healer
                else if (c[0] < 12 && c[0] >= 6)
                {
                    if (c.Count > 1)
                    {
                        //if second player is not a tank
                        if (c[1] >= 6)
                        {
                            return true;
                        }
                    }
                }
            }
            
            return !result;
        }

        private bool Accept(ACspProblem problem, List<int> c)
        {
            if(c.Count == problem.VariablesCount)
            {
                return !Reject(problem, c);
            }
            else
            {
                return false;
            }
        }

        private List<int> First(ACspProblem problem, List<int> c)
        {
            int k = c.Count;
            if(k == problem.VariablesCount)
            {
                return null;
            }
            else
            {
                List<int> moddedC = new List<int>();
                for(int i = 0; i < k; i++)
                {
                    moddedC.Add(c[i]);
                }
                moddedC.Add(0);
                return moddedC;
            }
        }

        private List<int> Next(ACspProblem problem, List<int> s)
        {
            int k = s.Count;
            if(s[k - 1] == problem.MaxVariableValue)
            {
                return null;
            }
            else
            {
                List<int> moddedS = new List<int>();
                for(int i = 0; i < k; i++)
                {
                    moddedS.Add(s[i]);
                }
                moddedS[k - 1] += 1;
                return moddedS;
            }
        }

        private void Output(ACspProblem problem, List<int> c)
        {
            allResults.Add(c);
            //Console.WriteLine($"New solution: {GetReadableResult(c)}");
            allResults = allResults.Distinct().ToList();
        }
    }
}
