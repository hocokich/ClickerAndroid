namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            timeSlider.Value = 3000;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new gameScreen((int)timeSlider.Value));
        }

        private void slider_valueChanged(object sender, ValueChangedEventArgs args)
        {
            timeLabel.Text = "Время: " + (int)args.NewValue/100 + " c.";
        }

        private void myBtnClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new myPage());
            
        }
    }
}