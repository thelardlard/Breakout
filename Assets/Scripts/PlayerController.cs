using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 7f;
    private float xRange = 7.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckBoundary();
        MovePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Powerup powerup = other.GetComponent<Powerup>();
            if (powerup != null)
            {
                GameManager.Instance.ApplyPowerup(powerup.currentPowerupType);
                Destroy(other.gameObject); // Remove the power-up
            }
        }
    }

    void CheckBoundary()
    {
        // Check for left and right bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }
void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.up * horizontalInput * Time.deltaTime * speed);
    }
}
