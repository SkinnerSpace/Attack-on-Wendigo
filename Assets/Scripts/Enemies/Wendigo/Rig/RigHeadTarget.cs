﻿using UnityEngine;

namespace WendigoCharacter
{
    public class RigHeadTarget : MonoBehaviour, IPooledObjectObserver
    {
        [SerializeField] private Wendigo wendigo;
        [SerializeField] private RigController rigController;
        [SerializeField] private Transform defaultPoint;
        [SerializeField] private WendigoPooledObject pooledObject;

        private Transform target;
        private Vector3 velocity;
        private Vector3 targetPosition;
        private WendigoData data;

        private void Awake()
        {
            pooledObject.SubscribeOnSpawn(OnSpawn);
        }

        public void OnSpawn()
        {
            wendigo.GetController<WendigoTargetManager>().Subscribe(SetTarget);
            data = wendigo.Data;
        }

        private void Update()
        {
            if (target != null)
            {
                targetPosition = data.Head.OnTarget ? target.position : defaultPoint.position;
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
