using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public float maxX;
    public Transform spawnPoint;
    public float spawnRate;
    public GameObject gameOverUI;
    public TextMeshProUGUI fscore;
    public TextMeshProUGUI hscore;
    public TextMeshProUGUI hiscore;

    public GameObject bgMusic;



    bool gameStarted = false;

    public GameObject tapText;
    public TextMeshProUGUI scoreText;

    int score = 0;

    int final = 0;

    int high = 0;



    private void Start()
    {
        gameOverUI.SetActive(false);
        if (PlayerPrefs.HasKey("High"))
        {
            high = PlayerPrefs.GetInt("High");
            hscore.text = $"All time High : {high.ToSafeString()}";
            hiscore.text = $"Highest Score : {high.ToSafeString()}";

        }

        hiscore.GetComponent<TextMeshProUGUI>().enabled = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted){
            StartSpawning();
            gameStarted = true;
            tapText.SetActive(false);
            hiscore.GetComponent<TextMeshProUGUI>().enabled = true;

        }
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnBlock", 0.5f, spawnRate);
    }

    private void SpawnBlock()
    {
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);
        Instantiate(block, spawnPos, Quaternion.identity);

        score++;
        scoreText.text = score.ToSafeString();

        final = score;


    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        fscore.text = final.ToSafeString() ;
        Time.timeScale = 0;
        if (final > high)
        {
            high = final ;
            hscore.text = $"All time High : {high.ToSafeString()}";
            hiscore.text = $"Highest Score : {high.ToSafeString()}";
            PlayerPrefs.SetInt("High", high);
        }
        bgMusic.SetActive(false);



    }

    public void tryAgain()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void quit()
    {
        Application.Quit();
        PlayerPrefs.SetInt("High", 0);
    }


}
