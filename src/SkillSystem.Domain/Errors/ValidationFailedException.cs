﻿using System.Runtime.Serialization;

namespace SkillSystem.Domain.Errors;

public class ValidationFailedException : BaseException
{
    public ValidationFailedException() { }
    public ValidationFailedException(string message) : base(message) { }
    public ValidationFailedException(string message, Exception inner) : base(message, inner) { }
    protected ValidationFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
