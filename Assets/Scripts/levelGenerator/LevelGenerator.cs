using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public float offset = 5f;
    public Material mat01, mat02;

    public void RemoveLabirynth()
    {
        //foreach (Transform child in transform)
        //{
        //    if (child.tag == "LevelGenerator")
        //        continue;
        //    DestroyImmediate(child.gameObject);
        //}

        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
    public void GenerateLabirynth()
    {
        for(int x = 0; x < map.width; x++)
        {
            for (int z = 0; z < map.height; z++)
            {
                GenerateTile(x, z);
            }
        }
        ColorTheChildren();
    }
    private void GenerateTile(int x, int z)
    {
        Color pixelColor = map.GetPixel(x, z);
        if (pixelColor.a == 0) return;

        foreach(ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 pos = new Vector3(x, 0, z) * offset;
                Instantiate(colorMapping.prefab, pos, Quaternion.identity, transform);
            }
        }
    }
    private void ColorTheChildren()
    {
        foreach (Transform child in transform) 
        {
            if (child.gameObject.tag == "Wall")
            {
                foreach(Transform grandchild in child.transform)
                {
                    grandchild.GetComponent<Renderer>().material =
                        GetMaterial();
                }
            }
        }
    }
    private Material GetMaterial()
    {
        if (UnityEngine.Random.Range(1, 100) % 3 == 0)
            return mat01;
        return mat02;
    }
}
