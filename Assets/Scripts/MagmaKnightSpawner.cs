using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaKnightSpawner : MonoBehaviour
{
    [SerializeField] GameObject magmaKnight;
    [SerializeField] static int knightsNumber = 5;
    private GameObject[] knights = new GameObject[knightsNumber];

    private float spawnTimer = 0f;
    private float spawnTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < knightsNumber; i++)
        {
            knights[i] = Instantiate(magmaKnight);
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
        GameObject[] magmaTowers; //Je viens les rechercher à chaque fois, si jamais elles sont détruires on ne spawnera plus x)
        magmaTowers = GameObject.FindGameObjectsWithTag("MagmaTower");

        if (magmaTowers.Length > 0)
        {
            int rand = Random.Range(0, magmaTowers.Length);
            for (int i = 0; i < knightsNumber; i++)
            {
                if (!knights[i].activeSelf)
                {
                    knights[i].transform.position = new Vector3(magmaTowers[rand].transform.position.x + 1, magmaTowers[rand].transform.position.y, magmaTowers[rand].transform.position.z);
                    knights[i].SetActive(true);
                    GameManager.instance.AddMagmaKnight();
                    return;
                }
            }
        }
    }
}
