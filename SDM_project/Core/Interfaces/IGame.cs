using SDMproject.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Maybe change into an abstract class?
namespace SDMproject.Core.Interfaces
{
    internal interface IGame
    {
        string Name { get; }
        ImmutableList<TeamScore> Teams { get; }

        public string DetermineVictor();
        public IReadOnlyList<int> DeterminePlacing();
    }
}
