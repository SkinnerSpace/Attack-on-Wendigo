using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public enum BuildModes
    {
        INSTANTLY,
        GRADUALLY
    }
    [SerializeField] private BuildModes buildMode;
    [Range(1, 100)]
    [SerializeField] private int constructionSpeed;

    [SerializeField] private Provider provider;
    private Iterator2D iterator;

    private Blueprint blueprint;
    private Vector3 fieldPosition;
    private float scale;

    public void Build(Blueprint blueprint)
    {
        PrepareForConstruction(blueprint);

        switch (buildMode)
        {
            case BuildModes.INSTANTLY:
                BuildInstantly();
                break;

            case BuildModes.GRADUALLY:
                StartCoroutine(BuildGradually());
                break;
        }
    }

    private void PrepareForConstruction(Blueprint blueprint)
    {
        this.blueprint = blueprint;

        fieldPosition = blueprint.field.position;
        scale = blueprint.scale;

        int offset = blueprint.offset;

        int min_X = offset;
        int max_X = blueprint.map.GetHeight() - offset;

        int min_Y = offset;
        int max_Y = blueprint.map.GetWidth() - offset;

        iterator = new Iterator2D(min_X, max_X, min_Y, max_Y); 
    }

    private void BuildInstantly()
    {
        while (!iterator.iterationIsOver)
            BuildAndIterate();
    }

    private IEnumerator BuildGradually()
    {
        while (!iterator.iterationIsOver)
        {
            for (int i=0; i < constructionSpeed; i++)
                BuildAndIterate();

            yield return null;
        }
    }

    private void BuildAndIterate()
    {
        BuildObject(blueprint, new Cell(iterator.x, iterator.y), blueprint.map.GetMark(iterator.x, iterator.y));
        iterator.Iterate();
    }

    private void BuildObject(Blueprint blueprint, Cell cell, Mark mark)
    {
        if (!mark.IsEmpty())
        {
            Vector3 position = (fieldPosition + new Vector3(cell.X, 0f, cell.Y)) * scale;
            Instantiate(provider.GetObject(mark), position, Quaternion.identity, blueprint.field);
        }
        else if (mark.IsEmpty())
        {
            iterator.Iterate();
        }
    }
}
