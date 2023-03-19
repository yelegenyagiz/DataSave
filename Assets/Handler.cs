using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Handler : MonoBehaviour
{

    private void Start()
    {
        SaveLoad.Write<int>("My age", 24);
    }

}


