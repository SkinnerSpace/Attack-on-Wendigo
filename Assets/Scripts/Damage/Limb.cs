using UnityEngine;

public class Limb : MonoBehaviour
{
    private float INJURY_THRESHOLD = 0.5f;
    private float AMPUTATION_THRESHOLD = 0.1f;

    private enum States
    {
        Flesh,
        Bones,
        Destroyed
    }

    [SerializeField] private int initialHealth;
    [SerializeField] private SkinnedMeshRenderer fleshMesh;
    [SerializeField] private SkinnedMeshRenderer musclesMesh;
    [SerializeField] private SkinnedMeshRenderer bonesMesh;

    private int health;

    private States state = States.Flesh;
    private float healthPercent;

    private void Awake()
    {
        health = initialHealth;
    }

    public void ReceiveDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }

        healthPercent = Mathf.InverseLerp(0f, initialHealth, health);
        Debug.Log(transform.name + " " + healthPercent);

        if (ReadyForMutilation()){
            Mutilate();
        }
        else if (ReadyForAmputation()){
            Amputate();
        }
    }

    private bool ReadyForMutilation(){
        return healthPercent <= INJURY_THRESHOLD && 
               state == States.Flesh;
    }

    private bool ReadyForAmputation(){
        return healthPercent <= AMPUTATION_THRESHOLD && 
               state == States.Bones;
    }

    private void Mutilate()
    {
        state = States.Bones;
        fleshMesh.enabled = false;
        musclesMesh.enabled = true;
        bonesMesh.enabled = true;
        Debug.Log("INJURED");
    }

    private void Amputate()
    {
        state = States.Destroyed;
        musclesMesh.enabled = false;
        bonesMesh.enabled = false;
        Debug.Log("DESTROYED");
    }
}