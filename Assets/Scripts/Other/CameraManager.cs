using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineConfiner confiner;
    private int orthoSize;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        confiner = virtualCamera.GetComponent<CinemachineConfiner>();
        orthoSize = (int)virtualCamera.m_Lens.OrthographicSize;
    }

    public void SetCameraBounds(Collider2D bounds)
    {
        if (confiner != null)
        {
            confiner.m_BoundingShape2D = bounds;
            confiner.InvalidatePathCache(); 
        }
    }

    public void SetOrthoSize(int size)
    {
        virtualCamera.m_Lens.OrthographicSize = size;
    }

}
