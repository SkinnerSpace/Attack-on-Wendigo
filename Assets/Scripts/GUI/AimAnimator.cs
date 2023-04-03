using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimAnimator : MonoBehaviour
{
    private const float DISTANCE_UNIT = 16f;
    private const float ROTATION_UNIT = -90f;
    private const float TIME_MULTIPLIER = 0.7f;

    [Header("Required Components")]
    [SerializeField] private RectTransform aim;
    [SerializeField] private List<RectTransform> aimElements;
    [SerializeField] private AimAnimationsPack animationData;

    [Header("Test")]
    [SerializeField] private bool testMode;
    [Range(0f, 1f)]
    [SerializeField] private float completeness;
    [SerializeField] private AimAnimation testAnimation;

    private List<Image> aimImages;

    private AimAnimation currentAnimation;

    private float distance = 1f;
    private float rotation = 0f;
    private float scale = 0.5f;

    private float time;
    private float animationTime;
    private bool isPlaying;

    public void SetAnimationData(AimAnimationsPack animationData, float animationTime){
        this.animationData = animationData;
        this.animationTime = animationTime * TIME_MULTIPLIER;
    }

    public void PlayShoot()
    {
        currentAnimation = animationData.shoot;
        ResetAnimation();
    }

    private void ResetAnimation(){
        time = 0f;
        isPlaying = true;
    }

    private void Awake()
    {
        FindAimImages();
    }

    private void Update()
    {
        if (!testMode && isPlaying){
            Tick();
            UpdateAnimation();
        }
        else if (testMode){
            UpdateTestAnimation();
        }
        
        UpdateAimTransform();
    }

    private void FindAimImages()
    {
        aimImages = new List<Image>();

        foreach (RectTransform aimElement in aimElements)
        {
            aimImages.Add(aimElement.GetComponent<Image>());
        }
    }

    private void Tick()
    {
        if (time < animationTime)
        {
            time += Time.deltaTime;
            completeness = time / animationTime;
            completeness = Mathf.Min(completeness, 1f);

            isPlaying = completeness < 1f;
        }
    }

    private void UpdateAnimation()
    {
        distance = currentAnimation.distance.Evaluate(completeness) * DISTANCE_UNIT;
        rotation = currentAnimation.rotation.Evaluate(completeness) * ROTATION_UNIT;
        scale = currentAnimation.scale.Evaluate(completeness);
    }

    private void UpdateTestAnimation()
    {
        distance = testAnimation.distance.Evaluate(completeness) * DISTANCE_UNIT;
        rotation = testAnimation.rotation.Evaluate(completeness) * ROTATION_UNIT;
        scale = testAnimation.scale.Evaluate(completeness);
    }

    private void UpdateAimTransform()
    {
        foreach (RectTransform aimElement in aimElements)
        {
            aimElement.localPosition = new Vector3(0f, distance, 0f);
            aim.localEulerAngles = new Vector3(0f, 0f, rotation);
            aim.localScale = new Vector3(scale, scale, 0f);
        }
    }
}