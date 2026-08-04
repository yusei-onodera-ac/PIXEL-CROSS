using PixelCross.Core;
using UnityEngine;

namespace PixelCross.UI
{
    public class RosterScreenController : MonoBehaviour
    {
        [SerializeField] private Transform listContainer;
        [SerializeField] private ActionRowView rowPrefab;

        private void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            var team = GameManager.Instance.PlayerTeam;
            ActionRowView.Populate(
                listContainer,
                rowPrefab,
                team.ActiveRoster,
                p => $"{p.Name}（{p.Grade}年） B{p.Stats.BodyRank} S{p.Stats.SpeedRank} St{p.Stats.StaminaDisplayRank} T{p.Stats.TechniqueRank} K{p.Stats.KeeperRank}",
                "詳細",
                null);
        }
    }
}
