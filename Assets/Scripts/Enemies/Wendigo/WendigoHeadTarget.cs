using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace WendigoCharacter
{
    public class WendigoHeadTarget : MonoBehaviour
    {
        [SerializeField] private Wendigo wendigo;
        [SerializeField] private RigController rigController;
        [SerializeField] private Transform defaultPoint;

        private Transform target;
        private Vector3 velocity;
        private Vector3 targetPosition;
        private WendigoData data;

        private void Start()
        {
            wendigo.GetController<WendigoTargetManager>().Subscribe(SetTarget);
            data = wendigo.Data;
        }

        private void Update()
        {
            if (target != null)
            {
                targetPosition = data.TargetFitsLookAngle ? target.position : defaultPoint.position;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.5f);
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;

            if (target != null) rigController.SwitchOn();
            else rigController.SwitchOff();
        }
    }
}
