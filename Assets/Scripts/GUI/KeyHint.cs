using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class KeyHint : IHint
{
    public string text { get; set; }
    public KeyCode key { get; set; }
    public Color color { get; set; }

    public KeyHint(string text, KeyCode key, Color color)
    {
        string keyFormatted = FormatKeyToString(key);
        this.text = string.Format(text, keyFormatted);
        this.color = color;
    }

    private string FormatKeyToString(KeyCode key)
    {
        string[] pieces = SplitCamelCase(key.ToString());

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < pieces.Length; i++)
        {
            stringBuilder.Append(pieces[i]);

            if (i < pieces.Length - 1)
            {
                stringBuilder.Append(" ");
            }
        }

        string keyFormatted = stringBuilder.ToString().ToUpper();
        return keyFormatted;
    }

    string[] SplitCamelCase(string source)
    {
        return Regex.Split(source, @"(?<!^)(?=[A-Z])");
    }
}