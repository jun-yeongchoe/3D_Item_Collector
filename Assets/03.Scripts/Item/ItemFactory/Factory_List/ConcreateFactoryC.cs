using UnityEngine;

public class ConcreateFactoryC : Factory
{
    [SerializeField] private ProductC m_ProductPrefab;

    public override IProduct GetProduct(Vector3 pos)
    {
        GameObject inst = Instantiate(m_ProductPrefab.gameObject, pos, Quaternion.identity);

        ProductC newProduct = inst.GetComponent<ProductC>();

        newProduct.Initialize();

        return newProduct;
    }
}
