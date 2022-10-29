using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;
    public int highScore;
    public string highScoreName;



    private void Awake()
    {
       // highScore = 0; // for start initiliaze the highscore to 0. later CHANGE TO JSON.
        if(instance!= null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        
    }



}



