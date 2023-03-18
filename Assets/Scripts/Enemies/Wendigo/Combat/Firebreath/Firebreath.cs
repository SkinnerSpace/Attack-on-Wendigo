﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WendigoCharacter
{
    public class Firebreath : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private FirebreathCollider firebreathCollider;
        [SerializeField] private FirebreathSurfaceSoundEmitter sFXEmitter;
        [SerializeField] private ParticleSystem firebreathVFX;
        [SerializeField] private GameObject flameVFX;
        [SerializeField] private Chronos chronos;

        [Header("Settings")]
        [SerializeField] private bool visualizeRaycast;

        private bool isSpewingFire;

        public void Launch()
        {
            isSpewingFire = true;
            firebreathVFX.Play();
            sFXEmitter.Play();
        }

        public void Stop()
        {
            isSpewingFire = false;
            firebreathCollider.Shrink();
            firebreathVFX.Stop();
            sFXEmitter.Stop();
        }

        public void UpdateFire()
        {
            if (isSpewingFire)
            {
                firebreathCollider.Expand();
                firebreathCollider.ActUponColliders(SetOnFire);
                sFXEmitter.UpdatePosition();
            }
        }

        private void SetOnFire(Collider subject)
        {
            IInflammable inflammable = subject.GetComponent<IInflammable>();
            inflammable.SetOnFire();
        }
    }
}

