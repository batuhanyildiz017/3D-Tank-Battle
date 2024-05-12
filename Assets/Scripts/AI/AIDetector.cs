using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    // AI'nin görme menzili için minimum ve maksimum değerleri belirleyen değişken.
    [Range(1, 15)]
    [SerializeField]
    private float viewRadius = 11;

    // Algılama işleminin ne sıklıkla gerçekleşeceğini belirleyen gecikme süresi.
    [SerializeField]
    private float detectionCheckDelay = 0.1f;

    // Hedef nesnenin referansını tutan dönüşken.
    [SerializeField]
    private Transform target = null;

    // Oyuncu katmanının maskesi.
    [SerializeField]
    private LayerMask playerLayerMask;

    // Görünürlük katmanının maskesi.
    [SerializeField]
    private LayerMask visibilityLayer;

    // Hedef nesnenin görünür olup olmadığını kontrol eden özellik.
    [field: SerializeField]
    public bool TargetVisible { get; private set; }

    // Hedef nesnenin referansını saklayan ve ayarlayan özellik.
    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    // Algılama işleminin başlangıcında çağrılan metot.
    private void Start()
    {
        // Algılama işlemini başlatan rutin.
        StartCoroutine(DetectionCoroutine());
    }

    // Her frame'de çağrılan metot.
    private void Update()
    {
        // Eğer hedef nesne varsa, görünürlüğünü kontrol eden metot.
        if (Target != null)
            TargetVisible = CheckTargetVisible();
    }

    // Hedef nesnenin görünürlüğünü kontrol eden metot.
    private bool CheckTargetVisible()
    {
        // AI'nin pozisyonundan hedef nesnenin pozisyonuna bir ışın gönderir ve görünürlük katmanındaki engelleri kontrol eder.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (Target.position - transform.position).normalized, out hit, viewRadius, visibilityLayer))
        {
            // Eğer bir engel varsa, hedefin oyuncu olup olmadığını kontrol eder.
            if (hit.collider.CompareTag("Player"))
                return true;
        }
        return false;
    }

    // Hedef nesneyi algılamayı sağlayan metot.
    private void DetectTarget()
    {
        // Eğer hedef nesne yoksa, oyuncu algı menzili içinde mi kontrol eder.
        if (Target == null)
            CheckIfPlayerInRange();
        // Eğer hedef nesne varsa, hala algı menzili içinde mi kontrol eder.
        else if (Target != null)
            DetectIfOutOfRange();
    }

    // Hedef nesnenin algı menzili dışında olup olmadığını kontrol eden metot.
    private void DetectIfOutOfRange()
    {
        // Eğer hedef nesne yoksa veya devre dışı bırakıldıysa veya algı menzili dışında ise, hedef nesneyi null olarak ayarlar.
        if (Target == null || !Target.gameObject.activeSelf || Vector3.Distance(transform.position, Target.position) > viewRadius + 1)
        {
            Target = null;
        }
    }

    // Oyuncunun algı menzili içinde olup olmadığını kontrol eden metot.
    private void CheckIfPlayerInRange()
    {
        // AI'nin bulunduğu noktada belirli bir yarıçapa sahip bir alan var mı diye kontrol eder.
        Collider[] colliders = Physics.OverlapSphere(transform.position, viewRadius, playerLayerMask);
        // Eğer bir alan varsa, bu alanın merkezindeki nesneyi hedef olarak ayarlar.
        foreach (Collider collider in colliders)
        {
            Target = collider.transform;
            break;
        }
    }

    // Algılama işlemini belirli aralıklarla gerçekleştiren rutin.
    IEnumerator DetectionCoroutine()
    {
        // Belirli bir süre sonra algılama işlemini tekrarlar.
        yield return new WaitForSeconds(detectionCheckDelay);
        // Hedef nesneyi algılamak için ilgili metodu çağırır.
        DetectTarget();
        // Algılama işlemini tekrarlamak için bu rutini yeniden başlatır.
        StartCoroutine(DetectionCoroutine());
    }

    // Gizmos ile çizilen alanı görselleştiren metot.
    private void OnDrawGizmos()
    {
        // Görselleştirme için gerekli renk ve şekli ayarlar.
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
