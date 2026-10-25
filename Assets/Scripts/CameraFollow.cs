
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -7f);

    void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
