using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine.Networking;

public class Gameboard : Grid
{
   private int _maxItems;
   private List<Vector2> _cardPositions;
   public GameObject card;
   public GameObject playerBase;
   private Vector2[] _playerPositions;
   
   
   private void Awake()
   {
      columns = 8;
      rows = 8;
      _maxItems = 8;
      tiles = new Tile[columns, rows];
      _cardPositions = new List<Vector2>();
   }
    
   private void Start()
   {
     _playerPositions = new Vector2[4];
     Generate();
     ResetColor();
     SpawnPlayers();
     SpawnItems();
     Checkered();
   }

   private void SpawnPlayers()
   {
       _playerPositions[0] = new Vector2(0, 0);
       _playerPositions[1] = new Vector2(0, columns - 1);
       _playerPositions[2] = new Vector2(columns - 1, 0);
       _playerPositions[3] = new Vector2(columns - 1,columns - 1);

       for (int l = 0; l < _playerPositions.Length; l++)
       {
           tiles[(int) _playerPositions[l].x, (int) _playerPositions[l].y].SetItem(playerBase);
       }
   }
   
   private void SpawnItems()
   {   
       for (int k = 0; k < _maxItems; k++)
       {
           int randomX = Random.Range(0, columns - 1);
           int randomY = Random.Range(0, columns - 1);
           
           tiles[randomX, randomY].SetItem(card);
       }
   }

   private void Checkered()
   {
       for (var i = 0; i < columns; i++)
       {
           for (var j = 0; j < rows; j++)
           {
               if (i % 2 != 0 && j % 2 != 0 || i % 2 == 0 && j % 2 == 0)
               {
                   tiles[i,j].GetComponent<Renderer>().material.SetColor("_Color", Color.black);
               }
               else
               {
                   tiles[i,j].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
               }
               
               for (int l = 0; l < _playerPositions.Length; l++)
               {
                   if (i == _playerPositions[l].x && j == _playerPositions[l].y)
                   {
                       tiles[i,j].GetComponent<Renderer>().material.SetColor("_Color", Color.red); 
                   }
               }
           }
       }
   }
   
   private void ResetColor()
   {
       for (var i = 0; i < columns; i++)
       {
           for (var j = 0; j < rows; j++)
           {
               tiles[i,j].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
           }
       }
   }
}
