using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("UI")]
    public GameObject loseTextObject;

    [Header("Effects")]
    public ParticleSystem loseEffect;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // �������� �������� ��������, ��� ����� �������
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        // ���� ������ �� ������ ������ � ������ �� �����
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
                player = foundPlayer.transform;
        }

        // ������� lose �����
        if (loseTextObject != null)
            loseTextObject.SetActive(false);
    }

    void Update()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �������� ��������
            if (loseEffect != null)
            {
                loseEffect.transform.position = collision.transform.position;
                loseEffect.Play();
            }

            // �������� "You Lose!"
            if (loseTextObject != null)
            {
                loseTextObject.SetActive(true);
                loseTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            }

            // ������� ������
            Destroy(collision.gameObject);
        }
    }
}
