using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviourPunCallbacks
{


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !photonView.IsMine)
        {
            Debug.Log("Tomou tiro");
            SceneManager.LoadSceneAsync("GameOver");
            Destroy(other.gameObject);
        }

        
    }
}
