namespace SDMproject;

class SDMproject
{
    static int Main()
    {
        var GroupElimination = new GroupElimination(1,2);
        List<FootBallTeam> teams = {new FootBallTeam("anglia"), new FootBallTeam("polska"),new FootBallTeam("francja"), new FootBallTeam("hiszpania")};
        GroupElimination.ProvideParticipants(teams);
        GroupElimination.Step();

        //...
        return 0;
    }
}
