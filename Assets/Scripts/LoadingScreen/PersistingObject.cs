using UnityEngine;

public class PersistingObject : MonoBehaviour
{
    public string objectID;

    private void Start()
    {
        PersistingObject[] objects = FindObjectsOfType<PersistingObject>();

        for (int i = 0; i < objects.Length; i++){
            DestroyClones(objects[i]);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void DestroyClones(PersistingObject foundObject){
        if (IsAClone(foundObject)){
            Destroy(gameObject);
        }
    }

    private bool IsAClone(PersistingObject foundObject){
        return foundObject != this &&
               foundObject.objectID == objectID;
    }
}