namespace Devantler.KindCLI.Tests.KindCliServiceTests;

[Collection("KindCliServiceTests")]
public class KindCliServiceTestsBase
{
    protected readonly IKindCliService _kindCliService;

    public KindCliServiceTestsBase()
    {
        _kindCliService = new KindCliService();
    }
}
