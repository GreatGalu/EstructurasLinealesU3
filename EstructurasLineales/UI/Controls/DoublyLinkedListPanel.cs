using System;
using System.Windows.Forms;
using EstructurasLineales.Dynamic;

namespace EstructurasLineales.UI.Controls
{
    /// <summary>
    /// Panel dedicado exclusivamente a las operaciones y visualización de la Lista Doblemente Enlazada.
    /// </summary>
    public class DoublyLinkedListPanel : UserControl
    {
        private readonly DoublyLinkedList<string> _list = new();
        private readonly TextBox _txtValue = new() { Width = 110 };
        private readonly NumericUpDown _nudIndex = new() { Minimum = 0, Maximum = 999, Width = 55 };
        private readonly LinkedListViewer _viewer = new();
        private readonly Label _lblStatus = UiBuilder.MessageLabel();
        private readonly Label _lblMessage = UiBuilder.MessageLabel();

        public DoublyLinkedListPanel()
        {
            Dock = DockStyle.Fill;

            var row1 = UiBuilder.Row(
                UiBuilder.Label("Valor:"), _txtValue,
                UiBuilder.Label("Índice:"), _nudIndex,
                UiBuilder.Button("Agregar inicio", (s, e) => ExecuteWithValue(_list.AddFirst, "AddFirst")),
                UiBuilder.Button("Agregar final", (s, e) => ExecuteWithValue(_list.AddLast, "AddLast")),
                UiBuilder.Button("Insertar en índice", (s, e) => ExecuteWithValue(v => _list.InsertAt((int)_nudIndex.Value, v), "InsertAt"), 130));

            var row2 = UiBuilder.Row(
                UiBuilder.Button("Eliminar inicio", (s, e) => ExecuteListAction(() => $"RemoveFirst devolvió \"{_list.RemoveFirst()}\".")),
                UiBuilder.Button("Eliminar final", (s, e) => ExecuteListAction(() => $"RemoveLast devolvió \"{_list.RemoveLast()}\".")),
                UiBuilder.Button("Eliminar valor", (s, e) => WithValue(v => _list.Remove(v)
                    ? $"Se eliminó la primera aparición de \"{v}\"."
                    : $"El valor \"{v}\" no existe en la lista.")),
                UiBuilder.Button("Buscar valor", (s, e) => WithValue(v => _list.Contains(v)
                    ? $"\"{v}\" Sí está en la lista."
                    : $"\"{v}\" NO está en la lista.")),
                UiBuilder.Button("Leer [índice]", (s, e) => ExecuteListAction(() => $"list[{(int)_nudIndex.Value}] = \"{_list[(int)_nudIndex.Value]}\".")));

            var groupList = UiBuilder.Group("Lista doblemente enlazada", UiBuilder.Vertical(
                (row1, UiBuilder.Auto),
                (row2, UiBuilder.Auto),
                (_viewer, UiBuilder.Px(115)),
                (_lblStatus, UiBuilder.Auto),
                (_lblMessage, UiBuilder.Auto)));

            Controls.Add(groupList);
            RefreshList();
        }

        private void ExecuteWithValue(Action<string> operation, string operationName)
        {
            WithValue(value =>
            {
                operation(value);
                return $"{operationName}(\"{value}\") realizado.";
            });
        }

        private void WithValue(Func<string, string> action)
        {
            string value = _txtValue.Text.Trim();
            if (value.Length == 0) { UiBuilder.Notify(_lblMessage, "Escribe un valor.", true); return; }
            ExecuteListAction(() => action(value));
        }

        private void ExecuteListAction(Func<string> action)
        {
            try { UiBuilder.Notify(_lblMessage, action(), false); }
            catch (InvalidOperationException ex) { UiBuilder.Notify(_lblMessage, ex.Message, true); }
            catch (ArgumentOutOfRangeException) { UiBuilder.Notify(_lblMessage, "Índice fuera de rango.", true); }
            RefreshList();
        }

        private void RefreshList()
        {
            _viewer.Show(_list);
            _lblStatus.Text = $"Count = {_list.Count}   |   {_list}";
        }
    }
}
