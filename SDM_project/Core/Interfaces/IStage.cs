namespace SDMproject.Core.Interfaces
{
    public interface IStage
    {
        public void ProvideParticipants(IEnumerable<IParticipant> players);
        public bool Step();
        public IEnumerable<IParticipant> AdvancingParticipants();
        public void PrintGames();


    }
}