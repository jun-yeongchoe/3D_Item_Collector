using UnityEngine;

public class ConcreateFactoryD : Factory
{
    private ProductD m_ProductPrefab;

    public override IProduct GetProduct(Vector3 pos)
    {
        GameObject inst = Instantiate(m_ProductPrefab.gameObject, pos, Quaternion.identity);

        ProductD newProduct = inst.GetComponent<ProductD>();

        newProduct.Initialize();

        return newProduct;
    }
}
