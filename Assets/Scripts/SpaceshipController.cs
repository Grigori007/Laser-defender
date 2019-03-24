using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float PlayersHealth = 150f;
    public float SpaceshipSpeed;
    public float projectileSpeed;
    public float rocketSpeed;
    public GameObject projectile;
    public GameObject rocket;
    public float rateOfFire = 0.3f;
    private ScoreKeeper scoreKeeper;
    private AudioSource audioSource;
    private float Xmin, Xmax;
    private float padding;

    private void Start()
    {
        padding = Screen.width / 19.2f;
        float distance = gameObject.transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Xmin = leftmost.x + padding;
        Xmax = rightmost.x - padding;
        audioSource = GetComponent<AudioSource>();
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();

    }

    private void FixedUpdate()
    {
        ArrowsControl();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("ShootLaser", 0.0001f, rateOfFire);

        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke();
        } else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            LaunchRocket();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missle = collider.GetComponent<Projectile>();
        if (missle)
        {
            Debug.Log("We were hit by the enemy!");
            PlayersHealth -= missle.GetDamage();
            missle.Hit();
            if (PlayersHealth <= 0)
            {
                Die();
            }
        }
    }


    private void MouseControl()
    {
        float mouseXAxis = Input.mousePosition.x;
        mouseXAxis = Mathf.Clamp(mouseXAxis, 50.0f, 1870.0f);
        Vector2 mousePos = new Vector2(mouseXAxis, gameObject.transform.position.y);
        gameObject.transform.position = mousePos;
        print(mousePos);
    }

    private void ArrowsControl()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-SpaceshipSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(SpaceshipSpeed, 0, 0);
        }

        float ShipPosX = Mathf.Clamp(gameObject.transform.position.x, Xmin, Xmax);
        Vector2 ShipPos = new Vector2(ShipPosX, gameObject.transform.position.y);
        gameObject.transform.position = ShipPos;
    }

    private void ShootLaser()
    {
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb1 = beam.GetComponent<Rigidbody2D>();
        rb1.velocity = new Vector3(0, projectileSpeed);
        audioSource.Play();
    }

    private void LaunchRocket()
    {
        GameObject rocketProjectile = Instantiate(rocket, transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb2 = rocketProjectile.GetComponent<Rigidbody2D>();
        rb2.velocity = new Vector3(0, rocketSpeed);
    }

    private void Die()
    {
        Destroy(gameObject);
        GameManager gm = FindObjectOfType<GameManager>();
        gm.ChangeLevel("End");
    }



}