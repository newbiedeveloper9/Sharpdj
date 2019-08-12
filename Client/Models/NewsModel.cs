using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SCPackets.Models;

namespace SharpDj.Models
{
    public class NewsModel : PropertyChangedBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                NotifyOfPropertyChange(() => Id);

                IsCreated = value != -1;
                NotifyOfPropertyChange(() => IsCreated);
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set
            {
                if (_imageSource == value) return;
                _imageSource = value;
                NotifyOfPropertyChange(() => ImageSource);
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        private bool _isCreated = false;
        public bool IsCreated
        {
            get => _isCreated;
            set
            {
                if (_isCreated == value) return;
                _isCreated = value;
                NotifyOfPropertyChange(() => IsCreated);
            }
        }

        public NewsModel()
        {

        }

        public static NewsModel CreateModel(RoomOutsideModel model)
        {
            return new NewsModel()
            {
                Title = model.Name,
                ImageSource = model.ImagePath,
                Id = model.Id,
                Description = "test",
            };
        }
    }
}
