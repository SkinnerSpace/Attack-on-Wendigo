﻿using UnityEngine;

public class AudioVariety : AudioParam
{
    private int variants;

    private int current;
    private int previous;

    public AudioVariety(int variants)
    {
        this.variants = variants;
    }

    public override void ApplyTo(AudioEvent audioEvent)
    {
        Update();
        audioEvent.SetVariant(current);
    }

    private void Update()
    {
        current = (variants > 1) ? GetRandomValue() : 0;
        previous = current;
    }

    private int GetRandomValue()
    {
        int rand = Rand.Range(0, variants);

        if (previous == rand){
            rand = ((rand + 1) < variants) ? (rand + 1) : 0;
        }

        return rand;
    }
}