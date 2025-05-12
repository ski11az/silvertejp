using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterBox : MonoBehaviour
{
    public event Action VaseDestroyed;

    [SerializeField] AudioClip shatterSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject rootObj = collision.attachedRigidbody.gameObject;
        if (rootObj.layer != LayerMask.NameToLayer("Vase")) return;

        Vase deliveredVase = rootObj.GetComponent<Vase>();

        if (deliveredVase.hasBeenDestroyed) return;
        deliveredVase.hasBeenDestroyed = true;

        AudioManager.Instance.PlayClip(shatterSound);

        Destroy(rootObj);
        VaseDestroyed?.Invoke();
    }
}
