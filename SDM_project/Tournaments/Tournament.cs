using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDMproject.Core.Interfaces;
using SDMproject.Core.Models;
using SDMproject.Games;

namespace SDMproject.Tournaments
{
    internal class Tournament
    {
        public ImmutableList<string> Teams { get; }
        public List<List<string>> StageMatches => _stageMatches;
        public List<string> RemainingTeams => _remainingTeams;

        private readonly GameType game;
        private readonly int NumTeamsInMatch;
        private readonly bool RandomStageMatches;
        private List<List<string>> _stageMatches;
        private List<string> _remainingTeams;

        public Tournament(GameType gameType, IEnumerable<string> teams, int numTeamsInMatch = 2, bool randomStageMatches = false)
        {
            if (teams.Count() < 2)
                throw new ArgumentException("Tournament should have at least two teams.");

            game = gameType;

            NumTeamsInMatch = numTeamsInMatch;

            Teams = [.. teams];
            _remainingTeams = [.. teams];

            RandomStageMatches = randomStageMatches;
            _stageMatches = DetermineStageMatches();
        }

        public string PlayMatch(IEnumerable<(string, int)> teamAndScores)
        {
            List<TeamScore> teamScores = [];
            foreach((string, int) teamAndScore in teamAndScores)
            {
                teamScores.Add(new TeamScore(teamAndScore.Item1, teamAndScore.Item2));
            }

            return PlayMatch(teamScores);
        }

        public string PlayMatch(List<TeamScore> teams)
        {
            List<string> set = [];
            foreach (TeamScore team in teams)
            {
                set.Add(team.Team);
            }

            if (!_stageMatches.Contains(set))
                throw new ArgumentException("The provided teams are not scheduled to play against each other in the current stage.");
            
            IGame match = CreateGame(teams);
            IReadOnlyList<TeamScore> victors = match.DetermineVictors();

            _stageMatches.Remove(set);
            if (victors.Count < teams.Count)
                return "No team was eliminated in this match.";

            List<string> eliminatedTeams = [];
            foreach(TeamScore team in teams)
            {
                if (!victors.Contains(team))
                {
                    RemainingTeams.Remove(team.Team);
                    eliminatedTeams.Add(team.Team);
                }
            }

            string endInfo = "";
            if (_stageMatches.Count == 0)
            {
                if (_remainingTeams.Count >= NumTeamsInMatch)
                {
                    _stageMatches = DetermineStageMatches();
                    endInfo = "\nThe stage has ended, starting another one.";
                }
                else
                {
                    endInfo = "\nThe tournament has ended";
                }
            }

            return $"Team{(eliminatedTeams.Count > 1 ? "s" : "")} {eliminatedTeams} have been eliminated.{endInfo}";
        }

        private List<List<string>> DetermineStageMatches()
        {
            if (_remainingTeams.Count < NumTeamsInMatch)
                throw new InvalidOperationException("Not enough teams to determine stage matches."); 

            if (RandomStageMatches)
            {
                var rnd = new Random();
                _remainingTeams = [.. _remainingTeams.OrderBy(_ => rnd.Next())];
            }

            List<List<string>> sm = [];
            List<string> d = [];
            foreach (string team in _remainingTeams)
            {
                d.Add(team);

                if (d.Count == NumTeamsInMatch)
                {
                    sm.Add(d);
                    d.Clear();
                }
            }

            return sm;
        }

        private IGame CreateGame(List<TeamScore> teams)
        {
            return game switch
            {
                GameType.Fencing => new Fencing(teams),
                GameType.Football => new Football(teams),
                _ => throw new NotImplementedException("Tournament does not have this game implemented yet."),
            };
        }
    }
}
