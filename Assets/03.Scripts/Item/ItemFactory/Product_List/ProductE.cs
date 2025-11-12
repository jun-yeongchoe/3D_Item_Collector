using UnityEngine;

public class ProductE : MonoBehaviour, IProduct
{
    [SerializeField] ItemSO itemData;
    private string m_ProductName;
    public string ProductName
    {
        get { return m_ProductName; }
        set { m_ProductName = value; }
    }
    private ParticleSystem m_particleSystem;

    public void Initialize()
    {
        m_ProductName = itemData.name;
        gameObject.name = m_ProductName;

        m_particleSystem = GetComponent<ParticleSystem>();
        if (m_particleSystem == null) return;

        m_particleSystem.Stop();
        m_particleSystem.Play();
    }
}
