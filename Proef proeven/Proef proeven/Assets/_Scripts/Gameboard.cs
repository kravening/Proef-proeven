using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class Gameboard : Grid
{
   private int _maxItems;
   private List<Vector2> _cardPositions;
   public GameObject card;

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
     Generate();
     ResetColor();
     SpawnItems();
     Checkered();
   }

   private void SpawnItems()
   {
       
       for (int k = 0; k < _maxItems; k++)
       {
           int randomX = Random.Range(0, 7);
           int randomY = Random.Range(0, 7);
           
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
