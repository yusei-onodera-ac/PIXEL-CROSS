using System;
using System.Linq;
using PixelCross.Core;
using PixelCross.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCross.UI
{
    public class TacticsScreenController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown offenseDropdown;
        [SerializeField] private TMP_Dropdown defenseDropdown;
        [SerializeField] private Toggle autoPlayToggle;

        private void Start()
        {
            var tactics = GameManager.Instance.PlayerTeam.Tactics;

            offenseDropdown.ClearOptions();
            offenseDropdown.AddOptions(Enum.GetNames(typeof(OffenseStyle)).ToList());
            offenseDropdown.SetValueWithoutNotify((int)tactics.Offense);
            offenseDropdown.onValueChanged.AddListener(i => tactics.Offense = (OffenseStyle)i);

            defenseDropdown.ClearOptions();
            defenseDropdown.AddOptions(Enum.GetNames(typeof(DefenseStyle)).ToList());
            defenseDropdown.SetValueWithoutNotify((int)tactics.Defense);
            defenseDropdown.onValueChanged.AddListener(i => tactics.Defense = (DefenseStyle)i);

            autoPlayToggle.SetIsOnWithoutNotify(tactics.AutoPlay);
            autoPlayToggle.onValueChanged.AddListener(v => tactics.AutoPlay = v);
        }
    }
}
