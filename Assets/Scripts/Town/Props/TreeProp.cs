using UnityEngine;

public class TreeProp : Prop
{
    private const int COLLAPSE_TRANSFORM = 1;
    private const int ADJUSTMENT_TRANSFORM = 2;

    private const float GRAVITY = 0.8f;
    private const float MIN_BOUNCINESS = 0.25f;
    private const float MAX_BOUNCINESS = 0.5f;

    Vector3 beforeCollapse;
    Vector3 afterCollapse;
    
    private float collapseSpeed;
    private float bounciness;
    private float lerp;

    private bool bounced;

    public override void Collapse(Vector3 impact)
    {
        isCollapsing = true;
        bounciness = UnityEngine.Random.Range(MIN_BOUNCINESS, MAX_BOUNCINESS);

        float angleY = (Mathf.Atan2(impact.x, impact.z) * Mathf.Rad2Deg) + transforms[0].Angle.y;
        Vector3 fallDirection = new Vector3(0f, angleY, 0f);
        Vector3 invertedFallDirection = new Vector3(0f, -angleY, 0f);

        transforms[COLLAPSE_TRANSFORM].LocalAngle = fallDirection;
        transforms[ADJUSTMENT_TRANSFORM].LocalAngle = invertedFallDirection;

        beforeCollapse = new Vector3(0f, angleY, 0f);
        afterCollapse = new Vector3(0f, angleY, 80f);
    }

    public override void UpdateCollapse()
    {
        collapseSpeed += GRAVITY * Time.deltaTime;
        lerp += collapseSpeed * Time.deltaTime;
        lerp = Mathf.Min(1f, lerp);

        if (lerp >= 1f)
            BounceOrStop();

        transforms[COLLAPSE_TRANSFORM].LocalAngle = Vector3.Lerp(beforeCollapse, afterCollapse, lerp);
        //Debug.Log("Update");
    } 

    private void BounceOrStop()
    {
        collapseSpeed = -1 * (collapseSpeed * bounciness);

        if (Mathf.Abs(collapseSpeed) <= 0.01f)
            isCollapsing = false;
    }
}
