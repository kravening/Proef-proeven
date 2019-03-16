using UnityEngine;

public abstract class Grid : MonoBehaviour
{
   public static Grid instance { get; private set; }
 

   [HideInInspector] public int columns;
   [HideInInspector] public int rows;
   [HideInInspector] public Tile[,] tiles;
   public GameObject tile;
   
   private void Awake()
   {
      if (instance != null && instance != this)
      {
         Destroy(this);
      }
      else
      {
         instance = this;
      }
   }
 
   private void OnDestroy()
   {
      if (instance == this)
      {
         instance = null;
      }
   }
   
   protected void Generate()
   {
      for (var i = 0; i < columns; i++)
      {
         for (var j = 0; j < rows; j++)
         {
            GameObject spawnedTile = Instantiate(tile, new Vector3(i, 0, j), Quaternion.identity);
            tiles[i, j] = spawnedTile.GetComponent<Tile>();
             spawnedTile.gameObject.transform.parent = transform;
         }
      }
   }
   
   public void RemoveTile(int x, int y)
   {
      Destroy(tiles[x,y]);
   }
}
