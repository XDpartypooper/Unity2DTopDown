using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D MapBound;
    [SerializeField] CinemachineConfiner2D[] Confinder;


    private void Awake()
    {
        Confinder = FindObjectsByType<CinemachineConfiner2D>(FindObjectsSortMode.None);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Confinder[0].BoundingShape2D = MapBound;
            Debug.Log("map name"+ MapBound.name);
        }
    }

}
