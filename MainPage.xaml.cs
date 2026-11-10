namespace MAUIQuiz
{
	public partial class MainPage : ContentPage
	{
		string[] btStart_Texts = { "Start", "Indít" };
		string[] btSubmit_Texts = { "Submit", "Beküld" };
		string[] Page_Titles = { "C# Quiz", "C# teszt" };
		string[] ResultCaption_Texts = { "Result", "Eredmény" };
		string[] Result1_Texts = { "points", "pontot" };
		string[] Result2_Texts = { "earned", "értél el" };
		string[] Error_Texts = { "Error", "Hiba" };
		string[] Download_Error_Texts = { "The questions could not be downloaded.", "A kérdések letöltése nem sikerült." };

		public MainPage()
		{
			InitializeComponent();
		}

		private async void btStart_Clicked(object? sender, EventArgs e)
		{
			slQuestions.Children.Clear();
			List<Item> res;
			try
			{
				res = await TestService.GetTest("https://johanyak.hu/vp/quiz.php", Data.n, Data.LanguageCode);
			}
			catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
			{
				await DisplayAlertAsync(Error_Texts[Data.LanguageCode],
					$"{Download_Error_Texts[Data.LanguageCode]}\n{ex.Message}", "OK");
				return;
			}
			// A beállított kérdésszámot nem írjuk felül, csak a ténylegesen kapott kérdéseket jelenítjük meg
			int count = Math.Min(Data.n, res.Count);
			for (int i = 0; i < count; i++)
			{
				var slActualQuestion = new VerticalStackLayout();
				slQuestions.Children.Add(slActualQuestion);
				slActualQuestion.Children.Add(new Label
				{
					Text = $"{i + 1}. {res[i].Question}"
				});
				foreach (string answer in new[] { res[i].Answer_1, res[i].Answer_2, res[i].Answer_3, res[i].Answer_4 })
				{
					slActualQuestion.Children.Add(new RadioButton
					{
						Content = answer,
						TextColor = Colors.Black,
						FontSize = 14
					});
				}
				slActualQuestion.Children.Add(new Label
				{
					Text = $"{res[i].Key}",
					IsVisible = false
				});
			}
		}

		private async void btSubmit_Clicked(object? sender, EventArgs e)
		{
			int questionCount = slQuestions.Children.Count;
			if (questionCount == 0) return;
			int points = 0;
			foreach (VerticalStackLayout sl in slQuestions.Children.Cast<VerticalStackLayout>())
			{
				string key = ((Label)sl.Children[5]).Text;
				for (int i = 1; i < 5; i++)
				{
					if (sl.Children[i] is not RadioButton rb) return;
					if (key == i.ToString())
					{
						rb.TextColor = Colors.Green;
						if (rb.IsChecked)
							points++;
					}
					else if (rb.IsChecked)
						rb.TextColor = Colors.Red;
				}
			}
			await DisplayAlertAsync(ResultCaption_Texts[Data.LanguageCode],
				$"{points} {Result1_Texts[Data.LanguageCode]} ({(double)points / questionCount:P2}) {Result2_Texts[Data.LanguageCode]}", "OK");
		}

		private async void tbiSettings_Clicked(object? sender, EventArgs e)
		{
			await Navigation.PushAsync(new SettingsPage());
		}

		private void MainPage_NavigatedTo(object? sender, NavigatedToEventArgs e)
		{
			btStart.Text = btStart_Texts[Data.LanguageCode];
			btSubmit.Text = btSubmit_Texts[Data.LanguageCode];
			Title = Page_Titles[Data.LanguageCode];
		}
	}
}
