using System.Diagnostics;
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

    public Form1()
    {
        InitializeComponent();
        ApplyTheme();
        SetupEventHandlers();
        try
        {
            this.Icon = new Icon("icon.ico");
            notifyIcon1.Icon = new Icon("icon.ico");
        }
        catch (Exception ex)
        {
            // Handle exception if icon.ico is not found
            MessageBox.Show("Icon file 'icon.ico' not found. Please place it in the application's root directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SetupEventHandlers()
    {
        // Make window draggable
        titleBarPanel.MouseDown += titleBarPanel_MouseDown;
        titleLabel.MouseDown += titleBarPanel_MouseDown;

        // Button animations
        AddHoverEffect(toggleFilterButton, () => isDarkMode ? Color.FromArgb(80, 90, 110) : Color.FromArgb(200, 220, 240));
        AddHoverEffect(themeToggleButton, () => isDarkMode ? Color.FromArgb(80, 90, 110) : Color.FromArgb(220, 220, 220));
        AddHoverEffect(closeButton, () => Color.Red);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // On load, hide the form from the taskbar
        this.ShowInTaskbar = false;
    }

    private void toggleFilterButton_Click(object sender, EventArgs e)
    {
        isFilterOn = !isFilterOn;
        if (isFilterOn)
        {
            GammaController.ApplyFilter(4500); // Apply a noticeable filter
            toggleFilterButton.Text = "Turn Off Filter";
        }
        else
        {
            GammaController.Reset();
            toggleFilterButton.Text = "Turn On Filter";
        }
    }

    private void copyrightLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo("https://t.me/metela_ru") { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MessageBox.Show("Could not open the link.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void themeToggleButton_Click(object sender, EventArgs e)
    {
        isDarkMode = !isDarkMode;
        ApplyTheme();
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        this.Hide();
    }

    // --- Tray Icon Logic ---
    private void notifyIcon1_DoubleClick(object sender, EventArgs e)
    {
        ShowForm();
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
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

        // Apply styles
        this.BackColor = backColor;
        titleBarPanel.BackColor = panelColor;
        titleLabel.ForeColor = textColor;
        closeButton.ForeColor = textColor;

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

    private void AddHoverEffect(Control control, Func<Color> hoverColorFunc)
    {
        Color originalColor = control.BackColor;
        control.MouseEnter += (s, e) => AnimateColor(control, control.BackColor, hoverColorFunc(), 100);
        control.MouseLeave += (s, e) => AnimateColor(control, control.BackColor, originalColor, 100);
    }

    private async void AnimateColor(Control control, Color from, Color to, int duration, bool isForeColor = false)
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
    private void titleBarPanel_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }
}

