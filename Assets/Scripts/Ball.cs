using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 7f;
    new private Rigidbody rigidbody;
    private Vector3 velocity;
    public AudioClip bounceSound;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        FireBall();
    }

    public void FixedUpdate()
    {
        // Ensure the ball maintains its intended speed
        if (rigidbody.linearVelocity.magnitude < speed * 0.9f)
        {
            rigidbody.linearVelocity = velocity.normalized * speed;
        }
        else
        {
            velocity = rigidbody.linearVelocity; // Track actual physics velocity
        }

        Debug.DrawRay(transform.position, rigidbody.linearVelocity, Color.green);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Vector3 averageNormal = Vector3.zero;

        foreach (var contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red, 10);
            averageNormal += contact.normal;
        }

        averageNormal.Normalize(); // Get the overall normal direction

        // Use Unity's built-in reflection function
        Vector3 newDirection = Vector3.Reflect(velocity.normalized, averageNormal);

        // Prevent near-horizontal or vertical movement
        if (Mathf.Abs(newDirection.y) < 0.3f)
        {
            newDirection.y = (newDirection.y >= 0) ? 0.3f : -0.3f;
        }
        if (Mathf.Abs(newDirection.x) < 0.3f)
        {
            newDirection.x = (newDirection.x >= 0) ? 0.3f : -0.3f;
        }

        velocity = newDirection.normalized * speed;
        rigidbody.linearVelocity = velocity;

        AudioManager.Instance.Play(bounceSound);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
            GameManager.Instance.UpdateLives(1);
        }
    }

    public void FireBall()
    {
        Vector3 direction = new Vector3(0.3f, 1, 0).normalized;
        velocity = direction * speed;
        rigidbody.linearVelocity = velocity;
    }
}