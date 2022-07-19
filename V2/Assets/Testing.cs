using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Timer timer = Timer.createTimer("Timer");
        timer.startTiming(10, OnComplete, OnProcess);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnComplete()
    {    
        Debug.Log("complete!");
        
    
    }
    void OnProcess(float p)
    {
        //Debug.Log("on process" + p);
    }

}
