using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightStateSafe : KnightState
{

    private LifeManager lifeManager;
    private float lifeTimer = 0;
    private bool towerIsDead = false;

    public override void ManageStateChange()
    {
        if (lifeManager.GetLives() == lifeManager.GetInitialLives() || towerIsDead)
        {
            Debug.Log("State change: Normal");
            GetComponent<KnightManager>().ChangeKnightState(KnightManager.KnightStateToSwitch.Normal);
        }
    }

    public override void MoveKnight()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //speed = 0f;
        lifeManager = GetComponent<LifeManager>();
    }

    private void Awake()
    {
        lifeTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameIsOver)
        {
            if (lifeTimer > 0)
            {
                lifeTimer -= Time.deltaTime;
            }
            else { 
                lifeManager.GetNewLife();
                lifeTimer = 1;
                ManageStateChange();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "IceTower")
        {
            towerIsDead = true;
        }
    }
}
