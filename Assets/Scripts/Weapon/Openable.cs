using UnityEngine;

public class Openable : MonoBehaviour, IOpenable
{
    [SerializeField] private Crate crate;

    public void Open() => crate.Open();
}
