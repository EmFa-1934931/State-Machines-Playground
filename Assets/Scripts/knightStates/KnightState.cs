using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KnightState : MonoBehaviour
{
    protected KnightManager knightManager;
    protected float speed;
    protected GameObject currentTarget;
    protected GameObject currentTower;
    // Start is called before the first frame update
    private void Start()
    {
    }

    void Awake()
    {
        knightManager = GetComponent<KnightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void MoveKnight();
    public abstract void ManageStateChange();

    public GameObject GetCurrentTarget()
    {
        if (this.currentTarget != null && this.currentTarget.activeSelf != false)
        {
            return this.currentTarget;
        }

        return null;
    }

    public void FindRandomTower(GameObject[] towerArray)
    {
        currentTower = towerArray[Random.Range(0, towerArray.Length)];
    }

    public GameObject getCurrentTower()
    {
        return this.currentTower;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
