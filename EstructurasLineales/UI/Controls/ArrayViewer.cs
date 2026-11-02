using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EstructurasLineales.UI.Adapters;

namespace EstructurasLineales.UI.Controls
{
    /// <summary>Dibuja un arreglo como una fila de celdas indexadas, con marcas (top, front, rear).</summary>
    public class ArrayViewer : Panel
    {
        private string?[] _cells = Array.Empty<string?>();
        private readonly Dictionary<int, string> _markers = new();

        public ArrayViewer()
        {
            DoubleBuffered = true;
            AutoScroll = true;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
        }

        /// <param name="cells">Valor por posición; null indica celda vacía.</param>
        /// <param name="markers">Etiqueta por índice (por ejemplo, "top" o "front/rear").</param>
        public void Show(string?[] cells, Dictionary<int, string> markers)
        {
            _cells = cells;
            _markers.Clear();
            foreach (var pair in markers) _markers[pair.Key] = pair.Value;
            AutoScrollMinSize = new Size(20 + cells.Length * 64, 0);
            Invalidate();
        }

        /// <summary>Muestra el estado visual provisto por un adaptador de estructura estática.</summary>
        public void Show(ArrayVisualState state)
        {
            Show(state.Cells, state.Markers);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.TranslateTransform(AutoScrollPosition.X, 0);
            using var font = new Font("Segoe UI", 10f);
            using var smallFont = new Font("Segoe UI", 8.5f);
            using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            for (int i = 0; i < _cells.Length; i++)
            {
                var cell = new Rectangle(10 + i * 64, 34, 60, 40);
                bool occupied = _cells[i] != null;
                using (var brush = new SolidBrush(occupied ? Color.LightSkyBlue : Color.WhiteSmoke))
                    g.FillRectangle(brush, cell);
                g.DrawRectangle(Pens.Black, cell);

                if (occupied) g.DrawString(_cells[i], font, Brushes.Black, cell, format);
                g.DrawString(i.ToString(), smallFont, Brushes.Gray,
                    new Rectangle(cell.X, cell.Bottom + 2, cell.Width, 16), format);

                if (_markers.TryGetValue(i, out string? marker))
                    g.DrawString(marker, smallFont, Brushes.Firebrick,
                        new Rectangle(cell.X - 6, 8, cell.Width + 12, 22), format);
            }
        }
    }
}
