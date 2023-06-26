using UnityEngine;

public class DarkFlare : Projectile
{
    protected override void InitializeProjectile()
    {
        _projectileSpeed = 5f;
        base.InitializeProjectile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            Debug.Log("���Ϳ��� ������");
        }

    }
}
