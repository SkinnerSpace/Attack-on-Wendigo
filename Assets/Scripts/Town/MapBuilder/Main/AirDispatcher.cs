using UnityEngine;

public class AirDispatcher : MonoBehaviour
{
    private Map map;

    public void SetMap(Map map){
        this.map = map;
    }

    public Vector3 GetLandingPosition()
    {
        // Make dispatcher find cells with all neighbours being empty within the town boundaries,
        // otherwise give a cell with the least amount of neighbours wihin the town boundaries
    }
}
