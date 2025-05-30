// File: SeuProjetoNET/Exceptions/RecursoNaoEncontradoException.cs
using System;

namespace SeuProjetoNET.Exceptions
{
    public class RecursoNaoEncontradoException : Exception
    {
        public RecursoNaoEncontradoException() : base() { }

        public RecursoNaoEncontradoException(string message) : base(message) { }

        public RecursoNaoEncontradoException(string message, Exception innerException) : base(message, innerException) { }
    }
}