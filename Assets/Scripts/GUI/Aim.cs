using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private Image point;
    [SerializeField] private List<Image> sprites;

    private void Awake(){
        SetIdleMode();
    }

    public void SetColor(Color color)
    {
        point.color = color;

        foreach (Image sprite in sprites){
            sprite.color = color;
        }
    }

    public void SetCombatMode()
    {
        point.enabled = false;

        foreach (Image sprite in sprites){
            sprite.enabled = true;
        }
    }

    public void SetIdleMode()
    {
        point.enabled = true;

        foreach (Image sprite in sprites){
            sprite.enabled = false;
        }
    }
}
