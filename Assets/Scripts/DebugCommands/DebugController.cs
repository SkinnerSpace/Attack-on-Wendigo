using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    private const KeyCode TOGGLE_KEY = KeyCode.BackQuote;
    private const string TOGGLE_COMMAND = "`";
    private const string EXECUTE_COMMAND = "!";
    private const string CONSOLE = "Console";

    [SerializeField] private DebugCommands commands;
    [SerializeField] private float windowWidth = 500f;
    [SerializeField] private float helpHeight = 100f;

    private bool showConsole;
    public bool showHelp { get; set; }
    private string input = "";

    private Vector2 scroll;
    float verticalPosition = 0f;
    float horizontalPosition = 0f;

    private void Update()
    {
        Toggle();
        OnReturn();
    }

    private void OnGUI()
    {
        if (!showConsole) return;

        verticalPosition = 0f;
        horizontalPosition = Screen.width - windowWidth;

        if (showHelp){
            ShowHelp();
        }

        ShowCommandLine();
    }

    private void ShowHelp()
    {
        GUI.Box(new Rect(horizontalPosition, verticalPosition, windowWidth, helpHeight), "");

        Rect viewport = new Rect(0, 0, windowWidth - 30, 20 * commands.Count);

        scroll = GUI.BeginScrollView(new Rect(horizontalPosition, verticalPosition + 5f, windowWidth, 90), scroll, viewport);

        for (int i = 0; i < commands.Count; i++)
        {
            DebugCommandBase command = commands.Get(i) as DebugCommandBase;
            string label = $"{command.CommandFormat} - {command.CommandDescription}";
            Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

            GUI.Label(labelRect, label);
        }

        GUI.EndScrollView();

        verticalPosition += helpHeight;
    }

    private void ShowCommandLine()
    {
        GUI.Box(new Rect(horizontalPosition, verticalPosition, windowWidth, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        GUI.FocusControl(CONSOLE);
        GUI.SetNextControlName(CONSOLE);
        input = GUI.TextField(new Rect(horizontalPosition + 10f, verticalPosition + 5f, windowWidth - 20f, 20f), input);
    }

    private void Toggle()
    {
        if (Input.GetKeyDown(TOGGLE_KEY) || (input != null && input.Contains(TOGGLE_COMMAND))){

            if (!showConsole){
                showConsole = true;
            }
            else if (showConsole){
                showConsole = false;
                GUI.UnfocusWindow();
                input = null;
            }
        }
    }

    private void OnReturn()
    {
        if (input != null && input != "" && input.Contains(EXECUTE_COMMAND)){
            if (showConsole)
            {
                HandleInput();
                input = "";
            }
        }
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ', '!');

        for (int i = 0; i < commands.Count; i++)
        {
            DebugCommandBase commandBase = commands.Get(i) as DebugCommandBase;

            if (input.Contains(commandBase.CommandId))
            {
                if (commands.Get(i) as DebugCommand != null){
                    (commands.Get(i) as DebugCommand).Invoke();
                }
                else if (commands.Get(i) as DebugCommand<int> != null){
                    (commands.Get(i) as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
                else if (commands.Get(i) as DebugCommand<float> != null){
                    (commands.Get(i) as DebugCommand<float>).Invoke(float.Parse(properties[1]));
                }
                else if (commands.Get(i) as DebugCommand<int, int> != null){
                    int firstValue = int.Parse(properties[1]);
                    int secondValue = int.Parse(properties[2]);

                    (commands.Get(i) as DebugCommand<int, int>).Invoke(firstValue, secondValue);
                }
            }
        }

        input = "";
    }
}
