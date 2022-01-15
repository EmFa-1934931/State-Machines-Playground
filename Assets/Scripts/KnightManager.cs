using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightManager : MonoBehaviour
{

    public enum KnightStateToSwitch { Normal, Brave, Flee, Hide, Safe }
    private KnightState knightState;
    private LifeManager lifeManager;
    private GameObject enemy;
    private int enemiesKilled = 0;

    // Start is called before the first frame update
    void Awake()
    {
        knightState = GetComponent<KnightState>();
        lifeManager = GetComponent<LifeManager>();
    }

    public void ChangeKnightState(KnightStateToSwitch nextState)
    {
        Destroy(knightState);

        switch (nextState)
        {
            case KnightStateToSwitch.Normal:
                {
                    knightState = gameObject.AddComponent<KnightStateNormal>() as KnightStateNormal;
                    break;
                }
            case KnightStateToSwitch.Brave:
                {
                    knightState = gameObject.AddComponent<KnightStateBrave>() as KnightStateBrave;
                    break;
                }
            case KnightStateToSwitch.Flee:
                {
                    knightState = gameObject.AddComponent<KnightStateFlee>() as KnightStateFlee;
                    break;
                }
            case KnightStateToSwitch.Safe:
                {
                    knightState = gameObject.AddComponent<KnightStateSafe>() as KnightStateSafe;
                    break;
                }
            case KnightStateToSwitch.Hide:
                {
                    knightState = gameObject.AddComponent<KnightStateHide>() as KnightStateHide;
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnDisable()
    {
        if (name.Contains("MagmaKnight"))
        {
            GameManager.instance.RemoveMagmaKnight();
        }
        else
        {
            GameManager.instance.RemoveIceKnight();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (canAttack) { 
            if (collision.name.Contains("IceArrow") && gameObject.tag == "MagmaKnight" || collision.name.Contains("MagmaArrow") && gameObject.tag == "IceKnight")
            {
                if (Vector2.Distance(collision.gameObject.transform.position, transform.position) <= 0.5)
                {
                    int damage = Random.Range(1, 5);
                    lifeManager.LoseLives(damage);
                    collision.gameObject.SetActive(false);

                    enemy = collision.GetComponent<ArrowManager>().getAttacker();

                    if (this.lifeManager.IsDead())
                    {
                        enemy.GetComponent<KnightManager>().AddEnemyKilled();
                    }
                }
            }
        //}
    }

    public void AddEnemyKilled()
    {
        this.enemiesKilled++;
    }

    public int GetNbEnemiesKilled()
    {
        return this.enemiesKilled;
    }

    public GameObject GetEnemy()
    {
        if(enemy != null && enemy.activeSelf)
        {
            return enemy;
        }
        else
        {
            return null;
        }
    }
}
