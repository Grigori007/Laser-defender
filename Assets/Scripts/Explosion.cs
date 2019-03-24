using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    public void DestroyObject()
    {
        StartCoroutine(WaitBeforeObjDstr());
        Destroy(gameObject);
    }

    IEnumerator WaitBeforeObjDstr()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
