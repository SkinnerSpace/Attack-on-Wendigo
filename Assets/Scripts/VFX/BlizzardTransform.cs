using System;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardTransform : MonoBehaviour
{
    private const float DEFAULT_SIZE = 1f;

    private static int noiseTilingProperty = Shader.PropertyToID("NoiseTiling");
    private static int bigNoiseTilingProperty = Shader.PropertyToID("BigNoiseTiling");

    [Header("Required Components")]
    [SerializeField] private List<BlizzardRing> rings;
    [SerializeField] private MeshRenderer blizzardRenderer;

    [Header("Settings")]
    [Range(0f,1f)]
    [SerializeField] private float sizeMultiplier = 1f;
    [SerializeField] private float heightMultiplier = 4f;
    [SerializeField] private float timeToChangeMultiplier = 5f;
    [SerializeField] private AnimationCurve sizeProgression;

    [Header("Shader Settings")]
    [SerializeField] private Vector4 defaultNoiseTiling;
    [SerializeField] private Vector4 defaultBigNoiseTiling;

    private Material material;

    private float sizeBeforeChange;
    private float targetSize = 1f;
    private float size = 1f;
    private float height = 1f;

    private float timeToChange;
    private float time;
    private bool isChanging;

    public event Action onTargetSizeUpdate;

    public float Size => size * sizeMultiplier;
    public float Completeness => completeness;
    private float completeness;

    private void Awake(){
        material = blizzardRenderer.sharedMaterial;
        UpdateSize(DEFAULT_SIZE);
    }

    private void Start(){
        if (GameEvents.current != null){
            GameEvents.current.onDeathProgressUpdate += SetTargetSize;
        }
    }

    private void Update()
    {
        if (isChanging){
            CountDown();

            float lerp = Mathf.InverseLerp(0f, timeToChange, time);
            lerp = Easing.QuadEaseOut(lerp);
            float newSize = Mathf.Lerp(sizeBeforeChange, targetSize, lerp);

            UpdateSize(newSize);
        }
    }

    private void CountDown()
    {
        time += Time.deltaTime;
        if (time >= timeToChange){
            isChanging = false;
        }
    }

    private void UpdateSize(float newSize)
    {
        size = newSize;
        height = GetHeight(Size);
        Vector3 scale = new Vector3(Size, height, Size);

        foreach (BlizzardRing ring in rings){
            ring.SetScale(scale);
        }

        SetNoiseTiling(height);
        SetBigNoiseTiling(height);
    }

    private float GetHeight(float size) => ((1f - size) * heightMultiplier) + 1f;

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
        sizeBeforeChange = size;
        targetSize = sizeProgression.Evaluate(progress);
        timeToChange = (progress * 10f) * timeToChangeMultiplier;
        time = 0f;
        isChanging = true;

        onTargetSizeUpdate?.Invoke();
    }
}
