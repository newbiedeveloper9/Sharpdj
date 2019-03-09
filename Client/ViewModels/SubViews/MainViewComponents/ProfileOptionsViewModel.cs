using Caliburn.Micro;
using SCPackets;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class ProfileOptionsViewModel : PropertyChangedBase
    {
        public Rank Role { get; set; } = Rank.Admin;
        public string Username { get; set; } = "Crisey";

        public ProfileOptionsViewModel()
        {
            
        }

        public void ShowProfile()
        {

        }
    }
}
