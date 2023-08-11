using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateAll : MonoBehaviour
{

    public List<PopulateCollider> PopulateColliders = new List<PopulateCollider>();
    public List<PopulateSquareSurface> PopulateSurfaces = new List<PopulateSquareSurface>();
    public void Generate()
    {
        ClearSpheres();
        CreateSpheres();
    }

    public void DestroyAll()
    {
        ClearSpheres();
    }

    private void CreateSpheres()
    {
        for (int i = 0; i < PopulateColliders.Count; i++)
        {
            PopulateColliders[i].Generate();
        }

        for (int i = 0; i < PopulateSurfaces.Count; i++)
        {
            PopulateSurfaces[i].Generate();
        }
    }

    private void ClearSpheres()
    {
        for (int i = 0; i < PopulateColliders.Count; i++)
        {
            PopulateColliders[i].DestroyAll();
        }

        for (int i = 0; i < PopulateSurfaces.Count; i++)
        {
            PopulateSurfaces[i].DestroyAll();
        }
    }

}
