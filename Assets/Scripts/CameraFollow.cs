using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Le joueur

    public float smoothing = 5f;

    private Vector3 offset;

    private void Start()
    {
        // Calculer l'offset initial
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Déplacer la caméra en suivant la position du joueur avec un effet de lissage
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            offset = transform.position - target.position; // Recalculer l'offset
        }
    }
}
