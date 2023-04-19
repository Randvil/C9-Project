using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnergyRegenerator
{
    public void AllowRegeneration(object allower);
    public void ProhibitRegeneration(object prohibitor);
    public void RemoveAllProhibitors();
}
