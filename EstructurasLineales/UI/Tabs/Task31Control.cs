using System;
using System.Drawing;
using System.Windows.Forms;
using EstructurasLineales.UI.Adapters;
using EstructurasLineales.UI.Controls;

namespace EstructurasLineales.UI.Tabs
{
    /// <summary>
    /// Pestaña de la Tarea 3.1: coordina la interacción visual con las estructuras estáticas
    /// (StaticStack, StaticLinearQueue y StaticCircularQueue) a través del patrón Adapter.
    /// </summary>
    public class Task31Control : UserControl
    {
        private readonly ComboBox _cmbStructure = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 190 };
        private readonly NumericUpDown _nudCapacity = new() { Minimum = 1, Maximum = 20, Value = 5, Width = 60 };
        private readonly TextBox _txtValue = new() { Width = 120 };
        private readonly Button _btnInsert;
        private readonly Button _btnExtract;
        private readonly Button _btnPeek;
        private readonly ArrayViewer _viewer = new();
        private readonly Label _lblStatus = new() { AutoSize = true, Padding = new Padding(6, 4, 0, 4), Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
        private readonly Label _lblMessage = UiBuilder.MessageLabel();
        private readonly ListBox _lstLog = new();

        private IStaticStructureAdapter _currentStructure = null!;

        public Task31Control()
        {
            Dock = DockStyle.Fill;
            _cmbStructure.Items.AddRange(new object[] { "Pila estática", "Cola simple estática", "Cola circular estática" });
            _cmbStructure.SelectedIndex = 0;

            _btnInsert = UiBuilder.Button("Push", (s, e) => Insert());
            _btnExtract = UiBuilder.Button("Pop", (s, e) => Extract());
            _btnPeek = UiBuilder.Button("Peek", (s, e) => Peek());
            var btnCreate = UiBuilder.Button("Crear / reiniciar", (s, e) => CreateStructure(), 130);

            _cmbStructure.SelectedIndexChanged += (s, e) => CreateStructure();
            _txtValue.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                Insert();
            };

            Controls.Add(UiBuilder.Vertical(
                (UiBuilder.Row(UiBuilder.Label("Estructura:"), _cmbStructure, UiBuilder.Label("Capacidad:"), _nudCapacity, btnCreate), UiBuilder.Auto),
                (UiBuilder.Row(UiBuilder.Label("Valor:"), _txtValue, _btnInsert, _btnExtract, _btnPeek), UiBuilder.Auto),
                (_lblStatus, UiBuilder.Auto),
                (_lblMessage, UiBuilder.Auto),
                (_viewer, UiBuilder.Px(120)),
                (UiBuilder.Group("Bitácora de operaciones", _lstLog), UiBuilder.Fill)));

            CreateStructure();
        }

        private void CreateStructure()
        {
            int capacity = (int)_nudCapacity.Value;
            _currentStructure = _cmbStructure.SelectedIndex switch
            {
                0 => new StaticStackAdapter(capacity),
                1 => new StaticLinearQueueAdapter(capacity),
                _ => new StaticCircularQueueAdapter(capacity)
            };

            _btnInsert.Text = _currentStructure.InsertOperationName;
            _btnExtract.Text = _currentStructure.ExtractOperationName;

            _lstLog.Items.Clear();
            Notify($"{_currentStructure.Name} creada con capacidad {capacity}.", false);
            RefreshView();
        }

        private void Insert()
        {
            string value = _txtValue.Text.Trim();
            if (value.Length == 0)
            {
                Notify("Escribe un valor antes de insertar.", true);
                return;
            }

            try
            {
                _currentStructure.Insert(value);
                Notify($"{_currentStructure.InsertOperationName}(\"{value}\") realizado.", false);
                _txtValue.Clear();
            }
            catch (InvalidOperationException ex)
            {
                Notify(ex.Message, true);
            }

            RefreshView();
            _txtValue.Focus();
        }

        private void Extract()
        {
            try
            {
                string value = _currentStructure.Extract();
                Notify($"{_currentStructure.ExtractOperationName}() devolvió \"{value}\".", false);
            }
            catch (InvalidOperationException ex)
            {
                Notify(ex.Message, true);
            }

            RefreshView();
        }

        private void Peek()
        {
            try
            {
                string value = _currentStructure.Peek();
                Notify($"Peek() = \"{value}\" (no se elimina).", false);
            }
            catch (InvalidOperationException ex)
            {
                Notify(ex.Message, true);
            }
        }

        private void RefreshView()
        {
            var visualState = _currentStructure.GetVisualState();
            _viewer.Show(visualState);
            _lblStatus.Text = visualState.StatusDescription;
        }

        private void Notify(string text, bool isError)
        {
            UiBuilder.Notify(_lblMessage, text, isError);
            _lstLog.Items.Insert(0, $"{DateTime.Now:HH:mm:ss}  {(isError ? "ERROR" : "OK")}  {text}");
        }
    }
}
