using UnityEngine;

public class FullSticker : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        if (holder == null)
            return;

        Vector3 position = holder.position + offset;
        transform.position = position;
    }
}
