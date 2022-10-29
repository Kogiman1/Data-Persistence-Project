using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.IO;
using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuUI : MonoBehaviour
{
  //  public static string nameChosen;
    public TMP_InputField nameText;



    // Start is called before the first frame update
    private void Start()
    {
        //( later add the last name used with json... in starting the scene also work with back to menu from settings..

       
    }




    public void StartGame()
    {

        KeepNameForScene(1);
        
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else

        Application.Quit();

#endif
    }


    public void OpenSetting()
    {
        KeepNameForScene(2);
    }

   

    public void KeepNameForScene(int index)
    {
        if (nameText.text == "Enter Name Here")//if name still "enter name".,change to mystery
        {
            nameText.text = "Mystery Player";
        }
        PlayerName.instance.nameChosen = nameText.text;//store the chosen name when starting next scene;
        SceneManager.LoadScene(index);
    }

    
}
