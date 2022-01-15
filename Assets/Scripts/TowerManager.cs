using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private LifeManager lifemanager;
    // Start is called before the first frame update
    void Start()
    {
        lifemanager = GetComponent<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lifemanager.IsDead())
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("IceArrow") && gameObject.tag == "MagmaTower" || collision.name.Contains("MagmaArrow") && gameObject.tag == "IceTower")
        {
            lifemanager.LoseALife();
            collision.gameObject.SetActive(false);
        }
    }
}

