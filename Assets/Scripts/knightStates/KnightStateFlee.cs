using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KnightStateFlee : KnightState
{
    private GameObject hidingSpot;
    private GameObject[] towers;
    private GameObject[] hidingSpots;

    private LifeManager lifeManager;
    private float lifeTimer = 0;
    private bool isATower;
    private bool isHidden = false;

    public override void ManageStateChange()
    {
        if (isHidden)
        {
            if (isATower)
            {
                Debug.Log("State change: Safe");
                GetComponent<KnightManager>().ChangeKnightState(KnightManager.KnightStateToSwitch.Safe);
            }
            else
            {
                Debug.Log("State change: Hide");
                GetComponent<KnightManager>().ChangeKnightState(KnightManager.KnightStateToSwitch.Hide);
            }
        }
    }

    public override void MoveKnight()
    {
        if (hidingSpot == null || hidingSpot.activeSelf == false)
        {
            GetAllHidingSpots();
            FindNearestSafeArea();
        }
        transform.position = Vector3.MoveTowards(transform.position, hidingSpot.transform.position, speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 4;
        lifeManager = GetComponent<LifeManager>();
    }

    void Awake()
    {
        GetAllHidingSpots();
        FindNearestSafeArea();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameIsOver)
        {
            MoveKnight();
            ManageStateChange();

            if (currentTower != null && currentTower.activeSelf == false)
            {
                FindRandomTower(towers);
            }

            if (lifeTimer > 0)
            {
                lifeTimer -= Time.deltaTime;
            }
            else
            {
                lifeManager.GetNewLife();
                lifeTimer = 1;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == hidingSpot
            && Vector2.Distance(transform.position, collision.transform.position) <= 0.5)
        {
            isHidden = true;
        }
    }

    private void FindNearestSafeArea()
    {
        Vector2 characterOffset = transform.position;
        List<float> towerDistanceArray = new List<float>();
        List<float> hidingSpotsDistanceArray = new List<float>();

        //On calcule les discances entre le joueur et tous les lieux de cachette
        for (int i = 0; i < towers.Length; i++)
        {
            towerDistanceArray.Add(Vector2.Distance(characterOffset, towers[i].transform.position));
        }

        for (int i = 0; i < hidingSpots.Length; i++)
        {
            hidingSpotsDistanceArray.Add(Vector2.Distance(characterOffset, hidingSpots[i].transform.position));
        }

        //On trouve si l'élément le plus rapproché est une tour ou un lieu de sûreté
        float closestTowerPosition = towerDistanceArray.Min();
        float closestHidingSpotPosition = hidingSpotsDistanceArray.Min();

        //on définit le point le plus proche
        if (closestHidingSpotPosition < closestTowerPosition)
        {
            int hidingSpotIndex = hidingSpotsDistanceArray.IndexOf(closestHidingSpotPosition);
            hidingSpot = hidingSpots[hidingSpotIndex];
            isATower = false;
        }
        else
        {
            int hidingSpotIndex = towerDistanceArray.IndexOf(closestTowerPosition);
            hidingSpot = towers[hidingSpotIndex];
            isATower = true;
        }
    }

    private void GetAllHidingSpots()
    {
        if (this.tag == "IceKnight")
        {
            towers = GameObject.FindGameObjectsWithTag("IceTower");
            hidingSpots = GameManager.instance.GetIceHidingSpots();
        }
        else
        {
            towers = GameObject.FindGameObjectsWithTag("MagmaTower");
            hidingSpots = GameManager.instance.GetMagmaHidingSpots();
        }
    }
}