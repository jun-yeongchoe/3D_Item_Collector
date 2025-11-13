using UnityEngine;

public class ConcreateFactoryA : Factory
{
    //private GameObject m_ProductPrefab;
    //[SerializeField] private ItemSO itemData;

    //private void Awake()
    //{
    //    m_ProductPrefab = itemData.prefab;
    //}

    //public override IProduct GetProduct(Vector3 pos)
    //{
    //    GameObject inst = Instantiate(m_ProductPrefab.g, pos, Quaternion.identity);

    [SerializeField] private ProductA m_ProductPrefab;

    public override IProduct GetProduct(Vector3 pos)
    {
        GameObject inst = Instantiate(m_ProductPrefab.gameObject, pos, Quaternion.identity);
        // ---------------------
        ProductA newProduct = inst.GetComponent<ProductA>();

        newProduct.Initialize();

        return newProduct;
    }
}
