using UnityEngine;
using Zenject;

public class ExtraPointsEffect : Effect
{
    //[Inject] [SerializeField] private ScoreManager _scoreManager;

    public override void Activate()
    {
        //Debug.Log(_scoreManager);
        //_scoreManager.OnCollectPoint += Execute;
    }

    public override void Deactivate()
    {
        //_scoreManager.OnCollectPoint -= Execute;
    }

    private void Execute(int count)
    {
        int random = Random.Range(0, 100);

        if (random < 50)
        {
            //_scoreManager.ChangeScore(count);
        }
    }

    public class Factory : PlaceholderFactory<ExtraPointsEffect>
    {
    }
}
