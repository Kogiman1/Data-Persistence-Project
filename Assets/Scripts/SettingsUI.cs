using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class SettingsUI : MonoBehaviour
{
   
   
  

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }





    //JSON stuff

    [System.Serializable]
    class SaveData
    {
        public int highScore1;
        public string playerName1;

    }
    public void ResetHighScore()
    {
        SaveData data = new SaveData();
        data.highScore1 = 0;
        data.playerName1 = "";

        string json = JsonUtility.ToJson(data);

       File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log(Application.persistentDataPath + "/savefile.json");
    }

    //setting color picker:

    public void PickBlue()
    {
        PlayerName.instance.colorIndex = 1;
    }

    public void PickYellow()
    {
        PlayerName.instance.colorIndex = 2;
    }

}
