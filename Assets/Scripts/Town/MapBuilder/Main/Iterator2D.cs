public class Iterator2D
{
    public bool iterationIsOver { get; private set; }

    private int min_X;
    private int max_X;

    private int min_Y;
    private int max_Y;

    public int x { get; private set; }
    public int y { get; private set; }

    public Iterator2D(int min_X, int max_X, int min_Y, int max_Y)
    {
        Set(min_X, max_X, 
            min_Y, max_Y);
    }

    public void Set(int min_X, int max_X, int min_Y, int max_Y)
    {
        this.min_X = min_X;
        this.max_X = max_X;
        x = min_X;

        this.min_Y = min_Y;
        this.max_Y = max_Y;
        y = min_Y;

        iterationIsOver = false;
    }

    public void Iterate()
    {
        if (!iterationIsOver)
        {
            if (x < max_X)
            {
                x++;
            }
            else if (x >= max_X)
            {
                if (y < max_Y)
                {
                    x = min_X;
                    y++;
                }
                else if (y >= max_Y)
                {
                    iterationIsOver = true;
                }
            }
        }
    }
}