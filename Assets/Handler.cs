using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Handler : MonoBehaviour
{

    private void Start()
    {
        SaveLoad.Write<int>("My age", 24);
        SaveLoad.Write<Vector3>("position", new Vector3(20, 30, 40));
        SaveLoad.Write<bool>("Am i a good person?", false);
        SaveLoad.Write<string>("My surname", "YELEGEN");
        SaveLoad.Write<float>("pi", 3.1415f);

        var v1 = SaveLoad.Read<int>("My age");
        var v2 = SaveLoad.Read<Vector3>("position");
        var v3 = SaveLoad.Read<System.Boolean>("Am i good person?");
        var v4 = SaveLoad.Read("My surname");

    }

}


