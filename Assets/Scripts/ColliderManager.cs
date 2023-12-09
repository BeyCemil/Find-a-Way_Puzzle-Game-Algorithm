using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] private GameObject[] lineRoads;
    private int[] randomRotations = { 0, 90, 180, 270 };
    //private bool success = false;

    private void Update() {

    }

    private void OnTriggerEnter(Collider collision)
    {
        foreach (GameObject lineroad in lineRoads)
        {
            if (collision.gameObject.tag == "Roads")
            {
                Debug.Log("Colliding with " + lineroad.name);
            }

        }
        Debug.Log("Waiting");

    }

}