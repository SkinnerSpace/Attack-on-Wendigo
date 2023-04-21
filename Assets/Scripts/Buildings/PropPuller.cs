using UnityEngine;

public class PropPuller : MonoBehaviour, ISwitchable
{
    private bool isActive;

    private void Update()
    {
        if (isActive)
        {
            transform.position += new Vector3(0f, 10f, 0f) * Time.deltaTime;
        }
    }

    public void SwitchOn()
    {
        isActive = true;
        enabled = true;

        Debug.Log("PULL AWAY");
    }

    public void SwitchOff()
    {
        isActive = false;
        enabled = false;
    }
}
