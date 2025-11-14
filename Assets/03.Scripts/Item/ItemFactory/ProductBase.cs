using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ProductBase : MonoBehaviour, IProduct
{
    [SerializeField] private ItemSO itemData;
    private string m_ProductName;
    public int score { get; private set; }
    private ParticleSystem m_particleSystem;

    public string ProductName
    {
        get { return m_ProductName; }
        set { m_ProductName = value; }
    }

    public void Initialize()
    {
        m_ProductName = itemData.name;
        score = itemData.point;
        m_particleSystem = GetComponent<ParticleSystem>();
        if (m_particleSystem == null) return;

        m_particleSystem.Stop();
        m_particleSystem.Play();
    }
}
