using Microsoft.AspNetCore.SignalR;

namespace MydemoFirst.Signal
{
    public class MeatHub : Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            return FavoriteMeat.VoteFavoriteMeat;
        }

    }

    public static class FavoriteMeat
    {

        public const string Chicken = "chicken";
        public const string Duck = "duck";
        public const string Cow = "cow";
        static FavoriteMeat()
        {
            VoteFavoriteMeat = new Dictionary<string, int>();
            VoteFavoriteMeat.Add(Cow, 0);
            VoteFavoriteMeat.Add(Chicken, 0);
            VoteFavoriteMeat.Add(Duck, 0);
        }

        public static Dictionary<string, int> VoteFavoriteMeat;


    }
}
