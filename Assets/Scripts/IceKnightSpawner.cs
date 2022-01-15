using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKnightSpawner : MonoBehaviour
{
    [SerializeField] GameObject iceKnight;
    [SerializeField] static int knightsNumber = 5; 
    private GameObject[] knights = new GameObject[knightsNumber];

    private float spawnTimer = 0f;
    private float spawnTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < knightsNumber; i++)
        {
            knights[i] = Instantiate(iceKnight);
            knights[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime)
        {
            SpawnKnight();
            spawnTimer = 0f;
        }
    }

    public void SpawnKnight()
    {
        GameObject[] iceTowers; //Je viens les rechercher à chaque fois, si jamais elles sont détruires on ne spawnera plus x)
        iceTowers = GameObject.FindGameObjectsWithTag("IceTower");

        if (iceTowers.Length > 0) { 
            int rand = Random.Range(0, iceTowers.Length);

            for (int i = 0; i < knightsNumber; i++)
            {
                if (!knights[i].activeSelf)
                {
                    knights[i].transform.position = new Vector3(iceTowers[rand].transform.position.x - 1, iceTowers[rand].transform.position.y, iceTowers[rand].transform.position.z);
                    knights[i].SetActive(true);
                    GameManager.instance.AddIceKnight();
                    return;
                }
            }
        }
    }
}
