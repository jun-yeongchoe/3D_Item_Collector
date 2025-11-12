using UnityEngine;

public class ConcreateFactoryA : Factory
{
    private ProductA m_ProductPrefab;

    public override IProduct GetProduct(Vector3 pos)
    {
        GameObject inst = Instantiate(m_ProductPrefab.gameObject, pos, Quaternion.identity);

        ProductA newProduct = inst.GetComponent<ProductA>();

        newProduct.Initialize();

        return newProduct;
    }
}
