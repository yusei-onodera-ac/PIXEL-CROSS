using PixelCross.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCross.UI
{
    public class ScoutScreenController : MonoBehaviour
    {
        [SerializeField] private TMP_Text ticketsLabel;
        [SerializeField] private TMP_InputField prospectNameField;
        [SerializeField] private Button scoutButton;
        [SerializeField] private TMP_Text resultLabel;

        private void OnEnable()
        {
            scoutButton.onClick.AddListener(OnScoutPressed);
            Refresh();
        }

        private void OnDisable()
        {
            scoutButton.onClick.RemoveListener(OnScoutPressed);
        }

        private void Refresh()
        {
            var gm = GameManager.Instance;
            ticketsLabel.text = $"スカウトチケット: {gm.PlayerTeam.ScoutTickets}枚";
            scoutButton.interactable = gm.IsScoutingWindowOpen && gm.PlayerTeam.ScoutTickets > 0;

            if (!gm.IsScoutingWindowOpen)
            {
                resultLabel.text = "スカウトは夏合宿期間のみ実施できます";
            }
        }

        private void OnScoutPressed()
        {
            var name = string.IsNullOrWhiteSpace(prospectNameField.text) ? "新入生候補" : prospectNameField.text.Trim();
            var result = GameManager.Instance.TryScoutRecruit(name);

            resultLabel.text = result.Success
                ? $"{result.Recruit.Name} の獲得に成功しました！"
                : "スカウトに失敗しました…";

            prospectNameField.text = string.Empty;
            Refresh();
        }
    }
}
