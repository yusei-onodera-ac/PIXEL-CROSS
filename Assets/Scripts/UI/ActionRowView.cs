using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCross.UI
{
    // Generic runtime-instantiated list row (a label + one action button),
    // reused across roster/inventory/shop/gacha-result lists instead of a
    // separate view class per data type.
    public class ActionRowView : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private Button actionButton;
        [SerializeField] private TMP_Text actionButtonLabel;

        public void Bind(string labelText, string buttonText, Action onAction, bool interactable = true)
        {
            label.text = labelText;
            if (actionButtonLabel != null) actionButtonLabel.text = buttonText;

            actionButton.onClick.RemoveAllListeners();
            if (onAction != null)
            {
                actionButton.onClick.AddListener(() => onAction());
            }
            actionButton.interactable = interactable && onAction != null;
        }

        public static void Populate<T>(
            Transform container,
            ActionRowView rowPrefab,
            IEnumerable<T> items,
            Func<T, string> labelSelector,
            string buttonText,
            Action<T> onAction,
            Func<T, bool> interactableSelector = null)
        {
            for (var i = container.childCount - 1; i >= 0; i--)
            {
                Destroy(container.GetChild(i).gameObject);
            }

            foreach (var item in items)
            {
                var row = Instantiate(rowPrefab, container);
                var interactable = interactableSelector == null || interactableSelector(item);
                row.Bind(labelSelector(item), buttonText, onAction == null ? null : () => onAction(item), interactable);
            }
        }
    }
}
