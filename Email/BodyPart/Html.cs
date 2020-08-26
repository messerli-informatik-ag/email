namespace Messerli.Email.BodyPart
{
    public sealed class Html : IBodyPartVariant
    {
        public Html(string content)
        {
            Content = content;
        }

        public string Content { get; }
    }
}
