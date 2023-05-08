using UnityEngine;

public class FireLaserCommand : InputCommand
{
    private LineRenderer laserLine;
    private Transform laserSpawnPoint;
    private float laserLength;
    private LayerMask laserHitLayers;
    private float damagePerSecond;

    public FireLaserCommand(LineRenderer laserLine, Transform laserSpawnPoint, float laserLength, LayerMask laserHitLayers, float damagePerSecond)
    {
        this.laserLine = laserLine;
        this.laserSpawnPoint = laserSpawnPoint;
        this.laserLength = laserLength;
        this.laserHitLayers = laserHitLayers;
        this.damagePerSecond = damagePerSecond;
    }

    public void Execute()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserSpawnPoint.position, laserSpawnPoint.up, laserLength, laserHitLayers);
        Vector3 endPoint;
        if (hit.collider != null)
        {
            endPoint = hit.point;
            // Apply damage to the hit object if it has a Health component
            Health hitHealth = hit.collider.GetComponent<Health>();
            if (hitHealth != null)
            {
                hitHealth.TakeDamage((int)(damagePerSecond * Time.deltaTime));
            }
        }
        else
        {
            endPoint = laserSpawnPoint.position + laserSpawnPoint.up * laserLength;
        }

        laserLine.SetPosition(0, laserSpawnPoint.position);
        laserLine.SetPosition(1, endPoint);
    }
}

