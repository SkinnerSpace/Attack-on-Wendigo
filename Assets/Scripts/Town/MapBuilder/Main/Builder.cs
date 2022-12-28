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
    [SerializeField] private float buildSpeed;

    [SerializeField] private Provider provider;

    private bool nonEmptyFound;
    private int countBeforeFound;

    private int x;
    private int y;

    private Blueprint blueprint;

    private int offset;
    private int width;
    private int height;

    private Vector3 fieldPosition;
    private float scale;

    public void Build(Blueprint blueprint)
    {
        this.blueprint = blueprint;
        offset = blueprint.offset;

        width = blueprint.map.GetLength(1) - offset;
        height = blueprint.map.GetLength(0) - offset;

        x = offset;
        y = offset;

        fieldPosition = blueprint.field.position;
        scale = blueprint.scale;

        switch (buildMode)
        {
            case BuildModes.INSTANTLY:
                BuildInstantly(); 
                //Iterate();
                break;

            case BuildModes.GRADUALLY:
                StartCoroutine(BuildGradually());
                break;
        }
    }

    private void Iterate()
    {
        while (y < height)
        {
            while (x < width)
            {
                x++;
                BuildObject(blueprint, new Cell(x, y), blueprint.map[x, y]);
            }
            x = offset;
            y++;
        }
    }

    private void BuildInstantly()
    {
        Debug.Log("Overall length " + blueprint.map.Length);
        Debug.Log("Offset " + blueprint.offset);

        for (y = offset; y < height; y++)
        {
            for (x = offset; x < width; x++)
            {
                if (!nonEmptyFound)
                    countBeforeFound += 1;

                BuildObject(blueprint, new Cell(x, y), blueprint.map[x, y]);
            }
        }

        Debug.Log("Empties before found " + countBeforeFound);
    }

    private IEnumerator BuildGradually()
    {
        for (y = offset; y < height; y++)
        {
            for (int x = offset; x < width; x++)
            {
                BuildObject(blueprint, new Cell(x, y), blueprint.map[x, y]);

                yield return null;// new WaitForSeconds(0.1f);
            }
        }
    }

    private void BuildObject(Blueprint blueprint, Cell cell, Mark mark)
    {
        if (!mark.IsEmpty())
        {
            nonEmptyFound = true;
            Vector3 position = (fieldPosition + new Vector3(cell.X, 0f, cell.Y)) * scale;
            Instantiate(provider.GetObject(mark), position, Quaternion.identity, blueprint.field);
        }
    }
}