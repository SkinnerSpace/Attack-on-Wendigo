using UnityEngine;

public static class LimbSFXPlayerFactory
{
    public static LimbSFXPlayer Create(Transform transform, GoreSFXData goreSFXData, LimbSFXSets sFXSet)
    {
        switch (sFXSet)
        {
            case LimbSFXSets.FleshCrash:
                return new GoreSFXPlayer(transform, goreSFXData);

            case LimbSFXSets.BonesCrash:
                return new BoneSFXPlayer(transform, goreSFXData);
        }

        return null;
    }
}
