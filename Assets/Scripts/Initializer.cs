using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    static public Capital[] capitals = MapGen.capitals;
    // Start is called before the first frame update
    static public Player[] players;
    static public Color[] colors;
    public void initalizeAll()
    {
        initalizeTeamNumbers();
        initalizePlayers();
        initalizeShops();
        initalizeColors();
        setDistrictColors();
    }

    void initalizeTeamNumbers()
    {
        for (int teamNumber = 0; teamNumber < capitals.Length; teamNumber++)
        {
            Capital capital = capitals[teamNumber];
            initalizeDistrict(capital, teamNumber);
            foreach (District district in capital.districts)
            {
                initalizeDistrict(district, teamNumber);
            }
        }
    }

    void initalizeDistrict(District district, int teamNumber)
    {
        district.capital = capitals[teamNumber];
        district.setSoldierSpawnPoint();
    }

    void initalizePlayers()
    {
        players = new Player[capitals.Length];
        int teamNumberCounter = 0;
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")){
            initalizeHumanPlayer(player.GetComponent<HumanPlayer>(), teamNumberCounter);
            teamNumberCounter += 1;
        }
        for (int teamNumber = teamNumberCounter; teamNumber < capitals.Length; teamNumber++)
        {
            initalizeAIPlayer(teamNumber);
        }
    }

    void initalizeAIPlayer(int teamNumber)
    {
        AIPlayer newAI = new AIPlayer();
        initalizePlayer(newAI, teamNumber);
    }

    void initalizeHumanPlayer(HumanPlayer player, int teamNumber)
    {
        initalizePlayer(player, teamNumber);
    }

    void initalizePlayer(Player player, int teamNumber)
    {
        player.teamNumber = teamNumber;
        player.capital = capitals[teamNumber];
        players[teamNumber] = player;
        capitals[teamNumber].player = player;
    }

    void initalizeShops()
    {
        Shop.soldierPrefabs = MapGen.soldierPrefabs;
        Shop.prices = new float[Shop.soldierPrefabs.Length];
        Shop.prices[0] = 20;
    }

    void initalizeColors()
    {
        colors = new Color[8];
        colors[0] = Color.blue;
        colors[1] = Color.red;
        colors[2] = Color.green;
        colors[3] = Color.yellow;
        colors[4] = Color.black;
        colors[5] = Color.cyan;
        colors[6] = Color.magenta;
        colors[7] = Color.grey;
    }

    void setDistrictColors()
    {
        foreach (Capital capital in capitals)
        {
            Color color = colors[capital.teamNumber];
            capital.GetComponent<MeshRenderer>().material.color = color;
            foreach (MeshRenderer mesh in capital.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material.color = color;
            }
        }
    }
}
