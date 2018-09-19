using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PCC
{
    public partial class PCC : Form
    {
        private static int captureAreaWidth = 20;
        private static int captureAreaHeight = 20;

        private static int captureAreaCenterX = captureAreaWidth / 2;
        private static int captureAreaCenterY = captureAreaHeight / 2;

        private static int pictureBoxZoomFactor = 5;
        private static int pictureBoxWidth = captureAreaWidth * pictureBoxZoomFactor;
        private static int pictureBoxHeight = captureAreaWidth * pictureBoxZoomFactor;

        private static Rectangle captureAreaRectangle = new Rectangle(0, 0, captureAreaWidth, captureAreaHeight);
        private static Rectangle captureAreaZoomedRectangle = new Rectangle(0, 0, pictureBoxWidth, pictureBoxHeight);
        private static Rectangle captureBullsEyeRectangle = new Rectangle((pictureBoxWidth/2) - 3, (pictureBoxHeight / 2) - 3, 6, 6); //9x9

        private static int updatesPerSecond = 25;

        private static Color backgroundRedWarning = Color.FromArgb(255, 220, 220);

        private Timer timer;
        private Bitmap lastCapture;
        private bool capturing;

        public PCC()
        {
            InitializeComponent();

            capturing = false;
            buttonCapture.Enabled = true;

            lastCapture = new Bitmap(pictureBoxWidth, pictureBoxHeight);

            timer = new Timer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = 1000 / updatesPerSecond; // 40 milliseconds or 25 updates/second
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (capturing)
            {
                Bitmap bmp = DesktopBitmapCapture.GetCursorAreaBitmap(captureAreaWidth, captureAreaHeight);
                Graphics g = Graphics.FromImage(lastCapture);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bmp, captureAreaZoomedRectangle, captureAreaRectangle, GraphicsUnit.Pixel);
                g.DrawRectangle(new Pen(Brushes.Black, 1), captureBullsEyeRectangle);
                pictureBoxZoom.Image = lastCapture;

                panelColor.BackColor = bmp.GetPixel(captureAreaCenterX, captureAreaCenterY);

                boxRGB.Text = panelColor.BackColor.R.ToString() + ", " + panelColor.BackColor.G.ToString() + ", " + panelColor.BackColor.B.ToString();
                boxHex.Text = ToHex(panelColor.BackColor.R) + ToHex(panelColor.BackColor.G) + ToHex(panelColor.BackColor.B);
            }
        }

        private string ToHex(int n)
        {
            if (n == 0) return "00";
            n = (int)Math.Max((decimal)0, (decimal)n);
            n = (int)Math.Min((decimal)n, (decimal)255);
            n = (int)Math.Round((decimal)n);
            return "0123456789ABCDEF".Substring((n - n % 16) / 16, 1) + "0123456789ABCDEF".Substring(n % 16, 1);
        }

        private void SelectColorIfCapturing()
        {
            if (capturing)
            {
                capturing = false;
                buttonCapture.Enabled = true;
                buttonCapture.Text = "Capture!";
            }
        }

        private void PCC_Deactivate(object sender, EventArgs e)
        {
            SelectColorIfCapturing();
        }

        private void PCC_Click(object sender, EventArgs e)
        {
            SelectColorIfCapturing();
        }

        private void ButtonCapture_Click(object sender, EventArgs e)
        {
            capturing = true;
            buttonCapture.Enabled = false;
            buttonCapture.Text = "waiting for pixel click";
            boxRGB.Text = "";
            boxHex.Text = "";
        }

        private void LabelHex_Click(object sender, EventArgs e)
        {
            SelectColorIfCapturing();
        }

        private void LabelRGB_Click(object sender, EventArgs e)
        {
            SelectColorIfCapturing();
        }

        private void PanelColor_Click(object sender, EventArgs e)
        {
            SelectColorIfCapturing();
        }

        private void BoxRGB_Click(object sender, EventArgs e)
        {
            SelectColorIfCapturing();
        }

        private void BoxHex_Click(object sender, EventArgs e)
        {
            SelectColorIfCapturing();
        }

        private void PictureBoxZoom_Click(object sender, EventArgs e)
        {
            SelectColorIfCapturing();
        }

        private void BoxRGB_TextChanged(object sender, EventArgs e)
        {
            if (capturing)
            {
                return;
            }

            panelColor.BackColor = Color.LightGray;
            boxHex.Text = "";

            string rgbString = boxRGB.Text.Trim();
            string[] rgbTokens = rgbString.Split(new char[] { ',' });

            if (rgbTokens.Length == 3)
            {
                bool parseSuccess = false;
                int r;
                int g;
                int b;

                parseSuccess = int.TryParse(rgbTokens[0].Trim(), out r);
                parseSuccess = int.TryParse(rgbTokens[1].Trim(), out g);
                parseSuccess = int.TryParse(rgbTokens[2].Trim(), out b);

                if (parseSuccess && RGBValid(r, g, b))
                {
                    SetPanelColor(r, g, b);
                    boxHex.Text = ToHex(panelColor.BackColor.R) + ToHex(panelColor.BackColor.G) + ToHex(panelColor.BackColor.B);
                    boxRGB.BackColor = Color.White;
                }
                else
                {
                    boxRGB.BackColor = backgroundRedWarning;
                }
            }
        }

        private void BoxHex_TextChanged(object sender, EventArgs e)
        {
            if (capturing)
            {
                return;
            }

            string hexString = boxHex.Text.Trim();

            if (hexString.Length != 6)
            {
                ClearPictureAndPanel();
                return;
            }

            if (hexString.IndexOf('#') != -1)
            {
                hexString = hexString.Replace("#", "");
            }

            int r = 0;
            int g = 0;
            int b = 0;

            try
            {
                //#RRGGBB
                r = int.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                g = int.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                b = int.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            catch (Exception)
            {
                boxHex.BackColor = backgroundRedWarning;
                ClearPictureAndPanel();
                return;
            }

            SetPanelColor(r, g, b);
            boxRGB.Text = panelColor.BackColor.R + ", " + panelColor.BackColor.G + ", " + panelColor.BackColor.B;
            boxHex.BackColor = Color.White;
        }

        private bool RGBValid(int r, int g, int b)
        {
            return r >= 0 && r < 256 && g >= 0 && g < 256 && b >= 0 && b < 256;
        }

        private void SetPanelColor(int r, int g, int b)
        {
            Color newColor = Color.FromArgb(r, g, b);

            if (pictureBoxZoom.Image != null)
            {
                Graphics gfx = Graphics.FromImage(pictureBoxZoom.Image);
                gfx.FillRectangle(new SolidBrush(newColor), new Rectangle(0, 0, 100, 100));
            }
            pictureBoxZoom.BackColor = newColor;
            panelColor.BackColor = newColor;
        }

        private void ClearPictureAndPanel()
        {
            if (pictureBoxZoom.Image != null)
            {
                Graphics gfx = Graphics.FromImage(pictureBoxZoom.Image);
                gfx.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(0, 0, 99, 99));
            }
            pictureBoxZoom.BackColor = Color.LightGray;
            panelColor.BackColor = Color.LightGray;
        }
    }
}
