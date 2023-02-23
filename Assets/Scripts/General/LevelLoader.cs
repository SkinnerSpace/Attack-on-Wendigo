using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void ReloadScene()
    {
        FMOD.ChannelGroup mcg;
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
        mcg.stop();

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
