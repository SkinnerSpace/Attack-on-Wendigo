using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AirDispatcher : MonoBehaviour, IAirDispatcher
{
    [SerializeField] private int size;
    [SerializeField] private float landingOffset;

    private Vector3 LandingOffset => new Vector3(0f, landingOffset, 0f);
    private List<Mark> potentialLandingPlaces;

    public void SetMap(Map map){
        MapNeighbourFinder.InitializeNeighbours(map, size);
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

    public Vector3 GetTheLandingPosition(Vector3 position)
    {
        Mark landingPlace = potentialLandingPlaces.
            OrderBy(t => t.neighboursCount).
            FirstOrDefault();

        Vector3 landingPosition = landingPlace.worldPosition + LandingOffset;

        return landingPosition;
    }
}
