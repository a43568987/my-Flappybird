using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour {

    public Bird birdProperty;
    private GameObject bird;
    private Rigidbody birdRig;
    public GameObject wall;
    public GameObject[] walls;
    private bool restart = false;
    bool gameStart = false;
    public static int highestScore = 0;
    InputManager input;


    bool isGameOver = false;
    int myscore = 0;

    public Text score;
    public Text over;

	// Use this for initialization
	void Start () {
        bird = GameObject.Find("bird");
        birdRig = bird.GetComponent<Rigidbody>();
        birdRig.useGravity = false;
        InvokeRepeating("creatWall", 0f, 1.1f);

        score.gameObject.SetActive(false);
        over.gameObject.SetActive(false);

        input = new InputManager();
        
    }
	
	// Update is called once per frame
	void Update () {
		if(gameStart == false && Input.GetMouseButtonDown(0))
        {
            gameStart = true;
        }

        if (gameStart)
        {
            birdRig.useGravity = true;
            score.gameObject.SetActive(true);
        }

        input.OnUpdate();
        birdProperty.OnUpdate();
        walls = GameObject.FindGameObjectsWithTag("moveWall");
        for(int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<Wall>().OnUpdate();
        }

        isGameOver = birdProperty.isGameOver;
        myscore = birdProperty.score;

        if (isGameOver)
        {
            gameover();
        }
        if(restart && Input.GetMouseButtonDown(0))
        {
            InputManager.mark = 0;
            SceneManager.LoadScene(0);
        }

        score.text = myscore.ToString();
	}

    void creatWall()
    {
        if(gameStart && isGameOver == false)
        {
            Debug.Log(gameStart);
            Instantiate(wall, new Vector3(14f + Random.Range(-0.5f, 0.5f), Random.Range(-1.82f, 1.82f), 0), Quaternion.identity);
        }
    }

    void gameover()
    {
        if(myscore > highestScore)
        {
            highestScore = myscore;
            fresh();
        }
        getHigh();
        over.gameObject.SetActive(true);
        over.text = "Game Over\n\nYour Score:" + myscore.ToString() + "\n\nBest Score:" + highestScore.ToString();
        Invoke("Restart", 2);
    }
    void Restart()
    {
        restart = true;
    }

    void fresh()
    {
        FileStream fs = new FileStream(Application.dataPath + "/save.txt", FileMode.Create);
        byte[] bytes = new UTF8Encoding().GetBytes(highestScore.ToString());
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
    }

    void getHigh()
    {
        FileStream fs_r = new FileStream(Application.dataPath + "/save.txt", FileMode.Open);
        byte[] bytes_r = new byte[10];
        fs_r.Read(bytes_r, 0, bytes_r.Length);
        string s = new UTF8Encoding().GetString(bytes_r);
        highestScore = System.Int32.Parse(s);
        fs_r.Close();
    }

}
