using System.Collections;
using UnityEngine;

public class PipeSpawner : Spawner
{
    [SerializeField] private GameObject scoreTrigger;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float gap = 2f;
    private float screenSize;
    private float topPipeY;
    private float topPipeSize;
    private float bottomPipeY;
    private float bottomPipeSize;
    private float scoreTriggerY;

    private void Start()
    {
        screenSize = Camera.main.orthographicSize * 2 - 1;
        GameManager.OnGameStart += StartSpawn;
    }

    protected override IEnumerator Spawn()
    {
        while (true)
        {
            Randomize();
            // Create top pipe
            GameObject topPipe = Instantiate(prefab, new Vector3(spawnX, topPipeY, 0f), Quaternion.identity);
            topPipe.transform.localScale = new Vector3(1f, -1f, 1f);
            topPipe.GetComponent<SpriteRenderer>().size = new Vector2(1f, topPipeSize);
            topPipe.GetComponent<BoxCollider2D>().size = new Vector2(1f, topPipeSize);
            // Create bottom pipe
            GameObject bottomPipe = Instantiate(prefab, new Vector3(spawnX, bottomPipeY, 0f), Quaternion.identity);
            bottomPipe.GetComponent<SpriteRenderer>().size = new Vector2(1f, bottomPipeSize);
            bottomPipe.GetComponent<BoxCollider2D>().size = new Vector2(1f, bottomPipeSize);
            // Create score trigger
            GameObject scoreTgr = Instantiate(scoreTrigger, new Vector3(spawnX, scoreTriggerY, 0f), Quaternion.identity);
            scoreTgr.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, gap);
            yield return new WaitForSeconds(spawnRatio);
        }
    }

    private void Randomize()
    {
        topPipeSize = Random.Range(1f,screenSize - gap - 1);
        bottomPipeSize = screenSize - gap - topPipeSize;
        topPipeY = 5 - 0.5f * topPipeSize;
        bottomPipeY = -4 + 0.5f * bottomPipeSize;
        scoreTriggerY = 5 - topPipeSize - gap / 2;
    }
}
