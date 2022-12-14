using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public static class BootstrapHook
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            SceneManager.LoadScene("Bootstrap");
        }
    }
}
