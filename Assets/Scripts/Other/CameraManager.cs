using UnityEngine;
using Cinemachine;

/*
    This class is responsible for managing the camera in the game.
    It handles setting the camera bounds and orthographic size.
*/

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineConfiner confiner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        confiner = virtualCamera.GetComponent<CinemachineConfiner>();
    }
    // Set the camera bounds for the virtual camera
    public void SetCameraBounds(Collider2D bounds)
    {
        if (confiner != null)
        {
            confiner.m_BoundingShape2D = bounds;
            confiner.InvalidatePathCache(); 
        }
    }
    // Set the orthographic size of the virtual camera
    public void SetOrthoSize(int size)
    {
        virtualCamera.m_Lens.OrthographicSize = size;
    }

}
