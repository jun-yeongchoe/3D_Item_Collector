using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ProductBase : MonoBehaviour, IProduct
{
    [SerializeField] private ItemSO itemData;
    private string m_ProductName;
    public int score { get; private set; }
    
    public AudioSource audioSource;

    public string ProductName
    {
        get { return m_ProductName; }
        set { m_ProductName = value; }
    }

    public void Initialize()
    {
        m_ProductName = itemData.name;
        score = itemData.point;

        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
        
    }
}
