using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    private float spawnRate = 1f;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    
    private float score;
    public bool isGameActive = false;
    
    public Button restartButton;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty) //takes the difficulty value from DiddicultyButton.cs by the method StartGame(difficulty)
    {
        isGameActive = true;
        score = 0;

        spawnRate /= difficulty; //calculates the spawn rate and then the spawn rate is Instantiating (Spawning objects) in that speed.

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
    }
}
