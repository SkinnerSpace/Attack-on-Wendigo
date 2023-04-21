using System;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardTransform : MonoBehaviour
{
    private static int speedProperty = Shader.PropertyToID("Speed");
    private static int noiseTilingProperty = Shader.PropertyToID("NoiseTiling");
    private static int bigNoiseTilingProperty = Shader.PropertyToID("BigNoiseTiling");

    [Header("Required Components")]
    [SerializeField] private List<BlizzardRing> rings;
    [SerializeField] private MeshRenderer blizzardRenderer;

    [Header("Settings")]
    [Range(0f,1f)]
    [SerializeField] private float sizeMultiplier = 1f;
    [SerializeField] private float heightMultiplier = 4f;
    [SerializeField] private float changeSpeed = 0.5f;
    [SerializeField] private AnimationCurve sizeProgression;

    [Header("Shader Settings")]
    [SerializeField] private Vector4 defaultNoiseTiling;
    [SerializeField] private Vector4 defaultBigNoiseTiling;

    [SerializeField] private float maxSpeed;

    private float speed;

    private Material material;

    private float targetSize = 1f;
    private float size = 1f;

    public event Action onTargetSizeUpdate;

    public float Size => size * sizeMultiplier;
    public float Completeness => completeness;
    private float completeness;

    private void Awake(){
        material = blizzardRenderer.sharedMaterial;
    }

    private void Start(){
        if (GameEvents.current != null){
            GameEvents.current.onDeathProgressUpdate += SetTargetSize;
        }
    }

    private void Update()
    {
        size = Mathf.Lerp(size, targetSize, changeSpeed * Time.deltaTime);
        float height = ((1f - Size) * heightMultiplier) + 1f;

        Vector3 scale = new Vector3(Size, height, Size);
        foreach (BlizzardRing ring in rings)
        {
            ring.SetScale(scale);
        }

        completeness = Mathf.InverseLerp(sizeProgression[0].value, sizeProgression[1].value, Size);
        SetSpeed(completeness);

        SetNoiseTiling(height);
        SetBigNoiseTiling(height);
    }

    private void SetSpeed(float completeness)
    {
        speed = Mathf.Lerp(1, maxSpeed, completeness);
        material.SetFloat(speedProperty, speed);
    }

    private void SetNoiseTiling(float height){
        float horizontalNoiseTiling = defaultNoiseTiling.x / height;
        Vector4 noiseTiling = new Vector4(horizontalNoiseTiling, defaultNoiseTiling.y, 0f, 0f);
        material.SetVector(noiseTilingProperty, noiseTiling);
    }

    private void SetBigNoiseTiling(float height)
    {
        float horizontalBigNoiseTiling = defaultBigNoiseTiling.x / height;
        Vector4 bigNoiseTiling = new Vector4(horizontalBigNoiseTiling, defaultBigNoiseTiling.y, 0f, 0f);
        material.SetVector(bigNoiseTilingProperty, bigNoiseTiling);
    }

    private void SetTargetSize(float progress)
    {
        targetSize = sizeProgression.Evaluate(progress);
        onTargetSizeUpdate?.Invoke();
    }
}
