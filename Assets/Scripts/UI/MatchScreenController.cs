using PixelCross.Core;
using PixelCross.Match;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCross.UI
{
    public class MatchScreenController : MonoBehaviour
    {
        [SerializeField] private TMP_Text weekLabel;
        [SerializeField] private TMP_Text matchInfoLabel;
        [SerializeField] private TMP_Text resultLabel;
        [SerializeField] private Button actionButton;
        [SerializeField] private TMP_Text actionButtonLabel;

        private void OnEnable()
        {
            actionButton.onClick.AddListener(OnActionPressed);
            Refresh();
        }

        private void OnDisable()
        {
            actionButton.onClick.RemoveListener(OnActionPressed);
        }

        private void Refresh()
        {
            var gm = GameManager.Instance;
            weekLabel.text = $"{gm.Turns.CurrentYear}年目 第{gm.Turns.CurrentWeek}週（{gm.Turns.CurrentPhase}）";

            var match = gm.GetMatchThisWeek();
            if (match == null)
            {
                matchInfoLabel.text = "今週の試合はありません";
                actionButtonLabel.text = "次の週に進む";
            }
            else if (match.IsIntercollegiate)
            {
                matchInfoLabel.text = "インカレ（全国大会）本番";
                actionButtonLabel.text = "試合開始";
            }
            else
            {
                matchInfoLabel.text = $"対戦相手: {match.Opponent.SchoolName}（強さ {match.Opponent.Strength}）";
                actionButtonLabel.text = "試合開始";
            }
        }

        private void OnActionPressed()
        {
            var gm = GameManager.Instance;
            var match = gm.GetMatchThisWeek();

            if (match == null)
            {
                gm.AdvanceWeek();
                resultLabel.text = string.Empty;
            }
            else
            {
                var outcome = gm.PlayScheduledMatch();
                resultLabel.text = DescribeOutcome(outcome);
            }

            Refresh();
        }

        private static string DescribeOutcome(MatchOutcome outcome)
        {
            if (outcome.IsIntercollegiate)
            {
                var t = outcome.TournamentResult;
                return t.PlayerIsChampion
                    ? "全国優勝！"
                    : $"インカレ敗退（{t.RoundsWon}回戦突破、優勝校: {t.ChampionName}）";
            }

            var r = outcome.Result;
            if (r.HomeWon) return $"勝利！ {r.HomeScore} - {r.AwayScore}";
            if (r.IsDraw) return $"引き分け {r.HomeScore} - {r.AwayScore}";
            return $"敗北... {r.HomeScore} - {r.AwayScore}";
        }
    }
}
