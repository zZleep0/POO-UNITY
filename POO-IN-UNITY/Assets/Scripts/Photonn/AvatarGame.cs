using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarGame : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject chosenAvatar = (GameObject)Instantiate(Resources.Load(photonView.Owner.NickName.Split(" | ")[1]));
        chosenAvatar.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
