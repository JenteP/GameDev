using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    private GameObject WeaponlessMummyPrefab, ArmedMummyPrefab, CasterMummyPrefab;

    // Use this for initialization
    void Start()
    {
        WeaponlessMummyPrefab = Resources.Load("GameWorld/Enemies/Mummy/Prefabs/Weaponless") as GameObject;
        ArmedMummyPrefab = Resources.Load("GameWorld/Enemies/Mummy/Prefabs/Armed") as GameObject;
        CasterMummyPrefab = Resources.Load("GameWorld/Enemies/Mummy/Prefabs/Caster") as GameObject;
    }

    private void SpawnWeaponlessEnemies(List<Vector3> positions)
    {
        int counter = 1;
        foreach (Vector3 position in positions)
        {
            GameObject weaponlessMummy = Instantiate(WeaponlessMummyPrefab, position, Quaternion.identity) as GameObject;
            weaponlessMummy.name = "Weaponless_Mummy_" + counter;
            counter++;
        }
    }

    private void SpawnArmedEnemies(List<Vector3> positions)
    {
        int counter = 1;
        foreach (Vector3 position in positions)
        {
            GameObject armedMummy = Instantiate(ArmedMummyPrefab, position, Quaternion.identity) as GameObject;
            armedMummy.name = "Armed_Mummy_" + counter;
            counter++;
        }
    }

    private void SpawnCasterEnemies(List<Vector3> positions)
    {

    }

    public void SpawnEnemyGroup1()
    {
        List<Vector3> weaponlesspositions = new List<Vector3>();
        List<Vector3> armedPositions = new List<Vector3>();

        //Positions weaponless enemies
        weaponlesspositions.Add(new Vector3(109, 10, 232));
        weaponlesspositions.Add(new Vector3(120, 10, 226));
        weaponlesspositions.Add(new Vector3(113, 10, 223));
        weaponlesspositions.Add(new Vector3(107, 10, 219));
        weaponlesspositions.Add(new Vector3(76, 10, 220));

        //Positions armed enemies
        armedPositions.Add(new Vector3(148, 10, 261));
        armedPositions.Add(new Vector3(150, 10, 258));
        armedPositions.Add(new Vector3(124, 10, 230));
        armedPositions.Add(new Vector3(104, 10, 217));
        armedPositions.Add(new Vector3(78, 10, 215));

        SpawnWeaponlessEnemies(weaponlesspositions);
        SpawnArmedEnemies(armedPositions);
    }
}
