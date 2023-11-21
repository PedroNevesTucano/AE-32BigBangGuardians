using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public GameObject enemyPrefab;

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0.2f)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (timer <= 0)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
}