using UnityEngine;
using System.Collections;



public class ShrinkAndExplode : MonoBehaviour

{

    private float shrinkSpeed = 3f; // How fast the object shrinks
    public AudioClip destroyBrick;

    public ParticleSystem explosionPrefab; // The explosion particle system

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            
            DestroyWithExplosion();
            GameManager.Instance.UpdateScore(1);
        }
    }


    public void DestroyWithExplosion()

    {

        StartCoroutine(ShrinkAndExplodeCoroutine());

    }



    IEnumerator ShrinkAndExplodeCoroutine()

    {

        // Start shrinking animation

        while (transform.localScale.x > 0.1f) // Adjust the threshold as needed

        {

            transform.localScale -= new Vector3(shrinkSpeed * Time.deltaTime, shrinkSpeed * Time.deltaTime, shrinkSpeed * Time.deltaTime);

            yield return null;

        }



        // Play explosion particle effect

        Instantiate(explosionPrefab, transform.position, transform.rotation);
        GameManager.Instance.ScreenShake();



        // Destroy the object

        Destroy(gameObject);
        GameManager.Instance.CheckForPowerupSpawn(transform.position);
        AudioManager.Instance.Play(destroyBrick);

    }

}
