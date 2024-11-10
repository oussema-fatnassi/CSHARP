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

    public void SetData(PlayerStats player)
    {
        nameText.text = player.playerName;
        levelText.text = "Lvl " + player.level;
        hpBar.SetHealth((float)player.health / player.maxHealth);
        manaBar.SetMana((float)player.mana / player.maxMana);

        // dialogBox.SetDialog(player.PlayerName + " is ready for battle!");
    }

}
