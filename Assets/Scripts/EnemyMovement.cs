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

        // Вимикаємо уникання перешкод, щоб ворог врізався
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        // Якщо вручну не задано гравця — знайти за тегом
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
                player = foundPlayer.transform;
        }

        // Сховати lose текст
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
            // Показати частинки
            if (loseEffect != null)
            {
                loseEffect.transform.position = collision.transform.position;
                loseEffect.Play();
            }

            // Показати "You Lose!"
            if (loseTextObject != null)
            {
                loseTextObject.SetActive(true);
                loseTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            }

            // Знищити гравця
            Destroy(collision.gameObject);
        }
    }
}
