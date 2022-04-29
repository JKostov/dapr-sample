namespace Products.Models.Dapr;

public class CreateState<T>
{
    public string Key { get; set; }

    public T Value { get; set; }
}
