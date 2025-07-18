using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace NightLight;

public partial class Form1 : Form
{
    private bool isFilterOn = false;
    private bool isDarkMode = false;

    // For moving the borderless window
    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int HT_CAPTION = 0x2;

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    // For rounded corners
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

    public Form1()
    {
        InitializeComponent(); // Initialize all form components

        try
        {
            this.Icon = new Icon("icon.ico");
            notifyIcon1.Icon = new Icon("icon.ico");
        }
        catch (Exception)
        {
            MessageBox.Show("Icon file 'icon.ico' not found. Please place it in the application's root directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SetupEventHandlers()
    {
        // Make window draggable
        titleBarPanel.MouseDown += titleBarPanel_MouseDown;
        titleLabel.MouseDown += titleBarPanel_MouseDown;

        // Button animations
        Color GetBtnBackColor() => isDarkMode ? Color.FromArgb(65, 75, 95) : Color.FromArgb(225, 235, 245);
        AddHoverEffect(toggleFilterButton, () => isDarkMode ? Color.FromArgb(80, 90, 110) : Color.FromArgb(200, 220, 240), GetBtnBackColor);
        AddHoverEffect(themeToggleButton, () => isDarkMode ? Color.FromArgb(80, 90, 110) : Color.FromArgb(220, 220, 220), GetBtnBackColor);
        AddHoverEffect(closeButton, () => Color.Red, () => titleBarPanel.BackColor);
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        // --- Initialization logic moved to Form_Load for stability ---

        // 1. Apply rounded corners and remove border
        this.FormBorderStyle = FormBorderStyle.None;
        Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        // 2. Apply the visual theme
        ApplyTheme();

        // 3. Set up all event handlers
        SetupEventHandlers();

        // 4. Hide the form from the taskbar on initial load
        this.ShowInTaskbar = false;
    }

    private void toggleFilterButton_Click(object? sender, EventArgs e)
    {
        isFilterOn = !isFilterOn;
        if (isFilterOn)
        {
            GammaController.ApplyFilter(intensityTrackBar.Value);
            toggleFilterButton.Text = "Turn Off Filter";
        }
        else
        {
            GammaController.Reset();
            toggleFilterButton.Text = "Turn On Filter";
        }
    }

    private void intensityTrackBar_Scroll(object? sender, EventArgs e)
    {
        intensityLabel.Text = $"Intensity: {intensityTrackBar.Value}K";
        if (isFilterOn)
        {
            GammaController.ApplyFilter(intensityTrackBar.Value);
        }
    }

    private void copyrightLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo("https://t.me/metela_ru") { UseShellExecute = true });
        }
        catch (Exception)
        {
            MessageBox.Show("Could not open the link.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void themeToggleButton_Click(object? sender, EventArgs e)
    {
        isDarkMode = !isDarkMode;
        ApplyTheme();
        // Re-initialize hover effects to capture the new theme's colors
        SetupEventHandlers();
    }

    private void closeButton_Click(object? sender, EventArgs e)
    {
        this.Hide();
    }

    // --- Tray Icon Logic ---
    private void notifyIcon1_DoubleClick(object? sender, EventArgs e)
    {
        ShowForm();
    }

    private void showToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        ShowForm();
    }

    private void exitToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        GammaController.Reset(); // Reset gamma on exit
        Application.Exit();
    }

    private void ShowForm()
    {
        this.Show();
        this.WindowState = FormWindowState.Normal;
        this.Activate();
    }

    // --- Theming and Animation ---
    private void ApplyTheme()
    {
        Color backColor, textColor, panelColor, btnBackColor, btnBorderColor;

        if (isDarkMode)
        {
            backColor = Color.FromArgb(45, 45, 58);
            textColor = Color.White;
            panelColor = Color.FromArgb(55, 55, 70);
            btnBackColor = Color.FromArgb(65, 75, 95);
            btnBorderColor = Color.FromArgb(90, 90, 110);
        }
        else // Light Mode
        {
            backColor = Color.WhiteSmoke;
            textColor = Color.Black;
            panelColor = Color.White;
            btnBackColor = Color.FromArgb(225, 235, 245);
            btnBorderColor = Color.FromArgb(190, 200, 210);
        }

        // Animate color transition
        AnimateColor(this, this.BackColor, backColor, 150);
        AnimateColor(titleBarPanel, titleBarPanel.BackColor, panelColor, 150);
        AnimateColor(titleLabel, titleLabel.ForeColor, textColor, 150, isForeColor: true);
        AnimateColor(closeButton, closeButton.ForeColor, textColor, 150, isForeColor: true);
        AnimateColor(intensityLabel, intensityLabel.ForeColor, textColor, 150, isForeColor: true);

        // Apply styles
        this.BackColor = backColor;
        titleBarPanel.BackColor = panelColor;
        titleLabel.ForeColor = textColor;
        closeButton.ForeColor = textColor;
        intensityLabel.ForeColor = textColor;

        copyrightLabel.LinkColor = isDarkMode ? Color.LightGray : Color.Gray;

        // Style buttons
        StyleButton(toggleFilterButton, btnBackColor, btnBorderColor, textColor);
        StyleButton(themeToggleButton, btnBackColor, btnBorderColor, textColor);
        themeToggleButton.Text = isDarkMode ? "Light" : "Dark";
    }

    private void StyleButton(Button btn, Color backColor, Color borderColor, Color foreColor)
    {
        btn.BackColor = backColor;
        btn.ForeColor = foreColor;
        btn.FlatAppearance.BorderColor = borderColor;
        btn.FlatAppearance.BorderSize = 1;
    }

    private void AddHoverEffect(Control control, Func<Color> hoverColorFunc, Func<Color> originalColorFunc)
    {
        // To prevent adding handlers multiple times, we store them in the Tag.
        if (control.Tag is Tuple<EventHandler, EventHandler> oldHandlers)
        {
            control.MouseEnter -= oldHandlers.Item1;
            control.MouseLeave -= oldHandlers.Item2;
        }

        EventHandler onEnter = (s, e) => AnimateColor(control, control.BackColor, hoverColorFunc(), 100);
        EventHandler onLeave = (s, e) => AnimateColor(control, control.BackColor, originalColorFunc(), 100);

        control.MouseEnter += onEnter;
        control.MouseLeave += onLeave;

        control.Tag = new Tuple<EventHandler, EventHandler>(onEnter, onLeave);
    }

    private void AnimateColor(Control control, Color from, Color to, int duration, bool isForeColor = false)
    {
        var timer = new System.Windows.Forms.Timer { Interval = 15 };
        int steps = duration / timer.Interval;
        int currentStep = 0;

        int r_step = (to.R - from.R) / steps;
        int g_step = (to.G - from.G) / steps;
        int b_step = (to.B - from.B) / steps;

        timer.Tick += (s, e) =>
        {
            currentStep++;
            int r = from.R + r_step * currentStep;
            int g = from.G + g_step * currentStep;
            int b = from.B + b_step * currentStep;
            Color newColor = Color.FromArgb(Math.Max(0, Math.Min(255, r)), Math.Max(0, Math.Min(255, g)), Math.Max(0, Math.Min(255, b)));

            if (isForeColor)
                control.ForeColor = newColor;
            else
                control.BackColor = newColor;

            if (currentStep >= steps)
                timer.Stop();
        };
        timer.Start();
    }

    // --- Draggable Window Logic ---
    private void titleBarPanel_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }

    private void button_Paint(object? sender, PaintEventArgs e)
    {
        if (sender is Button btn)
        {
            GraphicsPath path = GetRoundPath(new Rectangle(0, 0, btn.Width, btn.Height), 10);
            btn.Region = new Region(path);
        }
    }

    private GraphicsPath GetRoundPath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();
        path.AddArc(rect.Left, rect.Top, radius * 2, radius * 2, 180, 90);
        path.AddLine(rect.Left + radius, rect.Top, rect.Right - radius, rect.Top);
        path.AddArc(rect.Right - radius * 2, rect.Top, radius * 2, radius * 2, 270, 90);
        path.AddLine(rect.Right, rect.Top + radius, rect.Right, rect.Bottom - radius);
        path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
        path.AddLine(rect.Right - radius, rect.Bottom, rect.Left + radius, rect.Bottom);
        path.AddArc(rect.Left, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
        path.CloseFigure();
        return path;
    }
}
