using UnityEngine;

public class ConcreateFactoryD : Factory
{
    [SerializeField] private ProductD m_ProductPrefab;
    [SerializeField] private int initCount = 15;
    [SerializeField] private Transform poolRoot;

    private bool init = false;

    private void Awake()
    {
        if (init) return;
        init = true;

        if (m_ProductPrefab == null) return;
        if (poolRoot == null) poolRoot = GameManager.Pool.transform;

        GameManager.Pool.CreatePool(m_ProductPrefab, initCount, poolRoot);
    }

    public override IProduct GetProduct(Vector3 pos)
    {
        if (m_ProductPrefab == null) return null;

        ProductD inst = GameManager.Pool.GetFromPool(m_ProductPrefab);

        if (inst == null) inst = Instantiate(m_ProductPrefab, pos, Quaternion.identity);

        inst.transform.position = pos;
        inst.gameObject.SetActive(true);

        inst.Initialize();

        return inst;
    }
}
