using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Terrain SpawnPlace;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private Factory[] m_Factories;
    [SerializeField] private int itemCount = 15;
    private float maxY = 3f;
    private List<GameObject> m_createdProducts = new List<GameObject>();

    private void Start()
    {
        if(!SpawnPlace) SpawnPlace = Terrain.activeTerrain;
        PlaceItem();
    }

    private void PlaceItem()
    {
        if (m_Factories == null || m_Factories.Length == 0) return;

        TerrainData td = SpawnPlace.terrainData;
        Vector3 tPos = SpawnPlace.transform.position;
        Vector3 tSize = td.size;

        int spawned = 0;
        int maxTry = itemCount * 30;

        while(spawned < itemCount && maxTry > 0)
        {
            maxTry--;

            float x = Random.Range(tPos.x, tPos.x + tSize.x);
            float z = Random.Range(tPos.z, tPos.z + tSize.z);

            float y = SpawnPlace.SampleHeight(new Vector3(x, 0, z)) + tPos.y;

            if(y <= maxY)
            {
                Vector3 pos = new Vector3(x, y, z);
                Factory selectedFactory = m_Factories[Random.Range(0, m_Factories.Length)];
                IProduct product = selectedFactory.GetProduct(pos + m_Offset);
                if (product is Component component) { 
                    component.gameObject.SetActive(false);
                    m_createdProducts.Add(component.gameObject); }
                spawned++;
            }
        }
    }
    private void OnDestroy()
    {
        foreach (GameObject product in m_createdProducts)
        {
            if (product != null) Destroy(product);
        }
        m_createdProducts.Clear();
    }
}
