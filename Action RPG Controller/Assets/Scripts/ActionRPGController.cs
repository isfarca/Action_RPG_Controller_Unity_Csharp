using System.Collections;
using UnityEngine;

public class ActionRPGController : MonoBehaviour
{
    // Value types
    [Range(0.01f, 1f)] public float maxDistanceToTarget = 1f;
    [Range(1f, 10f)] public float movementSpeed = 5f;

    // Reference types
    private Ray ray;
    private RaycastHit hitInfo;
    [SerializeField] private LayerMask movementLayers;
    private Vector3 targetPosition;
    private Vector3 direction;
    private Coroutine moveCoroutine;
    [Header("UI"), SerializeField] private WorldSpaceLabelUI labelPrefab;

    // Use this for initialization
    void Start()
    {
        // Declare variables
        int layer1 = LayerMask.NameToLayer("BlockMovement");
        int layer2 = LayerMask.NameToLayer("Water");
        int layermask1 = 1 << layer1;
        int layermask2 = 1 << layer2;
        int finalmask = layermask1 | layermask2;

        Debug.Log(finalmask);
    }

    // Update is called once per frame
    void Update()
    {
        // Declare variables
        WorldSpaceLabelUI label;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hitInfo, float.MaxValue, movementLayers);

            if (Physics.Raycast(ray, float.MaxValue))
            {
                Debug.LogFormat("Ray hit {0} at {1}", hitInfo.collider.name, hitInfo.point);

                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);

                moveCoroutine = StartCoroutine(MoveToTarget(hitInfo.point));

                if (labelPrefab != null)
                {
                    label = Instantiate(labelPrefab);
                    label.Init(hitInfo.point + Vector3.up * 2f, hitInfo.collider.name);
                }
            }
        }
	}

    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > maxDistanceToTarget)
        {
            direction = targetPosition - transform.position;
            direction.y = 0f;
            direction.Normalize();

            transform.position += direction * movementSpeed * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        moveCoroutine = null;
    }
}
