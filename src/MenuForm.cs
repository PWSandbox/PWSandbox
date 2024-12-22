// This file is a part of PWSandbox ( https://github.com/yarb00/PWSandbox )

namespace PWSandbox;

public partial class MenuForm : Form
{
    public MenuForm()
    {
        InitializeComponent();
    }

    private void LoadMap(object sender, EventArgs e)
    {
        if (mapFileLocationTextBox.Text != "")
        {
            PlayForm playForm = new PlayForm(mapFileLocationTextBox.Text);
            if (!playForm.IsDisposed) playForm.Show();
        }
        else
        {
            MessageBox.Show(
                "ERROR loading PWSandbox:"
                + "\nmap file location is NOT specified!",
                "PWSandbox",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1
            );
            return;
        }
    }
}
