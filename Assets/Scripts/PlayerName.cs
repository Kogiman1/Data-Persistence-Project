using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviour
{

    public static PlayerName instance;
    public string nameChosen;
    public Material blue;// index 0
    public Material yellow; //index 1
    public int colorIndex; //saves the index chosen from settingsUI 
    

    private void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
      
    }


}
