using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in range, autodesctructing in 3 2 1...");
            StartCoroutine(DelayAndDeactivate(3f));
        }
    }

    private IEnumerator DelayAndDeactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
