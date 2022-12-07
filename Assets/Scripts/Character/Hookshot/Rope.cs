using UnityEngine;

public class Rope : MonoBehaviour
{
    public float length { get; private set; }
    private bool active = true;

    public void SetActive(bool active)
    {
        this.active = active;
    }

    public void LookAt(Vector3 position)
    {
        transform.LookAt(position);
    }

    public void Lengthen(float units)
    {
        length += units * Time.deltaTime;
        transform.localScale = new Vector3(1, 1, length);
    }

    public void Shorten(float units)
    {
        length -= units * Time.deltaTime;
        length = Mathf.Max(length, 0f);
        transform.localScale = new Vector3(1, 1, length);
    }

    public void SetLength(float length)
    {
        this.length = length;
    }
}
