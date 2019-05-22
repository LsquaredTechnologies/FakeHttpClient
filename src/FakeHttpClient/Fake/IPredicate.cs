namespace System.Net.Http.Fake
{
    public interface IPredicate
    {
        bool Match(HttpRequestMessage req);
    }
}