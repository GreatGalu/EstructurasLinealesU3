using System;
using System.Drawing;
using System.Windows.Forms;

namespace EstructurasLineales.UI
{
    /// <summary>
    /// Fábrica y constructor de controles comunes para el diseño por código (Layout & Controls).
    /// Centraliza la creación homogénea de botones, etiquetas, filas y tablas de distribución.
    /// </summary>
    public static class UiBuilder
    {
        public static RowStyle Auto => new RowStyle(SizeType.AutoSize);
        public static RowStyle Fill => new RowStyle(SizeType.Percent, 100f);
        public static RowStyle Pct(float percentage) => new RowStyle(SizeType.Percent, percentage);
        public static RowStyle Px(int height) => new RowStyle(SizeType.Absolute, height);
        public static ColumnStyle Col(float percentage) => new ColumnStyle(SizeType.Percent, percentage);

        public static Button Button(string text, EventHandler onClick, int minWidth = 110)
        {
            var btn = new Button
            {
                Text = text,
                AutoSize = true,
                MinimumSize = new Size(minWidth, 28),
                Margin = new Padding(3)
            };
            btn.Click += onClick;
            return btn;
        }

        public static Label Label(string text) => new Label
        {
            Text = text,
            AutoSize = true,
            Margin = new Padding(6, 8, 3, 3)
        };

        public static FlowLayoutPanel Row(params Control[] controls)
        {
            var row = new FlowLayoutPanel { AutoSize = true, WrapContents = true };
            row.Controls.AddRange(controls);
            return row;
        }

        public static GroupBox Group(string title, Control content)
        {
            var group = new GroupBox { Text = title, Dock = DockStyle.Fill, Padding = new Padding(8) };
            content.Dock = DockStyle.Fill;
            group.Controls.Add(content);
            return group;
        }

        /// <summary>Apila controles en una columna; cada fila indica su estilo de alto.</summary>
        public static TableLayoutPanel Vertical(params (Control Control, RowStyle Style)[] rows)
        {
            var table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = rows.Length };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            foreach (var (control, style) in rows)
            {
                table.RowStyles.Add(style);
                control.Dock = DockStyle.Fill;
                table.Controls.Add(control);
            }
            return table;
        }

        /// <summary>Coloca controles lado a lado; cada columna indica su estilo de ancho.</summary>
        public static TableLayoutPanel Horizontal(params (Control Control, ColumnStyle Style)[] columns)
        {
            var table = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 1, ColumnCount = columns.Length };
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            foreach (var (control, style) in columns)
            {
                table.ColumnStyles.Add(style);
                control.Dock = DockStyle.Fill;
                table.Controls.Add(control);
            }
            return table;
        }

        public static Label MessageLabel() => new Label { AutoSize = true, Padding = new Padding(6, 2, 0, 2) };

        public static void Notify(Label label, string text, bool isError)
        {
            label.ForeColor = isError ? Color.Firebrick : Color.SeaGreen;
            label.Text = (isError ? "⚠ " : "✔ ") + text;
        }
    }
}
