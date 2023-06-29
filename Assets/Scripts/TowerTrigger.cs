using Unity;
using UnityEngine;

public class TowerTrigger: MonoBehaviour { 
    public void StartCleaning()
    {
        GameManager.Instance.StartCleaning();
        //StickToUserHand();
        Destroy(this.gameObject);
    }

}