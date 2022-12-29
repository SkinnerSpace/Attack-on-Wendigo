public class GeoPoint
{
    public float altitude { get; private set; }
    public int priority { get; private set; }
    public bool immutable { get; private set; }

    public GeoPoint() { }

    public GeoPoint(float altitude, int priority, bool immutable)
    {
        this.altitude = altitude;
        this.priority = priority;
        this.immutable = immutable;
    }

    public void SetAltitude(float altitude)
    {
        this.altitude = altitude;
    }
}