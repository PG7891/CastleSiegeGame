using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGen : MonoBehaviour
{
    Capital[] capitals;
    float districtHeight;
    float capitalHeight;
    float capitalY;
    float districtY;

    public void runBuildingGen()
    {
        capitalHeight = MapGen.capitalPrefab.transform.localScale.y;
        districtHeight = MapGen.districtPrefab.transform.localScale.y;
        capitalY = capitalHeight + TerrainGen.groundThick;
        districtY = districtHeight + TerrainGen.groundThick;
        generateBuildings(MapGen.capitalCount, MapGen.center, MapGen.capitalToCenterDist);
    }

    public void generateBuildings(int capitalCount, Vector3 center, float dist){
        capitals = new Capital[capitalCount];
        for(int counter = 0; counter < capitalCount; counter++){
            Capital capital = generateCapital(new Vector3(dist, capitalY, 0));
            capitals[counter] = capital;
            float angle = counter * (360/capitalCount);
            capital.transform.RotateAround(center, Vector3.up, angle);
            District[] districts = generateDistricts(capital);
            capital.districts = districts;
        }
        MapGen.capitals = capitals;
    }

    Capital generateCapital(Vector3 position){
        return Instantiate(MapGen.capitalPrefab, position, Quaternion.identity).GetComponent<Capital>();
    }

    District[] generateDistricts(Capital capital){
        Vector3 centerDistrictPos = findCenterDistrictLocation(capital);
        District[] districts = new District[MapGen.districtCount];
        for(int count = 1; count <= MapGen.districtCount; count++){
            District district = generateDistrict(new Vector3(centerDistrictPos.x, districtY, centerDistrictPos.z), capital);
            float angle = (count*(180/(MapGen.districtCount+1))) - 90;
            district.transform.RotateAround(capital.transform.position, Vector3.up, angle);
            districts[count-1] = district;
        }
        return districts;
    }

    Vector3 findCenterDistrictLocation(Capital capital){
        Vector3 capitalToCenter = MapGen.center - capital.transform.position;
        Vector3 capitalToDistrict = capitalToCenter * (MapGen.districtToCapitalDist / MapGen.capitalToCenterDist);
        return capital.transform.position + capitalToDistrict;
    }

    District generateDistrict(Vector3 position, Capital capital){
        return Instantiate(MapGen.districtPrefab, position, Quaternion.identity, capital.transform).GetComponent<District>();
    }
}
