using UnityEngine;

public class Hint : IHint
{
    public string text { get; set; }
    public Color color { get; set; }

    private bool isShown;

    public Hint(string text, Color color)
    {
        this.text = text;
        this.color = color;
    }
}
