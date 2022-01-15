using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KnightStateHide : KnightState
{
    private LifeManager lifeManager;
    private KnightManager knightManager;
    private GameObject nearestTower;
    private GameObject[] towers;

    private bool isAttacked = false;
    private bool isInTower = false;
    private float lifeTimer = 0;

    public override void ManageStateChange()
    {
        if (lifeManager.GetLives() == lifeManager.GetInitialLives() ||
            (lifeManager.GetLives() >= 0.5 * lifeManager.GetInitialLives() && isAttacked))
        {
            GetComponent<KnightManager>().ChangeKnightState(KnightManager.KnightStateToSwitch.Normal);
        }
        else if (isInTower)
        {
            GetComponent<KnightManager>().ChangeKnightState(KnightManager.KnightStateToSwitch.Safe);
        }
    }

    public override void MoveKnight()
    {
        transform.position = Vector3.MoveTowards(transform.position, nearestTower.transform.position, speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        lifeManager = GetComponent<LifeManager>();
        lifeManager.isProtected = true;
        knightManager = GetComponent<KnightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageStateChange();

        if (knightManager.GetEnemy() != null)
        {
            isAttacked = true;
        }

        //Si le chevalier est attaqué, il se replie vers la tour la plus proche
        if (isAttacked && lifeManager.HasToFlee())
        {
            if (nearestTower == null)
            {
                GetAllHidingSpots();
                FindNearestTower();
            }

            speed = 2;
        }

        if (lifeTimer > 0)
        {
            lifeTimer -= Time.deltaTime;
        }
        else
        {
            lifeManager.GetNewLife();

            // S'il n'attaque pas, il gagne 2 fois plus de vie
            if (currentTarget == null)
            {
                lifeManager.GetNewLife();
            }

            lifeTimer = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == nearestTower
            && Vector2.Distance(transform.position, collision.transform.position) <= 1)
        {
            isInTower = true;
        }
        else if ((collision.name.Contains("MagmaKnight") || collision.name.Contains("IceKnight"))
            && collision.tag != tag)
        {
            currentTarget = collision.gameObject;
        }
    }

    private void FindNearestTower()
    {
        Vector2 characterOffset = transform.position;
        List<float> towerDistanceArray = new List<float>();

        //On calcule les discances entre le joueur et tous les lieux de cachette
        for (int i = 0; i < towers.Length; i++)
        {
            towerDistanceArray.Add(Vector2.Distance(characterOffset, towers[i].transform.position));
        }

        //On trouve si l'élément le plus rapproché est une tour ou un lieu de sûreté
        float closestTowerPosition = towerDistanceArray.Min();

        //on définit le point le plus proche
        int hidingSpotIndex = towerDistanceArray.IndexOf(closestTowerPosition);
        nearestTower = towers[hidingSpotIndex];
    }

    private void GetAllHidingSpots()
    {
        if (this.tag == "IceKnight")
        {
            towers = GameObject.FindGameObjectsWithTag("IceTower");
        }
        else
        {
            towers = GameObject.FindGameObjectsWithTag("MagmaTower");
        }
    }
}