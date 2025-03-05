using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Initialization
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private AppCore _appCorePrefab;
        [SerializeField] private int _targetFramerate = 60;

        private async void Start()
        {
            await UniTask.WaitWhile(() => !UnityEngine.Rendering.SplashScreen.isFinished);
            
            AppCore appCore = Instantiate(_appCorePrefab, Vector3.zero, Quaternion.identity);
            appCore.InitializeGame();
            
            await SceneManager.LoadSceneAsync(AppConst.GameProd, LoadSceneMode.Single);

            Application.targetFrameRate = _targetFramerate;

            appCore.EnterGame();
        }
    }
}