using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Sprite[] spriteArray;
    public float damage = 100f;
    public GameObject explosion;
    private SpriteRenderer spriteRenderer;
    private bool spriteChanged = false;
    private AudioSource audioSource;
    private float lol;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChangeSprite();
    }

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Explode();
        Debug.Log("There was an explosion!");
        if (audioSource)
        {
            audioSource.Stop();
            Debug.Log("Audio stopped.");
        }
        Debug.Log("About to destroy the rocket.");
        StartCoroutine(DisableComponents());
        if (gameObject.tag != "moab")
        {
            Destroy(gameObject);
        }
        Debug.Log("Rocket destroyed.");
    }

    void ChangeSprite()
    {
        if (!spriteChanged)
        {
            int NextArrayPos = Mathf.RoundToInt(Random.Range(0, spriteArray.Length - 0.6f)); //0.6 bo rozmiar tablicy wynosi 4, 3,4 zaokraglone do int daje 3 wiec sprite na pozycji [3] bedzie takze uzywany   
            spriteRenderer.sprite = spriteArray[NextArrayPos];
            spriteChanged = true;
        }
    }

    private void Explode()
    {
        if (gameObject.tag == "moab")
        {
            Debug.Log("About to instantiate explosion.");
            Instantiate(explosion, transform.position, Quaternion.identity, gameObject.transform);
            Debug.Log("Explosion has been instantiated.");
        }
    }

    private IEnumerator DisableComponents()
    {
        if (gameObject.tag == "moab")
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
            ParticleSystem ps = GetComponent<ParticleSystem>();
            spriteRenderer.enabled = false;
            boxCol.enabled = false;
            ps.Stop();
            yield return new WaitForSeconds(1.5f);
            boxCol.enabled = true;
        }

    }
}
