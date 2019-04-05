using System;
using Caliburn.Micro;
using SCPackets.Models;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.RoomViewComponents
{
    public class ColorPaletteViewModel : PropertyChangedBase, 
        IHandle<INickColorChanged>
    {
        private ColorModel _selectedColor = new ColorModel(255,255,255);
        public ColorModel SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor == value) return;
                _selectedColor = value;
                NotifyOfPropertyChange(() => SelectedColor);

                _eventAggregator.PublishOnUIThread(
                    new NickColorChanged(value));
            }
        }

        private readonly IEventAggregator _eventAggregator;
        private BindableCollection<ColorModel> _colorCollection;
        public BindableCollection<ColorModel> ColorCollection
        {
            get => _colorCollection;
            set
            {   
                if (_colorCollection == value) return;
                _colorCollection = value;
                NotifyOfPropertyChange(() => ColorCollection);
            }
        }


        #region .ctor
        public ColorPaletteViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            ColorCollection = new BindableCollection<ColorModel>()
            {
                new ColorModel(255,255,255),
                new ColorModel(100,100,100),
                new ColorModel(95,20,55),
                new ColorModel(55,210,22),
                new ColorModel(100,2,100),
                new ColorModel(5,5,150),
            };
        }

        public ColorPaletteViewModel()
        {
            ColorCollection = new BindableCollection<ColorModel>()
            {
                new ColorModel(255,255,255),
                new ColorModel(100,100,100),
                new ColorModel(95,20,55),
                new ColorModel(55,210,22),
                new ColorModel(100,2,100),
                new ColorModel(5,5,150),
            };
        }
        #endregion .ctor

        #region Methods

        public void SetNickColor(ColorModel model)
        {
            SelectedColor = model;
        }
        #endregion Methods

        public void Handle(INickColorChanged message)
        {
            SelectedColor = message.Color;
        }
    }
}
