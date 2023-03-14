using UnityEngine;
using System.Collections.Generic;

public class SkeletonRestructurer : MonoBehaviour
{
    [SerializeField] private Transform skeleton;
    [SerializeField] private Transform hips;
    [SerializeField] private Transform backBone;
    [SerializeField] private Transform armsRoot;
    [SerializeField] private Transform neckPeak;
    [SerializeField] private Transform neckTarget;
    [SerializeField] private Transform leftShoulder;
    [SerializeField] private Transform rightShoulder;

    private Transform world;

    public bool isDone { get; private set; } 

    private void Awake()
    {
        world = transform.parent.parent;
    }

    public void Restructure()
    {
        isDone = true;

        hips.SetParent(skeleton);
        armsRoot.SetParent(skeleton);
        neckTarget.SetParent(neckPeak);
        transform.SetParent(world);
        leftShoulder.SetParent(backBone);
        rightShoulder.SetParent(backBone);
    }
}
