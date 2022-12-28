using UnityEngine;

public class Cartographer : MonoBehaviour
{
    [SerializeField] private Renderer canvas;
    private Texture2D texture;

    private int width;
    private int height;

    public void DrawMap(float[,] map)
    {
        if (texture == null)
        {
            width = map.GetLength(0);
            height = map.GetLength(1);
            texture = new Texture2D(width, height);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float altitude = map[x, y];
                Color color = new Color(altitude, altitude, altitude);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        canvas.material.mainTexture = texture;
    }
}
