using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int capitalCountP;
    public GameObject capitalPrefabP;
    public GameObject districtPrefabP;
    public GameObject groundPrefabP;
    public float capitalToCenterDistP;
    public float districtToCapitalDistP;
    public int districtCountP;
    public GameObject[] soldierPrefabsP;
    public Vector3 centerP = Vector3.zero;

    static BuildingGen buildingGen;
    static TerrainGen terrainGen;
    static Initializer initializer;
    static SoldierSpawner soldierSpawner;

    static public int capitalCount;
    static public GameObject capitalPrefab;
    static public GameObject districtPrefab;
    static public GameObject groundPrefab;
    static public float capitalToCenterDist;
    static public float districtToCapitalDist;
    static public int districtCount;
    static public GameObject[] soldierPrefabs;
    static public Vector3 center;

    static public Capital[] capitals;
    void Start()
    {
        initalizeStatics();
        initializeGens();
        callGens();
        setInitalizer();
        callInitalizer();
    }

    void initalizeStatics(){
        capitalCount = capitalCountP;
        capitalPrefab = capitalPrefabP;
        districtPrefab = districtPrefabP;
        groundPrefab = groundPrefabP;
        capitalToCenterDist = capitalToCenterDistP;
        districtToCapitalDist = districtToCapitalDistP;
        districtCount = districtCountP;
        soldierPrefabs = soldierPrefabsP;
        center = centerP;
    }

    void initializeGens(){
        buildingGen = new BuildingGen();
        terrainGen = new TerrainGen();
    }

    void callGens(){
        buildingGen.runBuildingGen();
        terrainGen.generateTerrain();
    }

    void setInitalizer(){
        initializer = new Initializer();
    }

    void callInitalizer(){
        initializer.initalizeAll();
    }

    void setSoldierSpawner(){
        soldierSpawner = new SoldierSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
