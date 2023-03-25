using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoopMap : MonoBehaviour
{

    // Start is called before the first frame update
    public MapReader mapreader;
    void Start()
    {
        //mapの読み込み
        mapreader.ImportMap();
        print("MAP LOAD DONE");

        //
        
        //

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

