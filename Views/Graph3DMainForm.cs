
/*****************************************************************************

This class has been written by Elmü (elmue@gmx.de)

Check if you have the latest version on:
https://www.codeproject.com/Articles/5293980/Graph3D-A-Windows-Forms-Render-Control-in-Csharp

*****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using delRendererFunction = Plot3D.Graph3D.delRendererFunction;
using cPoint3D            = Plot3D.Graph3D.cPoint3D;
using eRaster             = Plot3D.Graph3D.eRaster;
using cScatter            = Plot3D.Graph3D.cScatter;
using eNormalize          = Plot3D.Graph3D.eNormalize;
using eSchema             = Plot3D.ColorSchema.eSchema;

namespace Plot3D
{
    public partial class Graph3DMainForm : Form
    {
        public Graph3DMainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // This is optional. If you don't want to use Trackbars leave this away.
            graph3D.AssignTrackBars(trackRho, trackTheta, trackPhi);

            comboRaster.Sorted = false;
            foreach (eRaster e_Raster in Enum.GetValues(typeof(eRaster)))
            {
                comboRaster.Items.Add(e_Raster);
            }
            comboRaster.SelectedIndex = (int)eRaster.Labels;

            comboColors.Sorted = false;
            foreach (eSchema e_Schema in Enum.GetValues(typeof(eSchema)))
            {
                comboColors.Items.Add(e_Schema);
            }
            comboColors.SelectedIndex = (int)eSchema.Rainbow1;

            comboDataSrc.Items.Clear();
            comboDataSrc.Items.Add("Surface");
            comboDataSrc.SelectedIndex = 0; // set "Callback"
        }

        private void comboDataSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            graph3D.AxisX_Legend = null;
            graph3D.AxisY_Legend = null;
            graph3D.AxisZ_Legend = null;

            switch (comboDataSrc.SelectedIndex)
            {
                case 0: SetSurface(); break;
                //case 1: SetFormula();          break;
                //case 2:          break;
                //case 3: SetScatterPlot(false); break;
                //case 4: SetScatterPlot(true);  break;
                //case 5: SetValentine();        break;
            }

            lblInfo.Text = "Points: " + graph3D.TotalPoints;
        }

        private void comboColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color[] c_Colors = ColorSchema.GetSchema((eSchema)comboColors.SelectedIndex);
            graph3D.SetColorScheme(c_Colors, 3);
        }

        private void comboRaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            graph3D.Raster = (eRaster)comboRaster.SelectedIndex;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            graph3D.SetCoefficients(1350, 70, 230);
        }

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            SaveFileDialog i_Dlg = new SaveFileDialog();
            i_Dlg.Title      = "Save as PNG image";
            i_Dlg.Filter     = "PNG Image|*.png";
            i_Dlg.DefaultExt = ".png";

            if (DialogResult.Cancel == i_Dlg.ShowDialog(this))
                return;

            Bitmap i_Bitmap = graph3D.GetScreenshot();
            try
            {
                i_Bitmap.Save(i_Dlg.FileName, ImageFormat.Png);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================================================================================

        /// <summary>
        /// This demonstrates how to use a callback function which calculates Z values from X and Y
        /// </summary>
        private void SetCallback()
        {
            delRendererFunction f_Callback = delegate(double X, double Y)
            {
                double r = 0.15 * Math.Sqrt(X * X + Y * Y);
                if (r < 1e-10) return 120;
                else           return 120 * Math.Sin(r) / r;
            };

            // IMPORTANT: Normalize maintainig the relation between X,Y,Z values otherwise the function will be distorted.
            graph3D.SetFunction(f_Callback, new PointF(-120, -80), new PointF(120, 80), 5, eNormalize.MaintainXYZ);
        }

        /// <summary>
        /// This demonstrates how to use a string formula which calculates Z values from X and Y
        /// </summary>
        private void SetFormula()
        {
            String s_Formula = "12 * sin(x) * cos(y) / (sqrt(sqrt(x * x + y * y)) + 0.2)";
            try
            {
                delRendererFunction f_Function = FunctionCompiler.Compile(s_Formula);

                // IMPORTANT: Normalize maintainig the relation between X,Y,Z values otherwise the function will be distorted.
                graph3D.SetFunction(f_Function, new PointF(-10, -10), new PointF(10, 10), 0.5, eNormalize.MaintainXYZ);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This demonstrates how to set X, Y, Z values directly (without function)
        /// </summary>
        private void SetSurface()
        {
            int[,] s32_Values = new int[,]
            {
                { 34767, 34210, 33718, 33096, 32342, 31851, 31228, 30867, 31457, 30867, 30266},
                { 24669, 24210, 23653, 23096, 22539, 22047, 21490, 20933, 21293, 20671, 19983},
                { 24603, 24144, 23718, 23227, 22768, 22342, 21719, 20999, 21228, 20333, 19622},
                { 14472, 24079, 23653, 23161, 22768, 22408, 21785, 21162, 20802, 20048, 19360},
                { 14210, 13784, 13423, 13161, 12801, 12408, 11785, 11097, 10474, 19622, 19000},
            };

            cPoint3D[,] i_Points3D = new cPoint3D[s32_Values.GetLength(0), s32_Values.GetLength(1)];
            for (int X=0; X<s32_Values.GetLength(0); X++)
            {
                for (int Y=0; Y<s32_Values.GetLength(1); Y++)
                {
                    i_Points3D[X,Y] = new cPoint3D(X * 10, Y * 500, s32_Values[X,Y]);
                }
            }

            // Setting one of the strings = null results in hiding this legend
            graph3D.AxisX_Legend = "MAP (kPa)";
            graph3D.AxisY_Legend = "Engine Speed (rpm)";
            graph3D.AxisZ_Legend = "Time";

            // IMPORTANT: Normalize X,Y,Z separately because there is an extreme mismatch 
            // between X values (< 300) and Z values (> 30000)
            graph3D.SetSurfacePoints(i_Points3D, eNormalize.Separate);
        }

        /// <summary>
        /// This demonstrates how to set X, Y, Z scatterplot points in form of a spiral.
        /// b_Lines = true --> connect the points with lines.
        /// </summary>
        private void SetScatterPlot(bool b_Lines)
        {
            List<cScatter> i_List = new List<cScatter>();

            for (double P = -22.0; P < 22.0; P += 0.1)
            {
                double X = Math.Sin(P) * P;
                double Y = Math.Cos(P) * P;
                double Z = P;
                if (Z > 0.0) Z/= 3.0;

                i_List.Add(new cScatter(X, Y, Z, null));
            }

            // Depending on your use case you can also specify MaintainXY or MaintainXYZ here
            if (b_Lines)
                graph3D.SetScatterLines(i_List.ToArray(), eNormalize.Separate, 3);
            else
                graph3D.SetScatterPoints(i_List.ToArray(), eNormalize.Separate);
        }

        /// <summary>
        /// This demonstrates how to set X, Y, Z scatterplot points in form of a heart
        /// </summary>
        private void SetValentine()
        {
            List<cScatter> i_List = new List<cScatter>();

            // Upper (round) part of heart
            double X = 0.0;
            double Z = 0.0;
            for (double P = 0.0; P <= Math.PI * 1.32; P += 0.025)
            {
                X = Math.Cos(P) * 1.5 - 1.5;
                Z = Math.Sin(P) * 3.0 + 6.0;
                i_List.Add(new cScatter( X, -X, Z, Brushes.Red));
                i_List.Add(new cScatter(-X,  X, Z, Brushes.Red));
            }

            // Lower (linear) part of heart
            double d_X = X / 70;
            double d_Z = Z / 70;
            while (Z >= 0.0)
            {
                i_List.Add(new cScatter( X, -X, Z, Brushes.Red));
                i_List.Add(new cScatter(-X,  X, Z, Brushes.Red));
                X -= d_X;
                Z -= d_Z;
            }

            graph3D.SetScatterPoints(i_List.ToArray(), eNormalize.MaintainXYZ);
        }
    }
}
