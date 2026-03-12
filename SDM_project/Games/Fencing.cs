using SDMproject.Core.Interfaces;
using SDMproject.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDMproject.Games
{
    internal class Fencing : IGame
    {
        public string Name => "Fencing";
        public ImmutableList<TeamScore> Teams { get; }

        public Fencing(IEnumerable<TeamScore> teams)
        {
            if (teams.Count() != 2)
                throw new ArgumentException("Fencing should have exactly two teams.");

            Teams = [.. teams];
        }

        public IReadOnlyList<TeamScore> DetermineVictors()
        {
            int maxScore = Teams.Max(t => t.Score);
            return [.. Teams.Where(t => t.Score == maxScore)];
        }

        public IReadOnlyList<TeamScore> DeterminePlacing()
        {
            return [.. Teams.OrderByDescending(t => t.Score)];
        }
    }
}
