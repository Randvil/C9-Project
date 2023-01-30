using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractive
{
    public Collider2D GetCollider2D { get; }
    //public void OnTriggerEnter2D(Collider2D other);
}
