using UnityEngine;

[ExecuteAlways]
public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private Transform root;
    [Header("Particles")]
    public ParticleSystem[] particles;

    private void OnEnable()
    {
        if (root != null){
            particles = root.GetComponentsInChildren<ParticleSystem>();
        }
    }
}
