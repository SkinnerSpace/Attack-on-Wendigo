﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoData : MonoBehaviour
    {
        public HealthData Health;
        public MovementData Movement;
        public FirebreathAbilityData Firebreath;
        public FireballSpawnerData FireballSpawner;
        public FireballAbilityData Fireball;
        public HeadData Head;

        public WendigoTarget Target;

        public TransformData Transform { get; private set; }

        public bool IsActive { get; set; }
        public bool IsArrived { get; set; }

        private List<IRebootable> backup;

        private void Awake()
        {
            Transform = new TransformData(transform);
            CreateBackup();
        }

        private void CreateBackup()
        {
            backup = new List<IRebootable>()
            {
                Health,
                Movement,
                Firebreath,
                Fireball,
                Head
            };

            foreach (IRebootable rebootable in backup)
                rebootable.Save();
        }


        public void ResetData()
        {
            foreach (IRebootable rebootable in backup)
                rebootable.Reboot();

            IsActive = false;
            IsArrived = false;
        }
    }
}
