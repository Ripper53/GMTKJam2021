using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour {
    public Rigidbody Projectile;

    private readonly List<Rigidbody> projectiles = new List<Rigidbody>();

    public Rigidbody Get() {
        foreach (Rigidbody projectile in projectiles) {
            if (!projectile.gameObject.activeSelf)
                return projectile;
        }
        Rigidbody newProjectile = Instantiate(Projectile);
        projectiles.Add(newProjectile);
        return newProjectile;
    }

}
