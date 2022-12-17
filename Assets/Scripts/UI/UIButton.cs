using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField]
        public string loadedLevel;
        
        public Sprite disabledSprite;
        private UnityEngine.UI.Button _button;
        void Start()
        {
            _button = GetComponent<UnityEngine.UI.Button>();
        }

        void Update()
        {
            if (!_button.interactable)
            {
                _button.image.sprite = disabledSprite;
            }
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(loadedLevel);
        }
    }
}
