using System;
using System.IO.Compression;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Grid
{
    int width;
    int height;
    float size;
    int [,] gridarray;
    

    public Grid(int w, int h,float s){
        width = w;
        height = h;
        size = s;

        gridarray = new int[width,height];

        Debug.Log("width: "+ width + " height: "+height + " size: "+ size);
        
        for(int x=0;x<gridarray.GetLength(0);x++){
            for(int z=0;z<gridarray.GetLength(1);z++){
                Debug.Log(x + " " + z);
                Debug.DrawLine(GetWorldPosition(x,z),GetWorldPosition(x+1,z),Color.white,1000f);
                Debug.DrawLine(GetWorldPosition(x,z),GetWorldPosition(x,z+1),Color.white,1000f);
                
            }
        }

       Debug.DrawLine(GetWorldPosition(0,height),GetWorldPosition(width,height),Color.white,1000f);
       Debug.DrawLine(GetWorldPosition(width,0),GetWorldPosition(width,height),Color.white,1000f);
    }

    private Vector3 GetWorldPosition(int x , int z){
        return new Vector3(x,0,z) * size;
    }

    public Vector3 GetMap(int x,int z){
        return new Vector3(x * size,0,z * size);
    }



}
