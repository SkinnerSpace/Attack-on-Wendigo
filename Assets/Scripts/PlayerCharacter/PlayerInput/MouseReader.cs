using UnityEngine;

public class MouseReader
{
    public  Vector2 mouse { get; private set; }
    public  bool shoot { get; private set; }
    public  bool aim { get; private set; }

    public void Read()
    {
        ReadMousePosition();
        ReadMouseClicks();
    }

    private void ReadMousePosition()
    {
        float xAxis = Input.GetAxis("Mouse X");
        float yAxis = Input.GetAxis("Mouse Y");

        mouse = new Vector2(xAxis, yAxis);
    }

    private void ReadMouseClicks()
    {
        shoot = Input.GetMouseButton(0);
        aim = Input.GetMouseButton(1);
    }
}
