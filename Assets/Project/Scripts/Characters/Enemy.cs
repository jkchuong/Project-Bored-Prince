using System;
using UnityEngine;

public class Enemy : PhysicsObject
{
    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        Health.DoDeath += HealthOnDoDeath;
    }

    protected virtual void HealthOnDoDeath()
    {
        gameObject.SetActive(false);
    }
}
