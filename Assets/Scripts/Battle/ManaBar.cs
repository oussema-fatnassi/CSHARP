using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is responsible for setting the mana bar in the battle dialog box.
*/

public class ManaBar : MonoBehaviour
{
    [SerializeField] GameObject mana;

    // Set the mana bar
    public void SetMana(float manaNormalized)
    {
        mana.transform.localScale = new Vector3(manaNormalized, 1f);
    }

}
