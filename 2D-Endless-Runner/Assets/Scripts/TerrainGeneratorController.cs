using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneratorController : MonoBehaviour
{
    
    [Header("Templates")]
    public List<TerrainTemplateController>terrainTemplates;
    public float terrainTemplateWidth;

    [Header("Generator Area")]
    public Camera gamecam;
    public float areaStartOffset;
    public float areaEndOffset;

    private const float debugLineH =10.0f;

    private float GetHPosStart()
    {
        return gamecam.ViewportToWorldPoint(new Vector2(0f,0f)).x + areaStartOffset;
    }

    private float GetHPosEnd()
    {
        return gamecam.ViewportToWorldPoint(new Vector2(1f,0f)).x + areaEndOffset;
    }

    // debug
    private void OnDrawGizmos()
    {
        Vector3 areaStartPos =transform.position;
        Vector3 areaEndPos = transform.position;

        areaStartPos.x = GetHPosStart();
        areaEndPos.x = GetHPosEnd();

        Debug.DrawLine(areaStartPos + Vector3.up * debugLineH / 2, areaStartPos + Vector3.down * debugLineH / 2, Color.red);
        Debug.DrawLine(areaEndPos + Vector3.up * debugLineH / 2, areaEndPos + Vector3.down * debugLineH / 2, Color.red);
    }


    // Gneerate Object
    private List<GameObject> spawnedTerrain;

    private float lastGeneratedPosX;

    [Header("Force Early Template")]
    public List<TerrainTemplateController> earlyTerrainTemplates;

    // Del Object
    private float lastRemovedPosX;

    // pool List
    private Dictionary<string,List<GameObject>>pool;
    private void Start()
    {
        // init pool
        pool = new Dictionary<string, List<GameObject>>();

        spawnedTerrain = new List<GameObject>();

        lastGeneratedPosX = GetHPosStart();
        lastRemovedPosX = lastGeneratedPosX - terrainTemplateWidth;

     foreach(TerrainTemplateController terrain in earlyTerrainTemplates)
     {
         GenerateTerrain(lastGeneratedPosX,terrain);
         lastGeneratedPosX +=terrainTemplateWidth;
     }

        while(lastGeneratedPosX < GetHPosEnd())
        {
            GenerateTerrain(lastGeneratedPosX);
            lastGeneratedPosX +=terrainTemplateWidth;
        }
    }

    // pool function
    private GameObject GenerateFromPool(GameObject item, Transform parent)
    {
        if(pool.ContainsKey(item.name))
        {
            // if item available in pool
            if(pool[item.name].Count > 0)
            {
                GameObject newItemFromPool = pool[item.name][0];
                pool[item.name].Remove(newItemFromPool);
                newItemFromPool.SetActive(true);
                return newItemFromPool;
            }
        }
        else{
            // if item list not defined, create new one
            pool.Add(item.name,new List<GameObject>());
        }

        // create new one if no item available in pool
        GameObject newItem = Instantiate(item,parent);
        newItem.name = item.name;
        return newItem;
    }

    private void ReturnToPool(GameObject item)
    {
        if(!pool.ContainsKey(item.name))
        {
            Debug.LogError("INVALID POOL ITEM!!");
        }
        pool[item.name].Add(item);
        item.SetActive(false);
    }

    private void GenerateTerrain(float posX, TerrainTemplateController forceterrain = null)
    {
        GameObject newTerrain = Instantiate(terrainTemplates[Random.Range(0,terrainTemplates.Count)].gameObject,transform);

        newTerrain.transform.position = new Vector2(posX,0f);

        spawnedTerrain.Add(newTerrain);
    }

    // Update is called once per frame
    private void Update()
    {
        while(lastGeneratedPosX < GetHPosEnd())
        {
            GenerateTerrain(lastGeneratedPosX);
            lastGeneratedPosX +=terrainTemplateWidth;
        }

        while(lastRemovedPosX + terrainTemplateWidth < GetHPosStart())
        {
            lastRemovedPosX += terrainTemplateWidth;
            RemoveTerrain(lastRemovedPosX);
        }
    }

    private void RemoveTerrain(float posX)
    {
        GameObject terrainToRemove = null;

        // find terrain at posX
        foreach(GameObject item in spawnedTerrain)
        {
            if(item.transform.position.x == posX)
            {
                terrainToRemove = item;
                break;
            }
        }

        // after found
        if(terrainToRemove != null)
        {
            spawnedTerrain.Remove(terrainToRemove);
            Destroy(terrainToRemove);
        }
    }
}
