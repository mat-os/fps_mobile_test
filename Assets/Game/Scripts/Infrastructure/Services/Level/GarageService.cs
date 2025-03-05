using Game.Scripts.LevelElements;
using Zenject;

namespace Game.Scripts.Infrastructure.Services
{
    public class GarageService
    {
        private ShelfFillerService _shelfFillerService;

        [Inject]
        public GarageService(ShelfFillerService shelfFillerService)
        {
            _shelfFillerService = shelfFillerService;
        }
        public void Initialize(LevelView levelView)
        {
            foreach (var shelf in levelView.GarageView.Shelves)
            {
                _shelfFillerService.FillShelf(shelf);
            }
        }
    }
}