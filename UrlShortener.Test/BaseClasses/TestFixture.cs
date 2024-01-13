using AutoFixture;

namespace UrlShortener.Test.BaseClasses;

public class TestFixture<T> where T : class
{
    protected readonly Fixture Fixture = new();
    protected readonly T Target;

    protected TestFixture()
    {
        Target = Fixture.Create<T>();
    }
}