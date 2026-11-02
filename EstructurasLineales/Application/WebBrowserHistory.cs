using System;
using System.Collections.Generic;
using EstructurasLineales.Dynamic;

namespace EstructurasLineales.Application
{
    /// <summary>
    /// Tarea 3.3: simulación del historial de un navegador web con dos pilas.
    /// _backStack guarda las páginas previas; _forwardStack las que se pueden "re-visitar".
    /// Visitar una página nueva invalida el historial hacia adelante (como un navegador real).
    /// </summary>
    public class WebBrowserHistory
    {
        private readonly DynamicStack<string> _backStack = new();
        private readonly DynamicStack<string> _forwardStack = new();

        public string? CurrentPage { get; private set; }
        public int BackPagesCount => _backStack.Count;
        public int ForwardPagesCount => _forwardStack.Count;
        public IEnumerable<string> BackHistory => _backStack.FromTopToBottom();
        public IEnumerable<string> ForwardHistory => _forwardStack.FromTopToBottom();

        /// <summary>Visita una URL nueva. O(1).</summary>
        public void Visit(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("La URL no puede estar vacía.", nameof(url));
            if (CurrentPage != null) _backStack.Push(CurrentPage);
            CurrentPage = url.Trim();
            _forwardStack.Clear();
        }

        /// <summary>Retrocede una página. Devuelve false si no hay historial. O(1).</summary>
        public bool Back()
        {
            if (_backStack.IsEmpty()) return false;
            _forwardStack.Push(CurrentPage!);
            CurrentPage = _backStack.Pop();
            return true;
        }

        /// <summary>Avanza una página. Devuelve false si no hay historial hacia adelante. O(1).</summary>
        public bool Forward()
        {
            if (_forwardStack.IsEmpty()) return false;
            _backStack.Push(CurrentPage!);
            CurrentPage = _forwardStack.Pop();
            return true;
        }

        public override string ToString() =>
            $"Actual: {CurrentPage ?? "(en blanco)"} | Atrás: {BackPagesCount} | Adelante: {ForwardPagesCount}";
    }
}
