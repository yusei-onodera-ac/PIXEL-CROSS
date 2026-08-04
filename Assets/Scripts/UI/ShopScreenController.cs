using System.Linq;
using PixelCross.Core;
using PixelCross.Inventory;
using TMPro;
using UnityEngine;

namespace PixelCross.UI
{
    public class ShopScreenController : MonoBehaviour
    {
        [SerializeField] private Transform listContainer;
        [SerializeField] private ActionRowView rowPrefab;
        [SerializeField] private TMP_Text currencyLabel;
        [SerializeField] private TMP_Text resultLabel;

        private void OnEnable()
        {
            Refresh();
        }

        private void Refresh()
        {
            var team = GameManager.Instance.PlayerTeam;
            currencyLabel.text = $"基本硬貨: {team.BasicCurrency}";

            var catalog = ItemShopSystem.GetCatalog();
            ActionRowView.Populate(
                listContainer,
                rowPrefab,
                Enumerable.Range(0, catalog.Count),
                i => $"{catalog[i].Name}（{catalog[i].Price}）",
                "購入",
                Purchase,
                i => team.BasicCurrency >= catalog[i].Price);
        }

        private void Purchase(int catalogIndex)
        {
            var success = GameManager.Instance.TryPurchaseShopItem(catalogIndex, out var item);
            resultLabel.text = success ? $"{item.Name} を購入しました" : "基本硬貨が足りません";
            Refresh();
        }
    }
}
