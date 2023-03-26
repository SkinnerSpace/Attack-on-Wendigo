using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LimbSkin
{
    [SerializeField] private List<SkinnedMeshRenderer> flesh;
    [SerializeField] private List<SkinnedMeshRenderer> bones;
    [SerializeField] private List<MeshRenderer> hair;

    public void ShowFlesh(){
        EnableRendering(flesh, true);
        EnableRendering(bones, false);
        EnableRendering(hair, true);
    }

    public void ShowBones(){
        EnableRendering(flesh, false);
        EnableRendering(bones, true);
        EnableRendering(hair, false);
    }

    public void Hide(){
        EnableRendering(flesh, false);
        EnableRendering(bones, false);
        EnableRendering(hair, false);
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
