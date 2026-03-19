namespace SDMproject.Core.Interfaces
{
    public class FootBallTeam : IParticipant
    {
        private sting name;
        FootBallTeam(string _name)
        {
            name = _name;
        }
        public void PPrint()
        {
            Console.Write(name);
        }
        public void ShortPrint()
        {
            
        }
    }
}
