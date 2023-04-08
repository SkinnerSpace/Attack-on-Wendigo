using UnityEngine;

namespace WendigoCharacter
{
    public class CorpseGroundCollider : MonoBehaviour, IAmputationObserver
    {
        [SerializeField] private CorpseCollisionShape collisionShape;

        private Rigidbody body;
        private CorpseCollisionController controller;
        private CorpseCollisionAftermathController aftermathController;
        private ShakeSettings shake;
        private WendigoData data;

        private bool isEnabled;
        private bool isDestroyed;

        private void Awake()
        {
            enabled = false;
            collisionShape.Subscribe(HandleCollision);

            body = transform.parent.GetComponent<Rigidbody>();
        }

        public void Initialize(CorpseCollisionController controller, WendigoData data, ShakeSettings shake){
            this.controller = controller;
            this.data = data;
            this.shake = shake;
        }

        private void Start(){
            aftermathController = new CorpseCollisionAftermathController(data, body, shake);
        }


        public void OnAmputation() => SwitchOff();

        public void SwitchOn()
        {
            if (!isDestroyed){
                isEnabled = true;
                enabled = true;
            }
        }

        public void SwitchOff()
        {
            isDestroyed = true;
            isEnabled = false;
            enabled = false;
        }

        private void Update()
        {
            if (isEnabled){
                collisionShape.UpdateCollision();
            }
        }

        private void HandleCollision()
        {
            Ray ray = new Ray(collisionShape.Center, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Landscape)){
                aftermathController.ActUponCollisionPoint(hit);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (IsAbleToVisualize()){
                collisionShape.Visualize();
            }
        }

        private bool IsAbleToVisualize() => controller != null && collisionShape != null && controller.Visualize;
#endif
    }
}


