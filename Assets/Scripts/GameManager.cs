using UnityEngine;
using System.Collections;
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
    public Camera mainCamera;
    public GameObject ballPrefab;
    public GameObject paddle;
    public GameObject brickPrefab;
    public GameObject grid;
    public GameObject powerupPrefab;
    public AudioClip destroyBrick;
    public AudioClip loseLife;
    private Vector3 ballSpawnPosition = new(0,0.6f,0);
    private Vector3 paddleSpawnPosition = new(0, 0, 0);
    private Vector3 paddleSpawnScale;
    public AnimationCurve shakeCurve;
    public float shakeDuration = 0.5f;
    private float paddleScale = 1.5f;
   

    void Awake()
    {
        _instance = this; // Set the current instance as the singleton
                paddleSpawnScale = paddle.transform.localScale;
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
            Debug.Log("ResetPaddle() should be called next...");
            ResetPaddle();
            RespawnBall();
            
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
        Debug.Log("ResetPaddle called.");
        Debug.Log("Paddle Position Before Reset: " + paddle.transform.position);
        Debug.Log("Paddle Scale Before Reset: " + paddle.transform.localScale);

        paddle.transform.position = paddleSpawnPosition;
        paddle.transform.localScale = paddleSpawnScale;

        Debug.Log("Paddle Position After Reset: " + paddle.transform.position);
        Debug.Log("Paddle Scale After Reset: " + paddle.transform.localScale);
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
        grid.GetComponent<BrickGrid>().SpawnBricks();
        gameLevel++;
        uiManager.UpdateLevelText();
        DestroyAllLiveBalls();
        ResetPaddle();
        RespawnBall();
        
    }

    //method to spawn a powerup object
    public void CheckForPowerupSpawn(Vector3 spawnPosition)
    {
        if (Random.Range(0, 9)  == 0)
        {
            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
        }

    }

    //method to apply a powerup via collision with paddle
    public void ApplyPowerup(Powerup.PowerupType type)
    {
        switch (type)
        {
            case Powerup.PowerupType.IncreasePaddle:
                Debug.Log("Increase Paddle Size");
                IncreasePaddleSize();
                // Apply paddle size increase here
                break;

            case Powerup.PowerupType.Multiball:
                Debug.Log("Activate Multiball");
                // Spawn multiple balls                
                break;

            case Powerup.PowerupType.Guns:
                Debug.Log("Activate Guns");
                // Enable paddle shooting
                break;
        }
    }
    
    public void IncreasePaddleSize()
    {
        //Increase paddle size
        if (paddle.transform.localScale.y <= paddleSpawnScale.y)
        {
            paddle.transform.localScale += new Vector3(0,paddleScale,0);
            Debug.Log("Increase Paddle Size");
        }
    }



    public void ScreenShake()
    {
        StartCoroutine(Shaking());
    }

    public IEnumerator Shaking()
    {
        Vector3 startPosition = mainCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = shakeCurve.Evaluate(elapsedTime / shakeDuration);
            mainCamera.transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}