using UnityEngine;

public class ProductC : ProductBase
{
    private float magneticRadius = 7f;
    [SerializeField] private LayerMask player;
    [SerializeField] ParticleSystem m_particleSystem;

    private Transform playerTF;
    private bool isMagnetOn;
    [SerializeField] private float followSpeed = 30f;

    private void OnEnable()
    {
        if (m_particleSystem != null)
        {
            m_particleSystem.Stop();
        }
        isMagnetOn = false;
        playerTF = null;
        Initialize();
    }

    private void Update()
    {
        if (!isMagnetOn) MagneticItem();
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
            GameManager.Score += base.score;

            if (audioSource != null && audioSource.clip != null)
            {
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, 3.0f);
            }
            if (m_particleSystem != null)
            {
                var fx = Instantiate(m_particleSystem, transform.position, Quaternion.identity);

                fx.gameObject.SetActive(true);
                fx.Stop();
                fx.Play();

                var main = fx.main;
                float life = main.duration + main.startLifetime.constantMax;
                Destroy(fx.gameObject, life);
            }
            //풀링에서 비활성화하는 코드필요
            GameManager.Pool.ReturnPool(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magneticRadius);
    }
}
