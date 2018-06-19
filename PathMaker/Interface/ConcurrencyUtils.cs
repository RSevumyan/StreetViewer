using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFinder.Interface
{
    delegate void ControlSetTextDelegate(Control control, string text);
    delegate void ControlAvailabilityDelegate(Control control, bool availability);
    delegate void PictureBoxImageSetDelegate(PictureBox pictureBox, Bitmap image);
    delegate void DataGridAddRowDelegate(DataGridView grid, object[] row);
    delegate void DataGridCellValueDelegate(DataGridView grid, int rowIndex, int cellIndex, object value);

    public class ConcurrencyUtils
    {
        public static void SetText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                ControlSetTextDelegate d = new ControlSetTextDelegate(SetText);
                control.Invoke(d, new object[] { control, text });
            }
            else
            {
                control.Text = text;
            }
        }

        public static void SetAvailability(Control control, bool availability)
        {
            if (control.InvokeRequired)
            {
                ControlAvailabilityDelegate d = new ControlAvailabilityDelegate(SetAvailability);
                control.Invoke(d, new object[] { control, availability });
            }
            else
            {
                control.Enabled = availability;
            }
        }

        public static void SetImage(PictureBox pictureBox, Bitmap image)
        {
            if (pictureBox.InvokeRequired)
            {
                PictureBoxImageSetDelegate dlgt = new PictureBoxImageSetDelegate(SetImage);
                pictureBox.Invoke(dlgt, new object[] { pictureBox, image });
            }
            else
            {
                pictureBox.Image = image;  
            }
        }

        public static void SetCellValue(DataGridView grid,int rowIndex,int cellIndex, object value)
        {
            if (grid.InvokeRequired)
            {
                DataGridCellValueDelegate dlgt = new DataGridCellValueDelegate(SetCellValue);
                grid.Invoke(dlgt, new object[] { grid, rowIndex, cellIndex, value});
            }
            else
            {
                grid.Rows[rowIndex].Cells[cellIndex].Value = value;
            }
        }

      
        public static void AddRow(DataGridView grid, object[] row)
        {
            if (grid.InvokeRequired)
            {
                DataGridAddRowDelegate d = new DataGridAddRowDelegate(AddRow);
                grid.Invoke(d, new object[] { grid, row});
            }
            else
            {
                grid.Rows.Add(row);
            }

        }
    }
}
