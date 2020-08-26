namespace Messerli.Email
{
    public interface IMultipartBoundaryGenerator
    {
        string GenerateBoundary();
    }
}
