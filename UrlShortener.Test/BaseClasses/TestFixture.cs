using AutoFixture;

namespace UrlShortener.Test.BaseClasses;

// Todo: refactor to cater different unit tests, not just generic ones
public class TestFixture<T> where T : class
{
    protected readonly Fixture Fixture = new();
    protected readonly T Target;

    protected TestFixture()
    {
        Target = Fixture.Create<T>();
    }
}