using System.Linq;
using PixelCross.Core;
using PixelCross.Data;
using TMPro;
using UnityEngine;

namespace PixelCross.UI
{
    public class InventoryScreenController : MonoBehaviour
    {
        [SerializeField] private Transform playerListContainer;
        [SerializeField] private ActionRowView playerRowPrefab;
        [SerializeField] private Transform itemListContainer;
        [SerializeField] private ActionRowView itemRowPrefab;
        [SerializeField] private TMP_Text selectedPlayerLabel;
        [SerializeField] private TMP_Text resultLabel;

        private PlayerData _selectedPlayer;

        private void OnEnable()
        {
            RefreshPlayerList();
            RefreshItemList();
        }

        private void RefreshPlayerList()
        {
            var team = GameManager.Instance.PlayerTeam;
            ActionRowView.Populate(
                playerListContainer,
                playerRowPrefab,
                team.ActiveRoster,
                p => p.Name,
                "選択",
                SelectPlayer);
        }

        private void SelectPlayer(PlayerData player)
        {
            _selectedPlayer = player;
            selectedPlayerLabel.text = $"使用対象: {player.Name}";
            RefreshItemList();
        }

        private void RefreshItemList()
        {
            var team = GameManager.Instance.PlayerTeam;
            ActionRowView.Populate(
                itemListContainer,
                itemRowPrefab,
                Enumerable.Range(0, team.Inventory.Count),
                i => team.Inventory[i].Name,
                "使う",
                UseItem,
                _ => _selectedPlayer != null);
        }

        private void UseItem(int inventoryIndex)
        {
            if (_selectedPlayer == null)
            {
                resultLabel.text = "対象の選手を選択してください";
                return;
            }

            var success = GameManager.Instance.UseInventoryItem(_selectedPlayer, inventoryIndex);
            resultLabel.text = success ? "アイテムを使用しました" : "使用できませんでした";
            RefreshItemList();
        }
    }
}
