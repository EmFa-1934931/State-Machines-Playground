using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject[] iceTowers = new GameObject[3];
    [SerializeField] GameObject[] magmaTowers = new GameObject[3];
    [SerializeField] GameObject[] iceHidingSpots = new GameObject[2];
    [SerializeField] GameObject[] magmaHidingSpots = new GameObject[2];
    public bool gameIsOver = false;

    [SerializeField] Text[] uiText = new Text[3];

    private int iceKnightCount = 0;
    private int magmaKnightCount = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown("enter"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (GetMagmaTowers().Length <= 0 || GetIceTowers().Length <= 0)
        {
            gameIsOver = true;
            
            if(GetMagmaTowers().Length <= 0)
            {
                //uiText[2].text = "Camp Magma gagnant";
            }
            else
            {
                //uiText[2].text = "Camp Glace gagnant";
            }

           // GameObject.Find("EndGametext").SetActive(true);
        }
    }

    public GameObject[] GetIceTowers()
    {
        iceTowers = GameObject.FindGameObjectsWithTag("IceTower");
        return iceTowers;
    }

    public GameObject[] GetMagmaTowers()
    {
        magmaTowers = GameObject.FindGameObjectsWithTag("MagmaTower");
        return magmaTowers;
    }

    public GameObject[] GetIceHidingSpots()
    {
        return iceHidingSpots;
    }

    public GameObject[] GetMagmaHidingSpots()
    {
        return magmaHidingSpots;
    }

    // --- Modification de l'UI ---
    public void AddIceKnight()
    {
        //iceKnightCount++;
        //uiText[1].text = iceKnightCount.ToString();
    }

    public void RemoveIceKnight()
    {
        //iceKnightCount--;
        //uiText[1].text = iceKnightCount.ToString();
    }

    public void AddMagmaKnight()
    {
        //magmaKnightCount++;
        //uiText[0].text = magmaKnightCount.ToString();
    }

    public void RemoveMagmaKnight()
    {
       // magmaKnightCount--;
        //uiText[0].text = magmaKnightCount.ToString();
    }
}
