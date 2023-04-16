using UnityEngine;

public class FireballMover
{
    private FireballData data;
    private IChronos chronos;

    public FireballMover(FireballData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void Move(){
        UpdateHomingSpeed();
        RotateTowardTheTarget();
        MoveForward();
    }

    private void UpdateHomingSpeed()
    {
        data.TimeToFullHomingLeft -= Time.deltaTime;

        if (data.TimeToFullHomingLeft <= 0f){
            data.TimeToFullHomingLeft = 0f;
        }

        float lerp = Mathf.InverseLerp(data.InitialTimeToFullHoming, 0f, data.TimeToFullHomingLeft);
        lerp = Easing.QuadEaseIn(lerp);
        data.HomingSpeed = Mathf.Lerp(0f, data.MaxHomingSpeed, lerp);
    }

    private void RotateTowardTheTarget(){
        data.Direction = (data.target.position - data.Position).normalized;
        data.LookRotation = Quaternion.LookRotation(data.Direction);
        data.Rotation = Quaternion.Slerp(data.Rotation, data.LookRotation, data.HomingSpeed * Time.deltaTime);
    }

    private void MoveForward(){
        data.Position += data.Forward * data.Speed * chronos.DeltaTime;
    }
}
