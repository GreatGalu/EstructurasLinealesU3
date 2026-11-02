using System;
using System.Windows.Forms;
using EstructurasLineales.Dynamic;

namespace EstructurasLineales.UI.Controls
{
    /// <summary>
    /// Panel dedicado exclusivamente a las operaciones y visualización de la Cola Dinámica.
    /// </summary>
    public class DynamicQueuePanel : UserControl
    {
        private readonly DynamicQueue<string> _queue = new();
        private readonly TextBox _txtValue = new() { Width = 100 };
        private readonly ListBox _lstQueue = new();
        private readonly Label _lblMessage = UiBuilder.MessageLabel();

        public DynamicQueuePanel()
        {
            Dock = DockStyle.Fill;

            var controlBar = UiBuilder.Row(_txtValue,
                UiBuilder.Button("Enqueue", (s, e) => Enqueue(), 80),
                UiBuilder.Button("Dequeue", (s, e) => Dequeue(), 80),
                UiBuilder.Button("Peek", (s, e) => Peek(), 70));

            var group = UiBuilder.Group("Cola dinámica (sin límite de capacidad)", UiBuilder.Vertical(
                (controlBar, UiBuilder.Auto),
                (_lblMessage, UiBuilder.Auto),
                (_lstQueue, UiBuilder.Fill)));

            Controls.Add(group);
        }

        private void Enqueue()
        {
            string value = _txtValue.Text.Trim();
            if (value.Length == 0) { UiBuilder.Notify(_lblMessage, "Escribe un valor.", true); return; }
            _queue.Enqueue(value);
            _lstQueue.Items.Add(value); // el frente se muestra arriba
            _txtValue.Clear();
            UiBuilder.Notify(_lblMessage, $"Enqueue(\"{value}\"). Count = {_queue.Count}.", false);
        }

        private void Dequeue()
        {
            try
            {
                string value = _queue.Dequeue();
                _lstQueue.Items.RemoveAt(0);
                UiBuilder.Notify(_lblMessage, $"Dequeue() devolvió \"{value}\". Count = {_queue.Count}.", false);
            }
            catch (InvalidOperationException ex) { UiBuilder.Notify(_lblMessage, ex.Message, true); }
        }

        private void Peek()
        {
            try { UiBuilder.Notify(_lblMessage, $"Peek() = \"{_queue.Peek()}\".", false); }
            catch (InvalidOperationException ex) { UiBuilder.Notify(_lblMessage, ex.Message, true); }
        }
    }
}
