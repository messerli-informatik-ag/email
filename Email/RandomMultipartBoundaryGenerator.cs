using MimeKit;

namespace Messerli.Email
{
    public sealed class RandomMultipartBoundaryGenerator : IMultipartBoundaryGenerator
    {
        public string GenerateBoundary()
        {
            // MimeKit generates a random boundary for new Multipart entities.
            // We can't call the boundary generator function directly, because it's private.
            var multipart = new Multipart();
            return multipart.Boundary;
        }
    }
}
