namespace Devantler.KindCLI.Tests.Integration.KindCliServiceTests;

public class KindCliServiceTestsBase
{
    internal readonly IKindCliService _kindCliService;

    public KindCliServiceTestsBase() =>
        _kindCliService = new KindCliService();
}
