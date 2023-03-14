using UnityEngine;

namespace WendigoCharacter
{
    public class FirebreathSurfaceSoundEmitter : MonoBehaviour
    {
        [SerializeField] private WendigoData data;
        [SerializeField] private Transform emissionPoint;
        [SerializeField] private FMODUnity.EventReference burnSFX;

        private FirebreathColliderData colliderData => data.Firebreath.Collider;
        private AudioPlayer burnSFXPlayer;

        private void Awake(){
            emissionPoint.parent = data.transform.parent;
            burnSFXPlayer = AudioPlayer.Create(burnSFX).WithAnchor(emissionPoint);
        }

        public void UpdatePosition(){
            emissionPoint.position = transform.position + (transform.forward * colliderData.ObservableRadius * colliderData.ObservableExpansion);
        }

        public void Play() => burnSFXPlayer.PlayLoop();
        public void Stop() => burnSFXPlayer.Stop();
    }
}

