using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LimbSkin
{
    [SerializeField] private List<SkinnedMeshRenderer> flesh;
    [SerializeField] private List<SkinnedMeshRenderer> muscles;
    [SerializeField] private List<SkinnedMeshRenderer> bones;
    [SerializeField] private List<MeshRenderer> hair;

    [SerializeField] private bool hideGoreOnStart;

    [SerializeField] private LimbSkinAlternative alternative;

    public void ShowFlesh(){
        EnableRendering(flesh, true);

        if (hideGoreOnStart){
            EnableRendering(muscles, false);

            EnableRendering(bones, false);
            EnableRendering(alternative.bones, false);
        }
    }

    public void ShowBones(){
        EnableRendering(flesh, false);
        EnableRendering(muscles, true);

        EnableRendering(bones, true);
        EnableRendering(alternative.bones, true);
    }

    public void Hide(){
        EnableRendering(flesh, false);
        EnableRendering(muscles, false);

        EnableRendering(bones, false);
        EnableRendering(alternative.bones, false);
    }

    public void GrowHair() => EnableRendering(hair, true);
    public void GoBald() => EnableRendering(hair, false);

    public void ExposeGoreButKeepTheFleshUntouched(){
        EnableRendering(muscles, true);

        EnableRendering(bones, true);
        EnableRendering(alternative.bones, true);
    }

    private void EnableRendering(List<SkinnedMeshRenderer> meshes, bool isEnabled){
        foreach (SkinnedMeshRenderer mesh in meshes)
            mesh.enabled = isEnabled;
    }

    private void EnableRendering(List<MeshRenderer> meshes, bool isEnabled)
    {
        foreach (MeshRenderer mesh in meshes)
            mesh.enabled = isEnabled;
    }
}
