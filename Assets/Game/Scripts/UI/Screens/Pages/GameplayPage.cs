using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.GameStateMachine;
using Game.Scripts.Infrastructure.LevelStateMachin;
using Game.Scripts.Services;
using Game.Scripts.UI.Screens.Base.Screens;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI.Screens.Pages
{
    public class GameplayPage : Page
    {
        [SerializeField] private Button _dropButton;
        
        private ItemPickupService _itemPickupService;
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [Inject]
        public void Construct(ItemPickupService itemPickupService)
        {
            _itemPickupService = itemPickupService;
            
            GlobalEventSystem.Broker.Receive<PlayerPickupItemEvent>()
                .Subscribe(_ => ShowDropButton())
                .AddTo(_disposable); 
        }

        private void ShowDropButton()
        {
            _dropButton.gameObject.SetActive(true);
        }

        public override void OnCreate()
        {
            base.OnCreate();
            
            _dropButton.onClick.AddListener(G_DropItem);
            _dropButton.gameObject.SetActive(false);
        }
        private void G_DropItem()
        {
            _itemPickupService.DropItem();
            _dropButton.gameObject.SetActive(false);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _dropButton.onClick.RemoveListener(G_DropItem);
            _disposable?.Dispose();
        }
    }
}