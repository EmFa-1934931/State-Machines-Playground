using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightStateBrave : KnightState
{
    private GameObject[] enemyTowers;
    private GameObject[] hidingSpots;

    private LifeManager lifeManager;
    private float lifeTimer = 0;

    void Start()
    {
        speed = 3f;
        lifeManager = GetComponent<LifeManager>();
    }

    private void Awake()
    {
        if (this.tag == "IceKnight")
        {
            enemyTowers = GameObject.FindGameObjectsWithTag("MagmaTower");
            hidingSpots = GameManager.instance.GetIceHidingSpots();
        }
        else
        {
            enemyTowers = GameObject.FindGameObjectsWithTag("IceTower");
            hidingSpots = GameManager.instance.GetMagmaHidingSpots();
        }
        lifeTimer = 3;

        // À l'activation, le joueur se dirige automatiquement vers une des tours.
        FindRandomTower(enemyTowers);
    }
    public override void ManageStateChange()
    {
        if (lifeManager.HasToFlee())
        {
            Debug.Log("State change: Flee");
            GetComponent<KnightManager>().ChangeKnightState(KnightManager.KnightStateToSwitch.Flee);
        }
    }

    public override void MoveKnight()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTower.gameObject.transform.position, speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameIsOver)
        {
            MoveKnight();
            ManageStateChange();
            DetectEnemy();

            if (currentTower.activeSelf == false)
            {
                FindRandomTower(enemyTowers);
            }

            if ((currentTarget != null && currentTarget.activeSelf == false) || currentTarget == null)
            {
                speed = 3f;
            }

            if (lifeTimer > 0)
            {
                lifeTimer -= Time.deltaTime;
            }
            else
            {
                lifeManager.GetNewLife();
                lifeTimer = 3;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject == hidingSpots[0] || collision.gameObject == hidingSpots[1])
            && Vector2.Distance(collision.gameObject.transform.position, transform.position) <= 2)
        {
            speed = 2f;
            lifeManager.isProtected = true;
        }
        else if (collision.tag == enemyTowers[0].tag)
        {
            currentTarget = collision.gameObject;
            speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == hidingSpots[0] || collision.gameObject == hidingSpots[1])
        {
            speed = 3f;
            lifeManager.isProtected = false;
        }
    }

    private void DetectEnemy()
    {
        if(GetComponent<KnightManager>().GetEnemy() != null)
        {
            speed = 0;
            currentTarget = GetComponent<KnightManager>().GetEnemy();
        }
    }
}
