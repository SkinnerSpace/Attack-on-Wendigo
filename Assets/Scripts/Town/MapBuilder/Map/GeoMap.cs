public class GeoMap
{
    private GeoPoint[,] geoPoints;
    public int width => geoPoints != null ? geoPoints.GetLength(0) : 0;
    public int height => geoPoints != null ? geoPoints.GetLength(1) : 0;

    public GeoMap(int width, int height)
    {
        geoPoints = new GeoPoint[width, height];

        for (int y=0; y < height; y++)
        {
            for (int x=0; x < width; x++)
            {
                geoPoints[x, y] = new GeoPoint();
            }
        }
    }

    public void SetAltitude(Cell cell, float altitude, int priority, bool immutable)
    {
        geoPoints[cell.X, cell.Y] = new GeoPoint(altitude, priority, immutable);
    }

    public void SetAltitude(Cell cell, float altitude)
    {
        geoPoints[cell.X, cell.Y].SetAltitude(altitude);
    }

    public float GetAltitude(int x, int y)
    {
        return geoPoints[x, y].altitude;
    }
}