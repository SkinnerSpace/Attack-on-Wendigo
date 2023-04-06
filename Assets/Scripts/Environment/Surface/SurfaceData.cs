using System;

[Serializable]
public class SurfaceData
{
    public string Name => (secondName != "") ? secondName : name;
    public string name;
    public string secondName;
    public SurfaceHitSFXData sfx;
}