using UnityEngine;
using Random = UnityEngine.Random;

public class ReproductionManager : MonoBehaviour
{
    [SerializeField] private float _minDelay = 10f;
    [SerializeField] private float _maxDelay = 50f;

    [SerializeField] private float _delay;

    private void Start()
    {
        _delay = GetRandomDelay();
    }

    private float GetRandomDelay()
    {
        return Random.Range(_minDelay, _maxDelay);
    }

    public void Reproduce()
    {

    }

    private void Update()
    {
        _delay -= Time.deltaTime;
        if (_delay <= 0)
        {
            Reproduce();
            _delay = GetRandomDelay();
        }
    }
}
