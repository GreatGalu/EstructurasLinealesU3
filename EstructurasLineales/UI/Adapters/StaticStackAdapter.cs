using System.Collections.Generic;
using EstructurasLineales.Static;

namespace EstructurasLineales.UI.Adapters
{
    /// <summary>
    /// Adaptador que conecta una StaticStack con la interfaz de usuario,
    /// traduciendo sus estados internos (Top, IsEmpty, IsFull) a información visual.
    /// </summary>
    public class StaticStackAdapter : IStaticStructureAdapter
    {
        private readonly StaticStack<string> _stack;

        public string Name => "Pila estática";
        public string InsertOperationName => "Push";
        public string ExtractOperationName => "Pop";
        public int Capacity => _stack.Capacity;
        public int Count => _stack.Count;

        public StaticStackAdapter(int capacity)
        {
            _stack = new StaticStack<string>(capacity);
        }

        public void Insert(string value) => _stack.Push(value);

        public string Extract() => _stack.Pop();

        public string Peek() => _stack.Peek();

        public ArrayVisualState GetVisualState()
        {
            string[] data = _stack.CopyArray();
            var cells = new string?[data.Length];
            for (int i = 0; i <= _stack.Top; i++)
                cells[i] = data[i];

            var markers = new Dictionary<int, string>();
            if (_stack.Top >= 0)
                markers[_stack.Top] = "top";

            string status = $"top = {_stack.Top}   |   isEmpty = {_stack.IsEmpty()}   |   isFull = {_stack.IsFull()}   |   elementos = {_stack.Count}/{_stack.Capacity}";
            return new ArrayVisualState(cells, markers, status);
        }
    }
}
