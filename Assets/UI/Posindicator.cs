using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posindicator : MonoBehaviour
{

    public GameObject player;
    public GameObject ball;

    private Transform balltrans;
    private Transform playertrans;
    private Vector3 ballpos;
    private Vector3 playerpos;
    private GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        balltrans = ball.GetComponent<Transform>();
        playertrans = player.GetComponent<Transform>();
        indicator = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        ballpos = Camera.main.WorldToScreenPoint(balltrans.position);
        playerpos = Camera.main.WorldToScreenPoint(playertrans.position);
        

        if (ballpos.x < Screen.width
            && ballpos.y < Screen.height
            && ballpos.x > 0 && ballpos.y > 0)
        {
            indicator.SetActive(false);
            
        }

        else
        {
            indicator.SetActive(true);

            OnLinearAlgebra(playerpos, ballpos);
        }


    }


    Vector3 target;

    Vector2 la = new Vector2(viewLerp, 50);
    Vector2 lb = new Vector2(viewLerp, Screen.height - 50f);
    Vector2 ra = new Vector2(Screen.width - viewLerp, 50);
    Vector2 rb = new Vector2(Screen.width - viewLerp, Screen.height - 50f);
    static float viewLerp = Screen.width * 0.12f;

   
    
    
    bool GetPoint(Vector2 pos1, Vector2 pos2, Vector2 pos3, Vector2 pos4)
    {
        
        float a = pos2.y - pos1.y;
        float b = pos1.x - pos2.x;
        float c = pos2.x * pos1.y - pos1.x * pos2.y;
        float d = pos4.y - pos3.y;
        float e = pos3.x - pos4.x;
        float f = pos4.x * pos3.y - pos3.x * pos4.y;
        float x = (f * b - c * e) / (a * e - d * b);
        float y = (c * d - f * a) / (a * e - d * b);

        if (x < 0 || y < 0 || x > Screen.width || y > Screen.height) return false;
        if (!GetOnLine(pos1, pos2, new Vector2(x, y))) return false;
        target = new Vector3(x, y);

        return true;
    }


    void OnLinearAlgebra(Vector3 pos1, Vector3 pos2)
    {

        Vector2 ua = lb;//左上
        Vector2 ub = rb;//右上
        Vector2 da = la;//左下
        Vector2 db = ra;//右下

        if (GetPoint(pos1, pos2, la, lb)) gameObject.transform.position = target;
        else if (GetPoint(pos1, pos2, ra, rb)) gameObject.transform.position = target;
        else if (GetPoint(pos1, pos2, ua, ub)) gameObject.transform.position = target;
        else if (GetPoint(pos1, pos2, da, db)) gameObject.transform.position = target;


    }

    bool GetOnLine(Vector2 pos1, Vector2 pos2, Vector2 pos3)
    {
        float EPS = 1e-3f;
        float d1 = Vector2.Distance(pos1, pos3);
        float d2 = Vector2.Distance(pos2, pos3);
        float d3 = Vector2.Distance(pos1, pos2);
        if (Mathf.Abs(d1 + d2 - d3) <= EPS)
            return true;
        else
            return false;
    }
}
