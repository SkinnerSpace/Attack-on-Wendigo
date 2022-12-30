
static class ControllerFactory
{
    public static IController Create(Controllers controller)
    {
        switch (controller)
        {
            case Controllers.Player:
                return new PlayerController();

            default:
                return null;
        }
    }
}
