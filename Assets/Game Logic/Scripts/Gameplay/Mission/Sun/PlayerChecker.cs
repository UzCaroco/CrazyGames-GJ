using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerChecker : NetworkBehaviour
{
    [SerializeField] private PlayerController playerController;
    public bool MissionProjectile(bool expectedResult)
    {
        return true;
    }

    public bool MissionCollectCoin(bool expectedResult)
    {
        return true;
    }

    public bool MissionCopyMoviment(bool expectedResult)
    {
        return true;
    }

    public bool MissionDontMove(bool expectedResult)
    {
        return true;
    }

    public bool MissionMove(bool expectedResult)
    {
        return true;
    }
    public bool MissionPushRival(bool expectedResult)
    {
        return true;
    }

    public bool MissionBomb(bool expectedResult)
    {
        return true;
    }

    public bool MissionStaySquare(bool expectedResult)
    {
        return true;
    }

}
