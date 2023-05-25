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
    void Awake()
    {
        freezeObject = GetComponent<IFreezeMechanic>();
        useRate = freezeDuration + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextUse)
        {
            nextUse = Time.time + useRate;
            StartCoroutine(TimedFreeze(freezeDuration));
        }
    }
    
    public IEnumerator TimedFreeze(float freezeDuration)
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
