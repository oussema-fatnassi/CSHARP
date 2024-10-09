using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
