public static class Easing
{
    public static float QuadEaseOut(this float t) => 1f - (1f - t).QuadEaseIn();
    public static float QuadEaseIn(this float t) => t * t;
    public static float QuadEaseInOut(this float t) => t.QuadEaseIn() * (3 - (2 * t));
}
