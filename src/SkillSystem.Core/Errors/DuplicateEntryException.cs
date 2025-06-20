using System.Runtime.Serialization;

namespace SkillSystem.Core.Errors;

[Serializable]
public class DuplicateEntryException : BaseException //NotAllowedException
{
    public DuplicateEntryException() { }
    public DuplicateEntryException(String message) : base(message) { }
    public DuplicateEntryException(String message, Exception inner) : base(message, inner) { }
    protected DuplicateEntryException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}