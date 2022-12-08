using UnityEngine;

public class BuildingsRenderer : IFractalVisitor
{
    public void GatherFractalData(FractalData data)
    {
        float addHeight = 0f;
        if (data.depth == 0)
        {
            addHeight = 4f;
        }

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = data.position + new Vector3(0f, addHeight, 0f);
        plane.transform.localScale = data.size;
    }
}
