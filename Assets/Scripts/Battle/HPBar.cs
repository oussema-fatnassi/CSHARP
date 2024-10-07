using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetHealth(float healthNormalized)
    {
        health.transform.localScale = new Vector3(healthNormalized, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
