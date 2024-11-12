using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is responsible for setting the health bar in the battle scene.
*/

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    // Set the health bar
    public void SetHealth(float healthNormalized)
    {
        health.transform.localScale = new Vector3(healthNormalized, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
