using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] Transform minPoint, maxPoint;
    [SerializeField] GrowBlock baseGridBlock;

    private Vector2Int gridSize;
    private Vector3 offset;

    public List<BlockRow> blockRows = new();

    public LayerMask gridBlockers;

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

        Vector3 startPos = minPoint.position + offset;

        gridSize = new Vector2Int(Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x),
            Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y));

        for (int y = 0; y < gridSize.y; y++)
        {
            blockRows.Add(new BlockRow());
            for (int x = 0; x < gridSize.x; x++)
            {
                Vector3 pos = startPos + new Vector3(x, y, 0);
                GrowBlock newBlock = Instantiate(baseGridBlock, pos, Quaternion.identity);

                newBlock.transform.SetParent(transform);
                blockRows[y].row.Add(newBlock);

                if (Physics2D.OverlapBox(newBlock.transform.position, new Vector2(0.9f, 0.9f), 0f, gridBlockers))
                {
                    newBlock.sr.sprite = null;
                    newBlock.preventUse = true;
                }
            }
        }

        baseGridBlock.gameObject.SetActive(false);
    }
}
