using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {

        }

        bool left = Input.GetKey(KeyCode.Q);
        bool right = Input.GetKey(KeyCode.D);
        if(left ^ right)
        {
            if(left)
            {

            }

            if(right)
            {

            }
        }
    }
}
