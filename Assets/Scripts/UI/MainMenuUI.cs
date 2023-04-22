using CodeMonkey.KitchenChaosControl.Management;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));
            quitButton.onClick.AddListener(Application.Quit);

            Time.timeScale = 1f;
        }
    }
}
