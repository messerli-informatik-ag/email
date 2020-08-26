namespace Messerli.Email.BodyPart
{
    public sealed class Plain : IBodyPartVariant
    {
        public Plain(string content)
        {
            Content = content;
        }

        public string Content { get; }
    }
}
