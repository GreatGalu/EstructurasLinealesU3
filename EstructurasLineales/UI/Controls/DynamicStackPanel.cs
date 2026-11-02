using System;
using System.Windows.Forms;
using EstructurasLineales.Dynamic;

namespace EstructurasLineales.UI.Controls
{
    /// <summary>
    /// Panel dedicado exclusivamente a las operaciones y visualización de la Pila Dinámica.
    /// </summary>
    public class DynamicStackPanel : UserControl
    {
        private readonly DynamicStack<string> _stack = new();
        private readonly TextBox _txtValue = new() { Width = 100 };
        private readonly ListBox _lstStack = new();
        private readonly Label _lblMessage = UiBuilder.MessageLabel();

        public DynamicStackPanel()
        {
            Dock = DockStyle.Fill;

            var controlBar = UiBuilder.Row(_txtValue,
                UiBuilder.Button("Push", (s, e) => Push(), 70),
                UiBuilder.Button("Pop", (s, e) => Pop(), 70),
                UiBuilder.Button("Peek", (s, e) => Peek(), 70));

            var group = UiBuilder.Group("Pila dinámica (sin límite de capacidad)", UiBuilder.Vertical(
                (controlBar, UiBuilder.Auto),
                (_lblMessage, UiBuilder.Auto),
                (_lstStack, UiBuilder.Fill)));

            Controls.Add(group);
        }

        private void Push()
        {
            string value = _txtValue.Text.Trim();
            if (value.Length == 0) { UiBuilder.Notify(_lblMessage, "Escribe un valor.", true); return; }
            _stack.Push(value);
            _lstStack.Items.Insert(0, value); // el tope se muestra arriba
            _txtValue.Clear();
            UiBuilder.Notify(_lblMessage, $"Push(\"{value}\"). Count = {_stack.Count}.", false);
        }

        private void Pop()
        {
            try
            {
                string value = _stack.Pop();
                _lstStack.Items.RemoveAt(0);
                UiBuilder.Notify(_lblMessage, $"Pop() devolvió \"{value}\". Count = {_stack.Count}.", false);
            }
            catch (InvalidOperationException ex) { UiBuilder.Notify(_lblMessage, ex.Message, true); }
        }

        private void Peek()
        {
            try { UiBuilder.Notify(_lblMessage, $"Peek() = \"{_stack.Peek()}\".", false); }
            catch (InvalidOperationException ex) { UiBuilder.Notify(_lblMessage, ex.Message, true); }
        }
    }
}
