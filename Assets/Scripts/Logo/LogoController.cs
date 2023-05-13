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

    private void Update()
    {
        Debug.Log("Save me ");
    }

    public void ResetState() => resetCommand.Execute();

    private IEnumerator PlayCoroutine()
    {
        for (int i = 0; i < logoCommands.Length; i++)
        {
            logoCommands[i].Execute();

            while (!logoCommands[i].IsDone)
            {
                Debug.Log("I'm waiting you fucking dummy");
                yield return null;
            }
        }

        Debug.Log("Is over");
    }
}
