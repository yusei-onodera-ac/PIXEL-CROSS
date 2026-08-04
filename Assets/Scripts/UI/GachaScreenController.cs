using System.Collections.Generic;
using System.Text;
using PixelCross.Core;
using PixelCross.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCross.UI
{
    public class GachaScreenController : MonoBehaviour
    {
        [SerializeField] private TMP_Text currencyLabel;
        [SerializeField] private TMP_Text resultLabel;
        [SerializeField] private Button pullSingleTicketButton;
        [SerializeField] private Button pullTenTicketButton;
        [SerializeField] private Button pullSinglePremiumButton;
        [SerializeField] private Button pullTenPremiumButton;

        private void OnEnable()
        {
            pullSingleTicketButton.onClick.AddListener(PullSingleWithTicket);
            pullTenTicketButton.onClick.AddListener(PullTenWithTicket);
            pullSinglePremiumButton.onClick.AddListener(PullSingleWithPremium);
            pullTenPremiumButton.onClick.AddListener(PullTenWithPremium);
            Refresh();
        }

        private void OnDisable()
        {
            pullSingleTicketButton.onClick.RemoveListener(PullSingleWithTicket);
            pullTenTicketButton.onClick.RemoveListener(PullTenWithTicket);
            pullSinglePremiumButton.onClick.RemoveListener(PullSingleWithPremium);
            pullTenPremiumButton.onClick.RemoveListener(PullTenWithPremium);
        }

        private void Refresh()
        {
            var team = GameManager.Instance.PlayerTeam;
            currencyLabel.text = $"ガチャチケット: {team.GachaTickets}枚 / 上位硬貨: {team.PremiumCurrency}";
        }

        private void PullSingleWithTicket()
        {
            var success = GameManager.Instance.TryGachaPullSingleWithTicket(out var item);
            resultLabel.text = success ? Describe(item) : "ガチャチケットが足りません";
            Refresh();
        }

        private void PullTenWithTicket()
        {
            var success = GameManager.Instance.TryGachaPullTenWithTicket(out var items);
            resultLabel.text = success ? DescribeAll(items) : "ガチャチケットが足りません";
            Refresh();
        }

        private void PullSingleWithPremium()
        {
            var success = GameManager.Instance.TryGachaPullSingleWithPremiumCurrency(out var item);
            resultLabel.text = success ? Describe(item) : "上位硬貨が足りません";
            Refresh();
        }

        private void PullTenWithPremium()
        {
            var success = GameManager.Instance.TryGachaPullTenWithPremiumCurrency(out var items);
            resultLabel.text = success ? DescribeAll(items) : "上位硬貨が足りません";
            Refresh();
        }

        private static string Describe(Item item) => $"獲得: {item.Name}";

        private static string DescribeAll(List<Item> items)
        {
            var sb = new StringBuilder("獲得:\n");
            foreach (var item in items)
            {
                sb.AppendLine($"・{item.Name}");
            }
            return sb.ToString();
        }
    }
}
