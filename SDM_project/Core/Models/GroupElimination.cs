namespace SDMproject.Core.Models;

public  class GroupElimination : IStage
{
        private record Matchup{
            public IParticipant first;
            public IParticipant second;
        }

        private List<List<IParticipant> groups;
        private int advancingFromGroup;
        private int currentGroup =0;
        private IEnumerable<Matchup> groupMatchups;

        GroupElimination(int groupCount,int _advancingFromGroup)
        {
            groups = new List<List<IParticipant>>();
            for(int i=0;i<groupCount;i++)
            {
                groups.PushBack(new List<IParticipant>())
            }
            advancingFromGroup = _advancingFromGroup;
            
            var group = groups[currentGroup];
            groupMatchups = from p1 in group from p2 in group select new Matchup(p1,p2) where p1 != p2;

        }

        public void ProvideParticipants(IEnumerable<IParticipant> players)
        {
            var i = groups.count;
            foreach (var player in players)
            {
                groups[i].PushBack(player);
            }


        }
        public bool Step(){
            foreach(var matchup in groupMatchups)
            {
                Console.WriteLine($"{matchup.first.PPrint()} is fighting {matchup.second.PPrint()}");
            }

        }
        public IEnumerable<IParticipant> AdvancingParticipants();
        public void PrintGames();
}