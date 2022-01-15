using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    private GameObject attacker;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAttacker(GameObject attacker)
    {
        this.attacker = attacker;
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    public GameObject getAttacker()
    {
        return attacker;
    }

    public GameObject getTarget()
    {
        return target;
    }
}
