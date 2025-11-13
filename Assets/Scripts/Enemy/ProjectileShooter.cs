using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Header("Setup")]
    public GameObject projectilePrefab;   // prefab Bullet
    public Transform shootPoint;          // GunPoint
    public KeyCode fireKey = KeyCode.F;

    [Header("Ballistics")]
    public float shootForce = 600f;
    public bool disableGravity = true;    // recto como láser

    private void Update()
    {
        if (Input.GetKeyDown(fireKey))
            Shoot();
    }

    private void Shoot()
    {
        if (!projectilePrefab || !shootPoint) return;

        var proj = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        if (proj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.useGravity = !disableGravity;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        }

        Debug.DrawRay(shootPoint.position, shootPoint.forward * 30f, Color.red, 0.25f);
    }
}