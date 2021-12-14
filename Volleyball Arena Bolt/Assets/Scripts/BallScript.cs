using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using Photon.Bolt.Internal;
using UnityEngine;

public class BallScript : EntityBehaviour<IBallObjectState>

{

    public override void Attached()
    {
        state.SetTransforms(state.BallObjectTransform, transform);
    }
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (BoltNetwork.IsServer)
        {
            
            if (other.collider.name.Equals("Ground"))
            {
                if (transform.position.x < 0)
                {
                    //TODO: CLIENT SCORE ALIR
                    ScoreManager.Instance.AddScoreToClient();
                }
                else
                {
                    //TODO: MASTER CLIENT SCORE ALIR
                    ScoreManager.Instance.AddScoreToMaster();
                }
            }
            
        }
        
    }
}
