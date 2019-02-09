using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class NewsCarouselViewModel : PropertyChangedBase
    {
        public NewsModel PrimaryNews { get; private set; }
        public BindableCollection<NewsModel> NewsCollection { get; private set; }

        public NewsCarouselViewModel()
        {
            var dicPic =
                "https://scontent-waw1-1.xx.fbcdn.net/v/t1.0-9/48381819_1430635463734644_3734049519239692288_n.jpg?_nc_cat=108&_nc_ht=scontent-waw1-1.xx&oh=78baa8d9ec33c2277aeaae214ab21f3a&oe=5CF5C8DD";

            PrimaryNews = new NewsModel
            {
                Title = "Jakub Maciejewski to fajny kolega",
                Description = "Wyszło tak, a nie inaczej. Komentował dla was Adam Skóra",
                ImageSource = dicPic
            };

            NewsCollection = new BindableCollection<NewsModel>()
            {
                new NewsModel(){ImageSource = dicPic},
                new NewsModel(){ImageSource = dicPic},
            };
        }


    }
}
