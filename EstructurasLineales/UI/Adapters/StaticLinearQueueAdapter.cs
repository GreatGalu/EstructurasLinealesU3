using System.Collections.Generic;
using EstructurasLineales.Static;

namespace EstructurasLineales.UI.Adapters
{
    /// <summary>
    /// Adaptador que conecta una StaticLinearQueue con la interfaz de usuario,
    /// traduciendo sus estados internos (Front, Rear) a información visual.
    /// </summary>
    public class StaticLinearQueueAdapter : IStaticStructureAdapter
    {
        private readonly StaticLinearQueue<string> _queue;

        public string Name => "Cola simple estática";
        public string InsertOperationName => "Enqueue";
        public string ExtractOperationName => "Dequeue";
        public int Capacity => _queue.Capacity;
        public int Count => _queue.Count;

        public StaticLinearQueueAdapter(int capacity)
        {
            _queue = new StaticLinearQueue<string>(capacity);
        }

        public void Insert(string value) => _queue.Enqueue(value);

        public string Extract() => _queue.Dequeue();

        public string Peek() => _queue.Peek();

        public ArrayVisualState GetVisualState()
        {
            string[] data = _queue.CopyArray();
            var cells = new string?[data.Length];
            for (int i = _queue.Front; i <= _queue.Rear; i++)
                cells[i] = data[i];

            var markers = new Dictionary<int, string>();
            if (!_queue.IsEmpty())
            {
                AddMarker(markers, _queue.Front, "front");
                AddMarker(markers, _queue.Rear, "rear");
            }

            string status = $"front = {_queue.Front}, rear = {_queue.Rear}   |   isEmpty = {_queue.IsEmpty()}   |   isFull = {_queue.IsFull()}   |   elementos = {_queue.Count}/{_queue.Capacity}";
            return new ArrayVisualState(cells, markers, status);
        }

        private static void AddMarker(Dictionary<int, string> markers, int index, string tag)
        {
            markers[index] = markers.TryGetValue(index, out string? current) ? current + "/" + tag : tag;
        }
    }
}
