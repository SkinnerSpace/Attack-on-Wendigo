using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AirDispatcher : MonoBehaviour, IAirDispatcher
{
    [Header("Landing")]
    [SerializeField] private int landingZoneSize = 18;

    [Header("Escape")]
    [SerializeField] private float escapeRadius;
    [SerializeField] private float minEscapeHeight = 30f;
    [SerializeField] private float maxEscapeHeight = 40f;
    [SerializeField] private Vector3 escapeAreaOffset;

    private List<Mark> potentialLandingPlaces;

    private float testSphereRadius = 10f;
    private Vector3 testSpherePosition;
    private bool visualizeEscapePosition;

    public void SetMap(Map map){
        MapNeighbourFinder.InitializeNeighbours(map, landingZoneSize);
        GetMarksWithTheLeastAmountOfNeighbours();
    }

    public void GetMarksWithTheLeastAmountOfNeighbours()
    {
        potentialLandingPlaces = new List<Mark>();

        for (int i = 4; i >= 0; i--){
            List<Mark> marks = Mark.marksSortedByNeighboursCount[i];

            if (marks != null){
                foreach (Mark mark in marks){
                    potentialLandingPlaces.Add(mark);
                }
            }
        }
    }

    public Vector3 GetTheLandingPosition(Vector3 position, float minHeight)
    {
        Mark landingPlace = potentialLandingPlaces.
            OrderBy(t => t.neighboursCount).
            FirstOrDefault();

        Vector3 landingPosition = landingPlace.worldPosition + new Vector3(0f, minHeight, 0f);

        return landingPosition;
    }

    public void TestEscapePositionGeneration()
    {
        visualizeEscapePosition = true;
        testSpherePosition = GetEscapePosition();
    }

    public void StopTesting()
    {
        visualizeEscapePosition = false;
    }

    public Vector3 GetEscapePosition()
    {
        Vector3 randomPosition = escapeAreaOffset + (Rand.GetCircularDirection() * escapeRadius);
        float escapeHeight = Rand.Range(minEscapeHeight, maxEscapeHeight);
        randomPosition = new Vector3(randomPosition.x, escapeHeight, randomPosition.z);

        return randomPosition;
    }

# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (visualizeEscapePosition)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, escapeRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(testSpherePosition, testSphereRadius);

            Gizmos.color = Color.white;
        }
    }
#endif
}
