using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int initialLives = 10;
    [SerializeField] int lives;

    public bool isProtected;

    private void Update()
    {
        if (IsDead())
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GenerateRandomLife();
        lives = initialLives;
    }

    public void LoseALife()
    {
        lives--;
    }

    public void LoseLives(int lifeAmount)
    {
        if (isProtected)
        {
            lives--;
        }
        else
        {
            lives -= lifeAmount;
        }
    }

    public void GetNewLife()
    {
       if(lives < initialLives)
            lives++;
    }

    public bool IsDead()
    {
        return (lives <= 0);
    }

    private void GenerateRandomLife()
    {
        initialLives += Random.Range((initialLives / 2) * -1, (initialLives / 2));
    }

    public bool HasToFlee()
    {
        if(lives < 25 * initialLives / 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetLives()
    {
        return this.lives;
    }

    public int GetInitialLives()
    {
        return this.initialLives;
    }
}