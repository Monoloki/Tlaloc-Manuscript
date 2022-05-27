using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    public Transform[] waypointArray;

    [SerializeField] private NavMeshAgent agent;

    private int activewayPoint = 0;

    private void Start() {
        StartCoroutine(MoveToDestination());
    }

    private IEnumerator MoveToDestination() {
        while (gameObject) {
            agent.destination = waypointArray[activewayPoint].position;

            if ((transform.position - waypointArray[activewayPoint].position).magnitude <= 4f && activewayPoint < waypointArray.Length - 1) {
                activewayPoint++;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
