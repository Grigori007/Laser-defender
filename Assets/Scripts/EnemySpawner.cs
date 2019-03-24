using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 1000f;
    public float height = 400f;
    public float horizontalMoveSpeed = 125f;
    public float verticalMoveSpeed = 25f;
    public float timeBetweenRespawns = 0.5f;
    private bool FormGoingRight = true;
    private float xMin, xMax;

	void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMin = leftBoundary.x;
        xMax = rightBoundary.x;

        SpawnEnemies();
	}
	
	void Update () {
        MoveFormation();

        if (AllMembersDead())
        {
            SpawnUntilFull();
        }
	}

    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    private void MoveFormation() {

        float formationMaxPos = transform.position.x + (width / 2);
        float formationMinPos = transform.position.x - (width / 2);

        if (formationMinPos <= xMin)
        {
            FormGoingRight = true;
        } else if (formationMaxPos >= xMax)
        {
            FormGoingRight = false;
        }

        if (FormGoingRight)
        {
            transform.position += Vector3.right * horizontalMoveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * horizontalMoveSpeed * Time.deltaTime;
        }
    }

    private bool AllMembersDead()
    {
        foreach (Transform childTransform in transform)
        {
            if (childTransform.childCount > 0)
            {
                return false;
            }
        }
        Debug.Log("All enemies are destroyed. The next wave is coming...");
        return true;
    }

    private Transform NextFreePosition()
    {
        foreach (Transform childTransform in transform)
        {
            if (childTransform.childCount == 0)
            {
                return childTransform;
            }
        }
        return null;
    }

    private void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    private void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition) {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", timeBetweenRespawns);
        }
    }
}
