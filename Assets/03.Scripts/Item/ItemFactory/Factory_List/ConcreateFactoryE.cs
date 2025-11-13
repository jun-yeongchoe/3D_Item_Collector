using UnityEngine;

public class ConcreateFactoryE : Factory
{
    [SerializeField] private ProductE m_ProductPrefab;

    public override IProduct GetProduct(Vector3 pos)
    {
        GameObject inst = Instantiate(m_ProductPrefab.gameObject, pos, Quaternion.identity);

        ProductE newProduct = inst.GetComponent<ProductE>();

        newProduct.Initialize();

        return newProduct;
    }
}
