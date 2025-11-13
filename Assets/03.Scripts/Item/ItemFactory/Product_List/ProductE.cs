using UnityEngine;

public class ProductE : MonoBehaviour, IProduct
{
    [SerializeField] ItemSO itemData;
    private string m_ProductName;

    private float magneticRadius = 7f;
    [SerializeField] private LayerMask player;

    private Transform playerTF;
    private bool isMagnetOn;
    [SerializeField] private float followSpeed = 30f;
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

    private void Update()
    {
        if (!isMagnetOn)
        {
            MagneticItem();
        }
        else if (playerTF != null) FollowPlayer();
    }
    private void MagneticItem()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, magneticRadius, player);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerTF = hit.transform;
                isMagnetOn = true;
                break;
            }
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPos = playerTF.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, targetPos);
        if (dist < 0.3f)
        {
            //점수처리 필요

            //풀링에서 비활성화하는 코드필요
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magneticRadius);
    }
}
