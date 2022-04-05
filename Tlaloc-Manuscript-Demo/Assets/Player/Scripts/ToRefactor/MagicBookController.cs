using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBookController : MonoBehaviour
{
    [SerializeField] private GameObject closedBook;
    [SerializeField] private GameObject openedBook;

    [SerializeField] private GameObject fireBallPrefab;
    
    [HideInInspector] public GameObject createdFireBall;

    public void OnActive() {
        closedBook.SetActive(false);
        openedBook.SetActive(true);

        CreateFireBall();
    }

    public void OnDeactivate() {
        closedBook.SetActive(true);
        openedBook.SetActive(false);

        if (createdFireBall != null) {
            Destroy(createdFireBall);
        }
    }

    public void CreateFireBall() {
        if (createdFireBall == null) {
            createdFireBall = Instantiate(fireBallPrefab, openedBook.transform);
            createdFireBall.transform.localPosition = new Vector3(0.07f, -0.08f, 0.277f);
        }
    }
}
