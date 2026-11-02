using System;
using System.Collections.Generic;
using EstructurasLineales.Dynamic;

namespace EstructurasLineales.Application
{
    /// <summary>
    /// Complemento de 3.3: despacho de pacientes/clientes con cola de prioridad
    /// (se incluye para demostrar PriorityQueue; el caso principal es el navegador).
    /// </summary>
    public class WaitingRoom
    {
        private readonly PriorityQueue<string> _queue = new();

        public int Count => _queue.Count;

        public void Register(string name, int priority) => _queue.Enqueue(name, priority);

        public string AttendNext() => _queue.Dequeue();

        public bool IsEmpty => _queue.IsEmpty();

        public IEnumerable<(string Data, int Priority)> WaitingList => _queue.Elements();
    }
}
