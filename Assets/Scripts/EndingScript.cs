using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RocketGameLevelManager;
public class EndingScript : MonoBehaviour
{

    private float _finalTime;
    // Start is called before the first frame update
    void Start()
    {
        _finalTime = GameLoader.GetPreservedTime();
        Debug.Log("Final time: " + (_finalTime - 2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
