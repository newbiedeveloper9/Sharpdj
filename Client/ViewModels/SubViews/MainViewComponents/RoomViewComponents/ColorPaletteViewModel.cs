using Caliburn.Micro;
using SCPackets.Models;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.RoomViewComponents
{
    public class ColorPaletteViewModel : PropertyChangedBase, 
        IHandle<INickColorChanged>
    {
        private Color _selectedColor = new Color().SetColor(new byte[] {105, 105, 255});
        public Color SelectedColor
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
        private BindableCollection<Color> _colorCollection;
        public BindableCollection<Color> ColorCollection
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

            SetTestColorPalette();
        }

        public ColorPaletteViewModel()
        {
            SetTestColorPalette();
        }

        private void SetTestColorPalette()
        {
            ColorCollection = new BindableCollection<Color>()
            {
                new Color().SetColor(new byte[]{255,255,255}),
                new Color().SetColor(new byte[]{100,100,100}),
                new Color().SetColor(new byte[]{95,20,55}),
                new Color().SetColor(new byte[]{55,210,22}),
                new Color().SetColor(new byte[]{100,2,100}),
                new Color().SetColor(new byte[]{5,5,150}),
            };
        }
        #endregion .ctor

        #region Methods

        public void SetNickColor(Color model)
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
