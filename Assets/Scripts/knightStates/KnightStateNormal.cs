using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightStateNormal : KnightState
{
    private GameObject[] enemyTowers;
    private GameObject[] hidingSpots;
    
    private LifeManager lifeManager;
    private float lifeTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
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

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameIsOver)
        {
            MoveKnight();
            ManageStateChange();

            if (currentTower.activeSelf == false)
            {
                FindRandomTower(enemyTowers);
            }

            if (currentTarget != null && currentTarget.activeSelf == false)
            {
                speed = 2f;
            }

            if (lifeTimer > 0)
            {
                lifeTimer -= Time.deltaTime;
            }
            else
            {
                if (speed > 0)
                {
                    lifeManager.GetNewLife();
                }
                lifeTimer = 3;
            }

        }
    }

    public override void MoveKnight()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTower.gameObject.transform.position, speed * Time.deltaTime);
    }
    public override void ManageStateChange()
    {
        if(GetComponent<KnightManager>().GetNbEnemiesKilled() == 3)
        {
            Debug.Log("State change: Brave");
            GetComponent<KnightManager>().ChangeKnightState(KnightManager.KnightStateToSwitch.Brave);
        }
        else if (lifeManager.HasToFlee())
        {
            Debug.Log("State change: Flee");
            GetComponent<KnightManager>().ChangeKnightState(KnightManager.KnightStateToSwitch.Flee);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject == hidingSpots[0] || collision.gameObject == hidingSpots[1])
            && Vector2.Distance(collision.gameObject.transform.position, transform.position) <= 2)
        {
            speed = 1f;
            lifeManager.isProtected = true;
            //Devront prendre moins de dommages
        }
        else if ((collision.name.Contains("MagmaKnight") || collision.name.Contains("IceKnight"))
            && collision.tag != tag)
        {
            speed = 0;
            currentTarget = collision.gameObject;
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
            speed = 2f;
            lifeManager.isProtected = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
    }
}