using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is responsible for warping the player to a different location in the game.
    It also changes the camera bounds and orthographic size when the player warps.
*/

public class Warp : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Collider2D newCameraBounds; 
    [SerializeField] private int orthoSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer == (playerLayer | (1 << collision.gameObject.layer)))
        {
            collision.transform.position = destination.position;

            CameraManager.Instance.SetCameraBounds(newCameraBounds);
            CameraManager.Instance.SetOrthoSize(orthoSize);
        }
    }
}
