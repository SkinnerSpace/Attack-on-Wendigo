public class Map
{
    private const int WIDTH = 0;
    private const int HEIGHT = 1;

    private Mark[,] marks;

    public Map(int size)
    {
        marks = new Mark[size, size];
    }

    public void SetMark(int x, int y, Mark mark){
        marks[x, y] = mark;
    }

    public Mark GetMark(int x, int y) => marks[x, y];

    public bool IsEmpty(int x, int y) => marks[x, y].IsEmpty();

    public int GetWidth()
    {
        return marks.GetLength(WIDTH);
    }

    public int GetHeight()
    {
        return marks.GetLength(HEIGHT);
    }
}