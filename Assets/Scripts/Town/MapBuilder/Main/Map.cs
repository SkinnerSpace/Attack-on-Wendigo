using UnityEngine;

public class Map
{
    private const int WIDTH = 0;
    private const int HEIGHT = 1;

    public int size;
    private Mark[,] marks;
    public MapExplication explication { get; private set; }

    public Cell Center => new Cell(size / 2, size / 2);

    public Map(MapExplication explication)
    {
        size = explication.size;
        this.explication = explication;

        marks = new Mark[size, size];
    }

    public void SetMark(int x, int y, Mark mark){
        marks[x, y] = mark;
    }

    public void SetMark(Mark mark) => marks[mark.mapPosition.X, mark.mapPosition.Y] = mark;

    public void SetMark(Cell cell, Mark mark)
    {
        marks[cell.X, cell.Y] = mark;
    }

    public Mark GetMark(int x, int y) => marks[x, y];
    public Mark GetMark(Cell cell) => marks[cell.X, cell.Y];

    public bool IsEmpty(int x, int y) => marks[x, y].IsEmpty();
    public bool IsEmpty(Cell cell) => marks[cell.X, cell.Y].IsEmpty();

    public int GetWidth()
    {
        return marks.GetLength(WIDTH);
    }

    public int GetHeight()
    {
        return marks.GetLength(HEIGHT);
    }
}