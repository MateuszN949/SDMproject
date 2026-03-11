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
    internal class Fencing(IEnumerable<TeamScore> teams) : IGame
    {
        public string Name => "Fencing";
        public ImmutableList<TeamScore> Teams { get; } = [.. teams];

        public IReadOnlyList<int> DeterminePlacing()
        {
            //...
            throw new NotImplementedException();
        }

        public string DetermineVictor()
        {
            //...
            throw new NotImplementedException();
        }
    }
}
