using UnityEngine;

public class WeaponOscillator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float sinFrequency = 8f;
    [SerializeField] private float sinMagnitude = 1f;

    [SerializeField] private bool sinX = true;
    [SerializeField] private bool sinY = true;

    private ICharacterData characterData;
    private SinCounter sinCounter = new SinCounter();

    public Vector2 movement { get; private set; }

    public WeaponOscillator InitializeOnTake(ICharacterData characterData)
    {
        this.characterData = characterData;
        return this;
    }

    public void ReadInput()
    {
        if (characterData.IsGrounded)
            Wave(characterData.FlatVelocity.magnitude);
    }

    public void Wave(float movementMagnitude)
    {
        float force = (movementMagnitude * 0.05f) * sinFrequency;

        if (force > 0f)
        {
            sinCounter.CountTime(force);
            movement = GetSinMovement(force);
        }
    }

    private Vector2 GetSinMovement(float movementMagnitude)
    {
        float sin = (Mathf.Sin(sinCounter.time) * sinMagnitude) * movementMagnitude;

        Vector3 sinMovement = new Vector2(
            sinX ? sin : 0f,
            sinY ? sin : 0f);

        return sinMovement;
    }
}
