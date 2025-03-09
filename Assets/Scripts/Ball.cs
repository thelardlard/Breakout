using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{

    public float speed = 7;

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
        // Maintain a constant speed.
        velocity = velocity.normalized * speed;
        rigidbody.linearVelocity = velocity;

        Debug.DrawRay(transform.position, rigidbody.linearVelocity, Color.green);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Vector3 d, n, r;

        foreach (var contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red, 10);

            d = velocity;
            n = contact.normal;
            r = d - (2 * Vector3.Dot(d, n) * n);

            // Check for corner hits
            if (Mathf.Abs(n.x) > 0.5f && Mathf.Abs(n.y) > 0.5f)
            {
                // Corner hit detected - force reflection to be more predictable
                if (Mathf.Abs(d.x) > Mathf.Abs(d.y))
                {
                    r.y = -d.y; // Invert Y
                    r.x *= 0.8f; // Slightly reduce X to avoid sticking to walls
                }
                else
                {
                    r.x = -d.x; // Invert X
                    r.y *= 0.8f; // Slightly reduce Y to avoid unnatural bounces
                }
            }

            velocity = r.normalized * speed;

            // Fix near-horizontal movement
            if (Mathf.Abs(velocity.y) < 0.3f)
            {
                velocity.y = (velocity.y >= 0) ? 0.3f : -0.3f; // Forces some vertical movement
                Debug.Log("Fixed Y!");
            }

            // Fix near-vertical movement
            if (Mathf.Abs(velocity.x) < 0.3f)
            {
                velocity.x = (velocity.x >= 0) ? 0.3f : -0.3f; // Forces some horizontal movement
                Debug.Log("Fixed X!");
            }

            velocity = velocity.normalized * speed; // Re-normalize after adjustments

            /* Prevent near-horizontal movement (keeps ball moving up/down)
            if (Mathf.Abs(velocity.y) < 1f)
            {
                velocity.y = (velocity.y >= 0) ? 1f : -1f; // Ensures a valid Y direction
                Debug.Log("Fixed Y!");
            }*/
        }                     
            AudioManager.Instance.Play(bounceSound);
        
    }
    public void OnTriggerEnter(Collider other)
    {
        
            //destroy the ball
            Destroy(gameObject);
           
            GameManager.Instance.UpdateLives(1);
            

        
    }
    public void FireBall()
    {
        Vector3 direction = new Vector3(0.3f, 1, 0);
        direction.Normalize();
        velocity = direction * speed;
        
    }
}
