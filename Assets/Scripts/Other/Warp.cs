using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Collider2D newCameraBounds; // Set this to the new bounds for this warp area

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer == (playerLayer | (1 << collision.gameObject.layer)))
        {
            // Warp the player
            collision.transform.position = destination.position;

            // Update the camera bounds
            CameraManager.Instance.SetCameraBounds(newCameraBounds);
        }
    }
}
