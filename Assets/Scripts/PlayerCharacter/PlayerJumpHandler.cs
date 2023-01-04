using UnityEngine;

public class PlayerJumpHandler : MonoBehaviour
{
    private const int MAX_JUMP_COUNT = 2;

    private PlayerCharacter player;

    private bool hasJumped;
    private int jumpCount;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
    }

    public void ResetState()
    {
        hasJumped = false;
        jumpCount = 0;
    }

    public void PerformAirJump(IJumper jumper)
    {
        if (!hasJumped && InputReader.jump)
        {
            if (jumpCount < MAX_JUMP_COUNT) 
                Jump(jumper);
        }
        else if (hasJumped && !InputReader.jump)
        {
            hasJumped = false;
        }
    }

    public void PerformJump(IJumper jumper)
    {
        if (InputReader.jump) Jump(jumper);
    }

    private void Jump(IJumper jumper)
    {
        hasJumped = true;

        jumpCount += 1;
        float jumpVelocity = GetJumpVelocity() * jumpCount;
        jumper.SetVelocity(jumpVelocity);
    }

    private float GetJumpVelocity()
    {
        return Mathf.Sqrt(player.JumpHeight * 2f * player.Gravity);
    }
}
