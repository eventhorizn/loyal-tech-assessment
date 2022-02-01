namespace review_api.Business;

public interface IMarkovChainTrainer
{
    public void TrainMarkovChain();
    public string GetRandomReview();
}