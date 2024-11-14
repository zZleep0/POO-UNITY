using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ShowPlayerNickname : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //Definir o nome do jogador
        GetComponent<Text>().text = photonView.Owner.NickName.Split(" | ")[0];
        Debug.Log(photonView.Owner.NickName.Split(" | ")[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
