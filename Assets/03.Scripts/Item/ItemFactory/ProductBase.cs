using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBase : MonoBehaviour, IProduct
{
    [SerializeField] private ItemSO itemData;
    private string m_ProductName;
    private ParticleSystem m_particleSystem;

    public string ProductName
    {
        get { return m_ProductName; }
        set { m_ProductName = value; }
    }

    public void Initialize()
    {
        m_ProductName = itemData.name;

        m_particleSystem = GetComponent<ParticleSystem>();
        if (m_particleSystem == null) return;

        m_particleSystem.Stop();
        m_particleSystem.Play();
    }
}
