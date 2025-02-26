using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] Transform minPoint, maxPoint;
    [SerializeField] GrowBlock baseGridBlock;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0.5f, 0.5f, 0);
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateGrid()
    {
        minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0);
        maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0);

        Instantiate(baseGridBlock, minPoint.position + offset, Quaternion.identity);
    }
}
