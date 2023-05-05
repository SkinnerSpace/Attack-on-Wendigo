using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    private const string DASH_HINT = "Dash";
    private const string SECOND_JUMP_HINT = "SecondJump";

    [Header("Required Components")]
    [SerializeField] private HintMessage hintMessage;
    [SerializeField] private FunctionTimer timer;

    [Header("Colors")]
    [SerializeField] private Color redColor;

    private static Dictionary<string, IHint> dodgeHints;
    private IHint chopperHint;

    private bool isLocked;
    private bool isPermanentlyLocked;

    private void Start()
    {
        ConnectEvents();
        SetUpHints();
    }

    private void ConnectEvents()
    {
        PlayerEvents.current.onSecondJump += () => dodgeHints.Remove(SECOND_JUMP_HINT);
        PlayerEvents.current.onDash += () => dodgeHints.Remove(DASH_HINT);
        PlayerEvents.current.onDeath += () => isPermanentlyLocked = true;

        PlayerEvents.current.onDamage += () => timer.Set("Dodge", 1f, ShowDodgeHint);
        HelicopterEvents.current.onIsGoingToLand += ShowGetToTheChopperHint;
    }

    public void SetUpHints()
    {
        dodgeHints = new Dictionary<string, IHint>()
        {
            {DASH_HINT, new KeyHint("PRESS \"{0}\" TO DASH", KeyBinds.Instance.Dash, redColor)},
            {SECOND_JUMP_HINT, new Hint("TRY TO PERFORM\nSECOND JUMP", redColor)}
        };

        chopperHint = new Hint("GET TO THE CHOPPER", Color.green);
    }

    private void ShowDodgeHint()
    {
        if (!isLocked && isPermanentlyLocked && dodgeHints.Count > 0)
        {
            isLocked = true;

            int index = Rand.Range(0, dodgeHints.Count);
            string randomKey = dodgeHints.Keys.ElementAt(index);
            IHint randomHint = dodgeHints[randomKey];
            hintMessage.Show(randomHint);

            dodgeHints.Remove(randomKey);

            timer.Set("Unlock", 6f, () => isLocked = false);
        }
    }

    private void ShowGetToTheChopperHint()
    {
        timer.Set("Chopper", 2f, () => hintMessage.Show(chopperHint));
    }
}
