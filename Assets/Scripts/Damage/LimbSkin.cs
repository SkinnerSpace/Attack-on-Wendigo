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

    public void ShowFlesh(){
        EnableRendering(flesh, true);

        if (hideGoreOnStart){
            EnableRendering(muscles, false);
            EnableRendering(bones, false);
        }
    }

    public void ShowBones(){
        EnableRendering(flesh, false);
        EnableRendering(muscles, true);
        EnableRendering(bones, true);
    }

    public void Hide(){
        EnableRendering(flesh, false);
        EnableRendering(muscles, false);
        EnableRendering(bones, false);
    }

    public void GoBald() => EnableRendering(hair, false);

    public void ExposeGoreButKeepTheFleshUntouched(){
        EnableRendering(muscles, true);
        EnableRendering(bones, true);
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
