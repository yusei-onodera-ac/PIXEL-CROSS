using PixelCross.Core;
using PixelCross.Data;
using PixelCross.Training;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCross.UI
{
    public class PracticeScreenController : MonoBehaviour
    {
        [SerializeField] private Transform playerListContainer;
        [SerializeField] private ActionRowView playerRowPrefab;
        [SerializeField] private TMP_Text selectedPlayerLabel;
        [SerializeField] private TMP_Text resultLabel;
        [SerializeField] private Button strengthTrainingButton;
        [SerializeField] private Button runningAgilityButton;
        [SerializeField] private Button tacticsPassDrillButton;
        [SerializeField] private Button goalkeeperTrainingButton;

        private PlayerData _selectedPlayer;

        private void Start()
        {
            strengthTrainingButton.onClick.AddListener(() => ApplyTraining(TrainingMenu.StrengthTraining));
            runningAgilityButton.onClick.AddListener(() => ApplyTraining(TrainingMenu.RunningAgility));
            tacticsPassDrillButton.onClick.AddListener(() => ApplyTraining(TrainingMenu.TacticsPassDrill));
            goalkeeperTrainingButton.onClick.AddListener(() => ApplyTraining(TrainingMenu.GoalkeeperTraining));
        }

        private void OnEnable()
        {
            RefreshPlayerList();
        }

        private void RefreshPlayerList()
        {
            var team = GameManager.Instance.PlayerTeam;
            ActionRowView.Populate(
                playerListContainer,
                playerRowPrefab,
                team.ActiveRoster,
                p => $"{p.Name}（{p.Grade}年） スタミナ {p.Stats.CurrentStaminaPoints}/{p.Stats.MaxStaminaPoints}",
                "選択",
                SelectPlayer);
        }

        private void SelectPlayer(PlayerData player)
        {
            _selectedPlayer = player;
            selectedPlayerLabel.text = $"選択中: {player.Name}";
            resultLabel.text = string.Empty;
        }

        private void ApplyTraining(TrainingMenu menu)
        {
            if (_selectedPlayer == null)
            {
                resultLabel.text = "選手を選択してください";
                return;
            }

            var result = TrainingSystem.ApplyTraining(_selectedPlayer, menu);
            resultLabel.text = result.Success
                ? $"{_selectedPlayer.Name} の {result.GrownStatName} が上昇しました"
                : $"{_selectedPlayer.Name} はスタミナが足りません";

            RefreshPlayerList();
        }
    }
}
