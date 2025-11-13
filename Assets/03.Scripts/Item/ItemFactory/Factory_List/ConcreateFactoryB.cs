using UnityEngine;

public class ConcreateFactoryB : Factory
{
    [SerializeField] private ProductB m_ProductPrefab;

    public override IProduct GetProduct(Vector3 pos)
    {
        GameObject inst = Instantiate(m_ProductPrefab.gameObject, pos, Quaternion.identity);

        ProductB newProduct = inst.GetComponent<ProductB>();

        newProduct.Initialize();

        return newProduct;
    }
}
