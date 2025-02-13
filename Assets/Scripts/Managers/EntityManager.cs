using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;
    public ScriptableEntity player;
    private List<ScriptableEntity> entities;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        entities = Resources.LoadAll<ScriptableEntity>("Entities").ToList();
    }

    public void SpawnPlayer()
    {

       BaseEntity spawnedEntity = Instantiate(player.EntityPrefab);
       spawnedEntity.name = player.name;

       Tile spawnTile = GridManager.Instance.GetEntitySpawnTile();

       spawnTile.SetEntity(spawnedEntity);

    }

    
    public void SpawnEntities()
    {
        Debug.Log("Spawning Entities");
        Debug.Log(entities.Count);
        for (int i = 0; i < entities.Count; i++)
        {

            BaseEntity spawnedEntity = Instantiate(entities[i].EntityPrefab);
            spawnedEntity.name = entities[i].name;


            Debug.Log($"Spawning {spawnedEntity.name}");

            Tile spawnTile = spawnedEntity is CrateEntity ? GridManager.Instance.GetEntitySpawnTileNoEdge() : GridManager.Instance.GetEntitySpawnTile();

            spawnTile.SetEntity(spawnedEntity);
        }
    }
    
}
