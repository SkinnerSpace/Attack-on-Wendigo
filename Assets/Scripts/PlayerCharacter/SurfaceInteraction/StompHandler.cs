﻿using UnityEngine;

namespace Character
{
    public class StompHandler : BaseController, ISurfaceObserver
    {
        private PlayerCharacter main;
        private ICharacterData data;

        private bool skipTheFirstStomp = true;

        public override void Initialize(PlayerCharacter main)
        {
            this.main = main;
            data = main.OldData;
        }

        public override void Connect() => main.GetController<SurfaceDetector>().Subscribe(this);
        public override void Disconnect() { }

        public void OnSurfaceFound(SurfaceProbe probe) => HitTheSurface(probe);

        private void HitTheSurface(SurfaceProbe probe)
        {
            if (!skipTheFirstStomp)
            {
                ISurface surface = probe.surface;
                Vector3 position = probe.position;
                Vector3 direction = data.FlatVelocity.normalized;

                surface.Hit().
                        WithPosition(position).
                        WithAngle(direction, Vector3.up).
                        WithShape(1f, 70f).
                        WithCount(5, 10).
                        Launch();
            }
            else
            {
                skipTheFirstStomp = false;
            }
        }
    }
}