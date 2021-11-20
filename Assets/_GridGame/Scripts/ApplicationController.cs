using Scripts.Grid;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class ApplicationController : MonoBehaviour
    {
        [SerializeField] private GridCreator _gridCreator = null;

        private void Start()
        {
            if (_gridCreator == null) {
                _gridCreator = FindObjectOfType<GridCreator>();
            }
        }

        public void StartGame()
        {
            _gridCreator.SaveGrid();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void CloseApplication()
        {
            // TODO: Popup confirmation menu
            Application.Quit();
        }
    }
}