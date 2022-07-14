using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5;
    public float shootForce = 5f;
    public bool isTurn = false;
    public bool isAttack = false;
    public GameObject ball;
    private Transform myTransform;
    private Rigidbody2D myRigidbody;
    public float angle = 30;
    float axisZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = gameObject.GetComponent<Transform>();
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axisZ = transform.localEulerAngles.z;
        if (axisZ > 180)
        {
            axisZ -= 360;
        }
        float movement = Input.GetAxisRaw("Horizontal");
        float upsideDown = Input.GetAxisRaw("Vertical");
        transform.Translate(-movement * moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(0, upsideDown * moveSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.E) && axisZ <= angle)
        {
            myTransform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q) && axisZ >= -angle)
        {
            myTransform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
            //Debug.Log(axisZ);
        }




        if (!isTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ball.GetComponent<Rigidbody2D>().AddForce(Vector2.up * shootForce, ForceMode2D.Impulse);
                isTurn = true;
                transform.DetachChildren();
            }
        }

    }
    public void isHit(int time)
    {
        moveSpeed = 0;
    }
}