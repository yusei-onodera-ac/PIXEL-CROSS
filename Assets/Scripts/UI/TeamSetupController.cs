using PixelCross.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PixelCross.UI
{
    // Tutorial step 1 ("監督就任＆チーム結成"): name the university/manager,
    // create the team, then hand off to gameplay.
    public class TeamSetupController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField universityNameField;
        [SerializeField] private TMP_InputField managerNameField;
        [SerializeField] private Button confirmButton;

        private void Start()
        {
            universityNameField.onValueChanged.AddListener(_ => RefreshConfirmState());
            managerNameField.onValueChanged.AddListener(_ => RefreshConfirmState());
            confirmButton.onClick.AddListener(OnConfirmPressed);
            RefreshConfirmState();
        }

        private void RefreshConfirmState()
        {
            confirmButton.interactable =
                !string.IsNullOrWhiteSpace(universityNameField.text) &&
                !string.IsNullOrWhiteSpace(managerNameField.text);
        }

        private void OnConfirmPressed()
        {
            var universityName = universityNameField.text.Trim();
            var managerName = managerNameField.text.Trim();

            GameManager.Instance.StartNewGame(universityName, managerName);
            GameManager.Instance.Tutorial.AdvanceStep();

            SceneManager.LoadScene(SceneNames.Gameplay);
        }
    }
}
