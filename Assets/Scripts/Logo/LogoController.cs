using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class LogoController : MonoBehaviour
{
    [Header("Required components")]
    [SerializeField] private LogoCommand resetCommand;
    [SerializeField] private LogoCommand[] logoCommands;

    public void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    public void ResetState() => resetCommand.Execute();

    private IEnumerator PlayCoroutine()
    {
        for (int i = 0; i < logoCommands.Length; i++)
        {
            logoCommands[i].Execute();

            while (!logoCommands[i].IsDone)
            {
                yield return null;
            }
        }

        Debug.Log("Is over");
    }
}
