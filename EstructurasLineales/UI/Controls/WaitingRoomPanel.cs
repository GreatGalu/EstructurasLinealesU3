using System;
using System.Drawing;
using System.Windows.Forms;
using EstructurasLineales.Application;

namespace EstructurasLineales.UI.Controls
{
    /// <summary>
    /// Panel dedicado exclusivamente a la gestión y despacho de pacientes en la sala de espera (cola de prioridad).
    /// </summary>
    public class WaitingRoomPanel : UserControl
    {
        private readonly WaitingRoom _waitingRoom = new();
        private readonly TextBox _txtName = new() { Width = 130 };
        private readonly NumericUpDown _nudPriority = new() { Minimum = 0, Maximum = 5, Value = 3, Width = 50 };
        private readonly ListBox _lstWaiting = new();
        private readonly ListBox _lstAttended = new();
        private readonly Label _lblMessage = UiBuilder.MessageLabel();

        public WaitingRoomPanel()
        {
            Dock = DockStyle.Fill;

            var bar = UiBuilder.Row(
                UiBuilder.Label("Nombre:"), _txtName,
                UiBuilder.Label("Prioridad:"), _nudPriority,
                UiBuilder.Button("Registrar", (s, e) => RegisterPatient(), 90),
                UiBuilder.Button("Atender siguiente", (s, e) => AttendNextPatient(), 130));

            var lists = UiBuilder.Horizontal(
                (UiBuilder.Group("En espera (orden de atención)", _lstWaiting), UiBuilder.Col(55)),
                (UiBuilder.Group("Atendidos", _lstAttended), UiBuilder.Col(45)));

            var group = UiBuilder.Group("Sala de espera (cola de prioridad)", UiBuilder.Vertical(
                (bar, UiBuilder.Auto),
                (new Label { Text = "0 = crítica … 5 = baja. A igual prioridad, gana quien llegó primero.", AutoSize = true, Padding = new Padding(6, 0, 0, 4) }, UiBuilder.Auto),
                (_lblMessage, UiBuilder.Auto),
                (lists, UiBuilder.Fill)));

            Controls.Add(group);
            RefreshWaitingRoom();
        }

        private void RegisterPatient()
        {
            string name = _txtName.Text.Trim();
            if (name.Length == 0) { UiBuilder.Notify(_lblMessage, "Escribe un nombre.", true); return; }

            _waitingRoom.Register(name, (int)_nudPriority.Value);
            _txtName.Clear();
            UiBuilder.Notify(_lblMessage, $"{name} registrado con prioridad {(int)_nudPriority.Value}.", false);
            RefreshWaitingRoom();
            _txtName.Focus();
        }

        private void AttendNextPatient()
        {
            try
            {
                string name = _waitingRoom.AttendNext();
                _lstAttended.Items.Add(name);
                UiBuilder.Notify(_lblMessage, $"Se atendió a {name}.", false);
            }
            catch (InvalidOperationException ex) { UiBuilder.Notify(_lblMessage, ex.Message, true); }
            RefreshWaitingRoom();
        }

        private void RefreshWaitingRoom()
        {
            _lstWaiting.Items.Clear();
            foreach (var (data, priority) in _waitingRoom.WaitingList)
                _lstWaiting.Items.Add($"[P{priority}] {data}");
        }
    }
}
