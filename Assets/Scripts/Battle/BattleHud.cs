using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{

    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] ManaBar manaBar;
    // [SerializeField] BattleDialogBox dialogBox;

    public void SetData(Player player)
    {
        nameText.text = player.PlayerName;
        levelText.text = "Lvl " + player.Level;
        hpBar.SetHealth((float)player.Health / player.MaxHealth);
        manaBar.SetMana((float)player.Mana / player.MaxMana);

        // dialogBox.SetDialog(player.PlayerName + " is ready for battle!");
    }

}
