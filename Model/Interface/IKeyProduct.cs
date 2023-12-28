namespace Model.Interface
{
    public interface IKeyProduct : IKey
    {
        string name { get; }
        string size { get; }
        string color { get; }
    }
}
