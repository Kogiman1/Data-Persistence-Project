using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class MainManager : MonoBehaviour
{
    private int highScore;
    private string highScoreName;
    private int lastScore;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    private Renderer ball_Renderer;

    public Text ScoreText;
    public Text HighScoreText;
    public TextMeshProUGUI NewHighScoreText;
    
    public GameObject GameOverText;
    public Button menuBottun;
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private string nameChosen = PlayerName.instance.nameChosen;

    
    // Start is called before the first frame update
    void Start()
    {

        LoadHighScore();
        ball_Renderer = GameObject.Find("Ball").GetComponent<Renderer>();              
        if (PlayerName.instance.colorIndex == 1)
        {
            ball_Renderer.material.color = Color.blue;
        }
        if (PlayerName.instance.colorIndex == 2)
        {
            ball_Renderer.material.color = Color.yellow;

        }

        highScore = HighScore.instance.highScore;  //initilize highscore from highscore object.
       highScoreName = HighScore.instance.highScoreName;
       
        HighScoreText.text = "Best Score: " + highScoreName + " : " + highScore;
        
        //add json for highscores.

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
       
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
       ScoreText.text = nameChosen + "'s Score :" +m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        menuBottun.gameObject.SetActive(true);

        lastScore = m_Points; 
        highScore= MaxScore(lastScore);  //high score is set to the higher score between the last high score and current score.

        //after game messages:
        Debug.Log("latest score:"+ lastScore);
        Debug.Log("highest Score:"+highScore);
        Debug.Log("Best Score Name: " + HighScore.instance.highScoreName);

        SaveHighScore();

                    //if score> high score save the score

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public int MaxScore(int lastScore)
    {
        if (lastScore > highScore)
        {
            highScore = lastScore;
            HighScore.instance.highScore = lastScore;// update high score static to lastest score.
            HighScore.instance.highScoreName = PlayerName.instance.nameChosen;
            NewHighScoreText.gameObject.SetActive(true);
            return highScore;
        }
        else
        {
            return highScore;
        }
    }


    //JSON stuff

    [System.Serializable]
    class SaveData
    {
        public int highScore1;
        public string playerName1;
        public int ballColorIndex1;

    }
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore1 = HighScore.instance.highScore;
        data.playerName1 = HighScore.instance.highScoreName;
        data.ballColorIndex1 = PlayerName.instance.colorIndex;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log(Application.persistentDataPath + "/savefile.json");
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore.instance.highScore = data.highScore1;
            HighScore.instance.highScoreName = data.playerName1;
          //  PlayerName.instance.colorIndex = data.ballColorIndex1;
            Debug.Log(path);
        }
    }

  


}
