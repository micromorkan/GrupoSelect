﻿using FluentValidation.Results;

namespace GrupoSelect.Services.Model
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Object { get; set; }
        public IList<ValidationFailure> Errors { get; set; }
    }
}
