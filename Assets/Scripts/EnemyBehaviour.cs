using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 100f;
    public GameObject enemyProjectile;
    public float enemyProjectileSpeed = 100f;
    public int pointsForEnemy = 100;
    private AudioSource audioSource;
    private ScoreKeeper scoreKeeper;
    public float shotsPerSecond = 0.05f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update()
    {
        float chanceValue = Random.value;
        float chanceForShot = Time.deltaTime * shotsPerSecond;
        if (chanceValue < chanceForShot)
        {
            ShootToPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missle = collider.gameObject.GetComponent<Projectile>();
        
        if(missle)
        {
            health -= missle.GetDamage();
            missle.Hit();
            Debug.Log("Enemy hit by projectile!");
            if (health <= 0)
            {
                Destroy(gameObject);
                scoreKeeper.Score(pointsForEnemy);
            }
        }
    }

    void ShootToPlayer()
    {
        GameObject beam = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb = beam.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0, -enemyProjectileSpeed);
        audioSource.Play();
    }


}
