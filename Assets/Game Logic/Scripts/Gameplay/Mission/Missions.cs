using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public abstract class Missions : NetworkBehaviour
{
    public override abstract void FixedUpdateNetwork();

    protected GameObject prefabObjects;
    protected abstract void StartMission();
    public abstract void CallStartMission();
    public abstract void CallCompleteMission();
    protected abstract void CompleteMission();
}
