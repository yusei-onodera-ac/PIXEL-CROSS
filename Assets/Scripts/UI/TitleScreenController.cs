using PixelCross.Core;
using PixelCross.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PixelCross.UI
{
    public class TitleScreenController : MonoBehaviour
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button settingsButton;

        private void Start()
        {
            continueButton.interactable = SaveSystem.HasSave();

            newGameButton.onClick.AddListener(OnNewGamePressed);
            continueButton.onClick.AddListener(OnContinuePressed);
            settingsButton.onClick.AddListener(OnSettingsPressed);
        }

        private void OnNewGamePressed()
        {
            SceneManager.LoadScene(SceneNames.TeamSetup);
        }

        private void OnContinuePressed()
        {
            GameManager.Instance.LoadGame();
            SceneManager.LoadScene(SceneNames.Gameplay);
        }

        private void OnSettingsPressed()
        {
            SceneManager.LoadScene(SceneNames.Settings);
        }
    }
}
