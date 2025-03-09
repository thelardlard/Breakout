using UnityEngine;

public class BrickGrid : MonoBehaviour
{
    public GameObject brickPrefab;
    public int gridWidth;
    public int gridHeight;
    private Grid grid;

    // Define an array of colors to cycle through for each row
    private Color[] rowColors = {
        new Color(0.6f, 0.2f, 0.2f),  // Dark Red
        new Color(0.2f, 0.6f, 0.2f),  // Dark Green
        new Color(0.2f, 0.2f, 0.8f),  // Dark Blue
        new Color(0.8f, 0.8f, 0.2f),  // Gold-like Yellow
        new Color(0.6f, 0.2f, 0.6f),  // Purple
        new Color(0.2f, 0.8f, 0.8f)   // Teal
    };

    void Start()
    {
        grid = GetComponent<Grid>(); // Access the grid component
        SpawnBricks();
    }

    public void SpawnBricks()
    {
        for (int x = -(gridWidth / 2); x < gridWidth / 2; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 cellPosition = grid.CellToWorld(new Vector3Int(x, y, 0)); // Get world position of cell
                GameObject brick = Instantiate(brickPrefab, cellPosition, Quaternion.identity); // Place brick

                // Assign color based on the row index (loop through the rowColors array)
                MeshRenderer brickRenderer = brick.GetComponent<MeshRenderer>();
                if (brickRenderer != null)
                {
                    brickRenderer.material = new Material(brickRenderer.material);
                    brickRenderer.material.color = rowColors[y % rowColors.Length];// Assign color based on row index
                    // Set metallic and smoothness values
                    brickRenderer.material.SetFloat("_Metallic", 1f);     // Fully metallic
                    brickRenderer.material.SetFloat("_Glossiness", 1f);  // High smoothness for reflections
                }
            }
        }
    }
}
