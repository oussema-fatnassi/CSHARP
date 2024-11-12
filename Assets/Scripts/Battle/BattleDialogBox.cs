using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    This class is responsible for setting the dialog text in the battle dialog box.
*/

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] private Text dialogText;

    // Set the dialog text
    public void SetDialog(string dialog) {
        dialogText.text = dialog;
    }
}
