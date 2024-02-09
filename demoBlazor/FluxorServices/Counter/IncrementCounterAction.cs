namespace demoBlazor.FluxorServices.Counter
{
    public class IncrementCounterAction
    {
        public int Amount { get; }

        public IncrementCounterAction(int amount = 1)
        {
            Amount = amount;
        }
    }

}
