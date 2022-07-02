using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{

    static public float groundThick = 1f;
    public void generateTerrain(){
        GameObject ground = Instantiate(MapGen.groundPrefab, Vector3.zero, Quaternion.identity);
        float y = groundThick;
        float xz = MapGen.capitalToCenterDist * 2.3f;
        ground.transform.localScale = new Vector3(xz, y, xz);
    }
}
