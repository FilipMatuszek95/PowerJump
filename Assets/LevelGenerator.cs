using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject platformPrefab;

    [SerializeField]
    int numberOfPlatforms = 200;

    [SerializeField]
    GameObject leftWallPrefab;

    [SerializeField]
    GameObject rightWallPrefab;

    [SerializeField]
    int numberOfWalls = 200;

    [SerializeField]
    float levelWidth = 2f;

    [SerializeField]
    float minY = 2f;

    [SerializeField]
    float maxY = 5f;

    void Start()
    {
        Vector3 platformSpawnPosition = new Vector3();

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            platformSpawnPosition.y += Random.Range(minY, maxY);
            platformSpawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(platformPrefab, platformSpawnPosition, Quaternion.identity);
        }

        Vector3 leftWallSpawnPosition = new Vector3();
        Vector3 rightWallSpawnPosition = new Vector3();
        for (int i = 0; i < numberOfWalls; i++)
        {
            leftWallSpawnPosition.y += leftWallPrefab.transform.localScale.y * 2f;
            leftWallSpawnPosition.x = leftWallPrefab.transform.position.x;
            Instantiate(leftWallPrefab, leftWallSpawnPosition, Quaternion.identity);

            rightWallSpawnPosition.y += rightWallPrefab.transform.localScale.y * 2f;
            rightWallSpawnPosition.x = rightWallPrefab.transform.position.x;
            Instantiate(rightWallPrefab, rightWallSpawnPosition, Quaternion.identity);
        }
    }
}
