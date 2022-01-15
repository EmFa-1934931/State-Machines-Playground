using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrows : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject arrow;

    private static int arrowAmount = 5;
    private float arrowTimer = 0;
    private GameObject[] arrowArray = new GameObject[arrowAmount];
    private int arrowForce = 100;

    private KnightState state;

    void Awake()
    {
        for (int i = 0; i < arrowAmount; i++)
        {
            arrowArray[i] = Instantiate(arrow);
            arrowArray[i].SetActive(false);
        }

        arrowTimer = 1;
    }

    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {

        state = this.gameObject.GetComponent<KnightState>();
        
        if (arrowTimer > 0)
        {
            arrowTimer -= Time.deltaTime;
        }
        else
        {
            if (state.GetSpeed() == 0)
            {
                Fire();
            }

            arrowTimer = 1;
        }

    }

    public void Fire()
    {
        Vector2 arrowDir;

        for (int i = 0; i < arrowAmount; i++)
        {
            if (!arrowArray[i].activeSelf)
            {
                GameObject currentTarget = gameObject.GetComponent<KnightState>().GetCurrentTarget();

                if (currentTarget != null)
                {
                    arrowArray[i].transform.position = GetComponent<Rigidbody2D>().transform.position;
                    arrowArray[i].SetActive(true);

                    arrowArray[i].GetComponent<ArrowManager>().setAttacker(this.gameObject);
                    arrowArray[i].GetComponent<ArrowManager>().setTarget(currentTarget);

                    //Pour la rotation : https://www.reddit.com/r/Unity2D/comments/42et55/help_how_do_i_rotate_a_bullet_direction/
                    Vector3 dir = currentTarget.transform.position - arrowArray[i].transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    arrowArray[i].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    arrowDir = currentTarget.transform.position - arrowArray[i].transform.position;
                    arrowDir = arrowDir.normalized;

                    arrowArray[i].GetComponent<Rigidbody2D>().AddForce(arrowDir * arrowForce);
                }
                return;
            }
        }
    }
}