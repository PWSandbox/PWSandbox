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
        PlayForm playForm = new PlayForm(mapFileLocationTextBox.Text);
        playForm.Show();
    }
}
