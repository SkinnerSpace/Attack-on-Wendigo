using System.Collections;
using System;
using UnityEngine;
using UnityEditor;

public class WeaponKeeper : BaseController, IKeeper, IInteractor, IMousePosObserver
{
    private MainController main;
    private ICharacterData data;
    public Transform Root => data.Cam.transform;

    private Weapon weapon;

    public override void Initialize(MainController main)
    {
        this.main = main;
        data = main.Data; 
    }

    public override void Connect()
    {
        MainInputReader.Get<InteractionInputReader>().Subscribe(this);
        MainInputReader.Get<MousePositionInputReader>().Subscribe(this);
    }

    public void TakeAnItem(Transform item)
    {
        Weapon takenWeapon = item.GetComponent<Weapon>();

        if (takenWeapon != weapon)
        {
            weapon = takenWeapon;
            weapon.PickUp(this, CallMeBack);
        }
    }

    public void Interact() => DropAnItem();

    public void DropAnItem()
    {
        if (weapon != null)
        {
            weapon.Drop(data.CameraForward * data.DropItemStrength);
            weapon = null;

            // IF there is an obstacle before the weapon then offset its position
        }

        
    }

    private void CallMeBack() => Debug.Log("Item PICKED UP!");

    Vector3 lineStart;
    Vector3 lineEnd;

    public void OnMousePosUpdate(Vector2 pos)
    {
        Ray ray = data.Cam.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Vision))
        {
            if (weapon != null)
            {
                
                Vector3 shortVec = ray.direction * (hit.distance - 2);
                Vector3 closerPoint = ray.origin + shortVec;

                Vector3 weaponVec = (weapon.transform.position - closerPoint);

                float dot = Vector3.Dot(ray.direction, weaponVec);

                if (dot > 0f)
                    Debug.Log(dot);

                (Vector3 pos, Color col) point;
                point.pos = closerPoint;
                point.col = Color.red;

                TestVisualizer.Instance.DrawPoint(point);

                // When weapon clips through the wall make an offeset position of the weapon with a distance equal the dot product
                // When You drop a weapon the weapon would be set in that position so that it would not be in the wall!
            }
        }
    }
}
