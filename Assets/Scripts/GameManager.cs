using UnityEngine;
public class GameManager : MonoBehaviour

{

    private static GameManager _instance; // Private static variable to hold the single instance



    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>(); // Find the Game Manager if not set
            }
            return _instance;
        }
    }

    public int userScore = 0;
    public int userLives = 3;
    public int gameLevel = 1;
    public UIManager uiManager;
    public GameObject ballPrefab;
    public GameObject paddle;
    public GameObject brickPrefab;
    public GameObject grid;
    public AudioClip destroyBrick;
    public AudioClip loseLife;
    private Vector3 ballSpawnPosition = new(0,0.6f,0);
    private Vector3 paddleSpawnPosition = new(0, 0, 0);

    void Awake()
    {
        _instance = this; // Set the current instance as the singleton
    }

    private void Update()
    {
        //check for level complete
        int bricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        if (bricks == 0)
        {
            LevelComplete();            
        }
    }
    
    public void UpdateScore(int points)

    {
        // Logic to update the score
        
        userScore += points;
        uiManager.UpdateScoreText();
    }

    public void UpdateLives(int lives)
    {
        userLives -= lives;
        uiManager.UpdateLivesText();
        AudioManager.Instance.Play(loseLife);
        if (userLives > 0)
        {
            //restart from the paddle position
            RespawnBall();
            ResetPaddle();
        }
        else
        {
            Debug.Log("Game Over!");
            //Add game over screen and code
        }
        
    }

    public void RespawnBall()
    {
        Instantiate(ballPrefab, ballSpawnPosition, Quaternion.identity);
        ballPrefab.GetComponent<Ball>().FireBall();
    }

    public void ResetPaddle()
    {
        paddle.transform.position = paddleSpawnPosition;
    }

    public void DestroyAllLiveBalls()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }
    }

    public void LevelComplete()
    {
        gameLevel++;
        uiManager.UpdateLevelText();
        DestroyAllLiveBalls();
        ResetPaddle();
        RespawnBall();
        grid.GetComponent<BrickGrid>().SpawnBricks();
    }


}