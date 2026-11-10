namespace MAUIQuiz;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
		pcLanguage.SelectedIndex = Data.LanguageCode;
		slQuestionCount.Value = Data.n;
	}

	private void pcLanguage_SelectedIndexChanged(object? sender, EventArgs e)
	{
		if (pcLanguage.SelectedIndex == -1)
			return;
		Data.LanguageCode = pcLanguage.SelectedIndex;
	}

	private void slQuestionCount_ValueChanged(object? sender, ValueChangedEventArgs e)
	{
		Data.n = (int)slQuestionCount.Value;
	}
}