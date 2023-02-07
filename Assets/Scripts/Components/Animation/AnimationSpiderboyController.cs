using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpiderboyController : MonoBehaviour
{
    private Animator animator;
    private SpiderBoy spiderBoy;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spiderBoy = GetComponent<SpiderBoy>();
    }
    private void Start()
    {
        spiderBoy.SpawnEvent.AddListener(OnSpawn);
    }

    private void OnSpawn()
    {
        animator.SetBool("IsSpawning", true);
        StartCoroutine(ResetSpawnParameter());
    }

    private IEnumerator ResetSpawnParameter()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("IsSpawning", false);
    }
}
