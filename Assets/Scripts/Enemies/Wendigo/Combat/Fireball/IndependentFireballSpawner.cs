using UnityEngine;

public class IndependentFireballSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            Fireball fireball = Instantiate(fireballPrefab, transform.position, transform.rotation).GetComponent<Fireball>();
            fireball.SwitchOn();
            fireball.SetTarget(target);
        }
    }
}
