using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField]
    private int max = 3;
    public int Max => max;
    public int Value { get; private set; }
    public float InvulnerableTime = 0.5f;

    private bool invulnerable = false;

    protected void OnEnable() {
        Value = Max;
    }

    public delegate void DamagedAction(Health source, int damage);
    public event DamagedAction Damaged;
    private Coroutine invulnerableCoroutine = null;
    public void Damage(int amount) {
        if (invulnerable) return;
        Value -= amount;
        if (invulnerableCoroutine != null)
            StopCoroutine(invulnerableCoroutine);
        invulnerable = true;
        invulnerableCoroutine = StartCoroutine(StartInvulnerableTimer());
        Damaged?.Invoke(this, amount);
        if (Value <= 0)
            Dead();
    }
    private IEnumerator StartInvulnerableTimer() {
        yield return new WaitForSeconds(InvulnerableTime);
        invulnerable = false;
    }

    public delegate void DiedAction(Health source);
    public event DiedAction Died;
    public void Dead() {
        Died?.Invoke(this);
    }

}
