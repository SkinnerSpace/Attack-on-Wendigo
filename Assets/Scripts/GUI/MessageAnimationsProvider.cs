using UnityEngine;

public static class MessageAnimationsProvider
{
    private static int appear = Animator.StringToHash("Appear");

    private static int flickOut = Animator.StringToHash("FlickOut");
    private static int fadeOut = Animator.StringToHash("FadeOut");

    public static int Get(MessageAppearAnimations animation)
    {
        switch (animation)
        {
            case MessageAppearAnimations.Appear:
                return appear;
        }

        return 0;
    }

    public static int Get(MessageDisappearAnimations animation)
    {
        switch (animation)
        {
            case MessageDisappearAnimations.FlickOut:
                return flickOut;

            case MessageDisappearAnimations.FadeOut:
                return fadeOut;
        }

        return 0;
    }
}
