using System.Collections.Generic;

public class AreasRenderer
{
    public AreasRenderer(List<FractalArea> areas, RawAreaData rawData)
    {
        foreach (FractalArea area in areas)
            PrepareForRender(area, rawData);
    }

    private void PrepareForRender(FractalArea area, RawAreaData rawData)
    {
        float lengthAdjustment = rawData.width / rawData.length;
        float widthAdjustment = rawData.length / rawData.width;

        float areaLength = (rawData.length > rawData.width) ? (rawData.size * lengthAdjustment) : (rawData.size);
        float areaWidth = (rawData.length < rawData.width) ? (rawData.size * widthAdjustment) : (rawData.size);

        //Debug.Log("Area length " + areaLength);
        //Debug.Log("Area width " + areaWidth);

        area.Render(areaLength, areaWidth, 0.9f);
    }
}
