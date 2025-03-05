using Game.Scripts.Core.Update;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.States;
using Game.Scripts.Services;
using Game.Scripts.UI.Screens.Serviсes;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Initialization.ZenjectInstallers
{
    public class CommonDependenciesInstaller : MonoInstaller
    {
        [SerializeField] private PageService _pageService;
        [SerializeField] private PopupService _popupService;
        [SerializeField] private MessageBoxService _messageBoxService;
        [SerializeField] private UpdateService _updateService;
        
        public override void InstallBindings()
        {
            #region Core

            Container.Bind<InputService>().AsSingle();
            
            Container.Bind<UpdateService>().FromInstance(_updateService).AsSingle();

            Container.Bind<LevelBuilderService>().AsSingle();
            Container.Bind<LevelDataService>().AsSingle();

            #endregion

            #region Level

            Container.Bind<ShelfFillerService>().AsSingle();
            Container.Bind<GarageService>().AsSingle();

            #endregion
            
            #region Player
            
            Container.Bind<PlayerService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ItemSelectService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemPickupService>().AsSingle();
            
            #endregion

            #region UI

            Container.Bind<ScreensService>().AsSingle();
            Container.Bind<PageService>().FromInstance(_pageService).AsSingle();
            Container.Bind<PopupService>().FromInstance(_popupService).AsSingle();
            Container.Bind<MessageBoxService>().FromInstance(_messageBoxService).AsSingle();
            
            #endregion
        }
    }
}