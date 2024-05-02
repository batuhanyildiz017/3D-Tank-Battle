using System.Collections;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField]
    private float viewRadius = 11;
    [SerializeField]
    private float detectionCheckDelay = 0.1f;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private LayerMask playerLayerMask;
    [SerializeField]
    private LayerMask visibilityLayer;

    [field: SerializeField]
    public bool TargetVisible { get; private set; }
    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if (Target != null)
            TargetVisible = CheckTargetVisible();
    }

    private bool CheckTargetVisible()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, viewRadius, visibilityLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void DetectTarget()
    {
        if (Target == null)
            CheckIfPlayerInRange();
        else if (Target != null)
            DetectIfOutOfRange();
    }

    private void DetectIfOutOfRange()
    {
        if (Target == null || !Target.gameObject.activeSelf || Vector3.Distance(transform.position, Target.position) > viewRadius + 1)
        {
            Target = null;
        }
    }

    private void CheckIfPlayerInRange()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, viewRadius, playerLayerMask);
        foreach (Collider col in collisions)
        {
            if (col.CompareTag("Player"))
            {
                Target = col.transform;
                break;
            }
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
