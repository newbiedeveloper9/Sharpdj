using Communication.Shared;

namespace Communication.Client
{
    public class UserClient
    {
        public UserClient()
        {

        }

        public UserClient(long id, string username, Rank rank)
        {
            Id = id;
            Username = username;
            Rank = rank;
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public Rank Rank { get; set; } = Rank.User;

        public override bool Equals(object obj)
        {
            if (!(obj is UserClient)) return false;
            var eq = (UserClient)obj;

            return Rank == eq.Rank &&
                   Username == eq.Username &&
                   Id == eq.Id;
        }
    }
}
