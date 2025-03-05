using System;
using DG.Tweening;
using Game.Scripts.Infrastructure;
using UniRx;
using UnityEngine;

namespace Game.Scripts.LevelElements.Doors
{
    public class DoorController : MonoBehaviour, IDoorController
    {
        [SerializeField]private DOTweenAnimation _doorAnimation;
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        private void Start()
        {
            GlobalEventSystem.Broker.Receive<StartPlayLevelEvent>()
                .Subscribe(_ => OpenDoor())
                .AddTo(_disposable); 
        }
        public void OpenDoor()
        {
            _doorAnimation.DOPlay();
        }
        public void CloseDoor()
        {
            _doorAnimation.DORewind();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}