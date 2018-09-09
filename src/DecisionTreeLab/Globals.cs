namespace DecisionTreeLab
{
    public class Globals<TContext>
    {
        public Globals(TContext context, dynamic[] variables)
        {
            this.Context = context;
            this.Variables = variables;
        }

        public TContext Context { get; }

        public dynamic[] Variables { get; }
    }
}
