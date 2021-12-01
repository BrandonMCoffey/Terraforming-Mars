using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu = null;

        public static PauseMenuController Instance;

        private bool _paused;

        private void Awake()
        {
            Instance = this;
        }

        public void Pause(bool forcePause = false)
        {
            _paused = !_paused || forcePause;
            _pauseMenu.SetActive(_paused);
        }

        public void QuitGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}