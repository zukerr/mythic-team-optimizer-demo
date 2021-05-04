using MythicTeamOptimiser.Algorithms.ValidTeamCompsFinder;
using MythicTeamOptimiser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Algorithms.CspProblemSolver
{
    public class CspProblemSpecRoles : ACspProblem
    {
        private PlayerCharacterModel rootPlayer;
        public PlayerCharacterModel RootPlayer
        {
            get
            {
                return rootPlayer;
            }
        }

        public CspProblemSpecRoles(PlayerCharacterModel rootPlayer)
        {
            this.rootPlayer = rootPlayer;
            MaxVariableValue = LookupSpec.Instance.GetSpecsCount() - 1;
            VariablesCount = 5;
        }

        public override int GetCountOfBrokenConstraints(int elementIndex, List<int> variables)
        {
            int result = 0;

            SpecRole target = LookupSpec.Instance.GetSpecRoleFromInt(variables[elementIndex]);

            if(target.Role == CharacterRole.Tank)
            {
                int tankCount = 0;
                foreach(int elem in variables)
                {
                    if(LookupSpec.Instance.GetSpecRoleFromInt(elem).Role == CharacterRole.Tank)
                    {
                        tankCount++;
                    }
                }
                if(tankCount > 1)
                {
                    result++;
                }
            }
            else if (target.Role == CharacterRole.Healer)
            {
                int healerCount = 0;
                foreach (int elem in variables)
                {
                    if (LookupSpec.Instance.GetSpecRoleFromInt(elem).Role == CharacterRole.Healer)
                    {
                        healerCount++;
                    }
                }
                if (healerCount > 1)
                {
                    result++;
                }
            }
            else if (target.Role == CharacterRole.Dps)
            {
                int dpsCount = 0;
                foreach (int elem in variables)
                {
                    if (LookupSpec.Instance.GetSpecRoleFromInt(elem).Role == CharacterRole.Dps)
                    {
                        dpsCount++;
                    }
                }
                if (dpsCount > 3)
                {
                    result++;
                }
            }

            bool givenRootPlayerExists = false;
            foreach (int elem in variables)
            {
                if (LookupSpec.Instance.GetSpecRoleFromInt(elem).CharClass == rootPlayer.ClassName)
                {
                    if(LookupSpec.Instance.GetSpecRoleFromInt(elem).Spec == rootPlayer.SpecName)
                    {
                        givenRootPlayerExists = true;
                    }
                }
            }
            if(!givenRootPlayerExists)
            {
                result++;
            }

            return result;
        }
    }
}
