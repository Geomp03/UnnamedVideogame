using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMechanic : MonoBehaviour
{
    [SerializeField] private float freezeDuration;
    private IFreezeMechanic freezeObject;

    private float useRate;
    private float nextUse = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        freezeObject = GetComponent<IFreezeMechanic>();
        useRate = freezeDuration + 0.5f;

        InputManager.Instance.OnFreezeAction += InputManager_OnFreezeAction;
    }

    private void InputManager_OnFreezeAction(object sender, System.EventArgs e)
    {
        if (Time.time > nextUse)
        {
            nextUse = Time.time + useRate;
            StartCoroutine(TimedFreeze(freezeDuration));
        }
    }
    
    private IEnumerator TimedFreeze(float freezeDuration)
    {
        freezeObject.FreezeObject();
        yield return new WaitForSeconds(freezeDuration);
        freezeObject.UnfreezeObject();
    }
}

public interface IFreezeMechanic
{
    void FreezeObject();
    void UnfreezeObject();
}
