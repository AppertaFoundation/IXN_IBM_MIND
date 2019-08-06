using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class as an Object-> passed into Dilague Manager
//HOsts all Information about a single Dialogue

[System.Serializable]
public class Dialogue {
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
   
}
