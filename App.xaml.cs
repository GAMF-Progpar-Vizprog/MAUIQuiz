namespace MAUIQuiz
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		}

		protected override Window CreateWindow(IActivationState? activationState)
		{
			// A főoldalt NavigationPage-be csomagoljuk, így működik a
			// Navigation.PushAsync a beállítások oldal megnyitásához.
			// A méret asztali platformon (Windows, Mac Catalyst) érvényes,
			// eszközfüggetlen egységben; mobilon nincs hatása.
			return new Window(new NavigationPage(new MainPage()))
			{
				Width = 330,
				Height = 700
			};
		}
	}
}
