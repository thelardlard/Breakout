using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 7f;
    private float baseXRange = 7.1f; // Base boundary range without paddle size consideration
    private float xRange; // Updated range with paddle size consideration

    void Start()
    {
        // Initialize xRange based on the paddle size
        xRange = baseXRange;
    }

    void Update()
    {
        // Update the xRange to reflect the current paddle size
        xRange = baseXRange - (transform.localScale.x / 2); // Half the paddle width will be deducted from xRange
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
        // Check for left and right bounds based on the updated xRange
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