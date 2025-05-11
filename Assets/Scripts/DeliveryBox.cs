using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox : MonoBehaviour
{
    public event Action<int> VaseDelivered;

    [SerializeField] AudioClip sellSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject rootObj = collision.attachedRigidbody.gameObject;
        if (rootObj.layer != LayerMask.NameToLayer("Vase")) return;

        Vase deliveredVase = rootObj.GetComponent<Vase>();
        int value = deliveredVase.GetScore();

        AudioManager.Instance.PlayClip(sellSound);
        VaseDelivered?.Invoke(value);

        Destroy(deliveredVase.gameObject);
    }
}
