using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Graphics.Platform;

namespace MauiApp1;

public partial class gameScreen : ContentPage
{
    //uTime = user time

    IDispatcherTimer timer;
    CController controller;

    int spawnRate = 100;
    double startTime;
    Size sceneSize;


    public class Drawable : IDrawable
    {
        List<CObject> objs;

        public Drawable(List<CObject> sad)
        {
            this.objs = sad;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.BlueViolet;
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 4;


            for (int i = 0; i < objs.Count; i++)
            {
                canvas.FillEllipse((float)objs[i].position.X, (float)objs[i].position.Y, (float)objs[i].size.Width, (float)objs[i].size.Height);
                canvas.DrawEllipse((float)objs[i].position.X, (float)objs[i].position.Y, (float)objs[i].size.Width, (float)objs[i].size.Height);
            }
        }

    }

    public gameScreen(int uTime)
	{
		InitializeComponent();

        time.Text = "Время: " + uTime;

        //Создание таймера
        timer = Dispatcher.CreateTimer();
        timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        timer.Tick += (s, e) => Timer_Tick();
        timer.Start();

        startTime = uTime;

        controller = new CController(startTime);
    }

    private void Timer_Tick()
    {
        controller.update(0.1); //обновляем состояние контроллера

        startTime--;
        time.Text = "Время: " + startTime / 100;
        score.Text = "Очки: " + controller.points;

        if (timer.IsRunning)
        {
            previewer.Drawable = new Drawable(controller.spawnObjects());
        }

        //Если время истекло
        if (startTime == 0)
        {
            timer.Stop();
            previewer.Drawable = null;

            startTime = 3000;
            score.Text = score.Text;

            Message();
        }
    }
    async void Message()
    {
        await DisplayAlert("Время вышло", score.Text, "Пон");
    }

    private void tap(object sender, TappedEventArgs e)
    {
        //Point mPosition = new Point(previewer.TranslationX, previewer.TranslationY);
        Point mPosition = (Point)e.GetPosition((View)sender);

        controller.mouseClick(mPosition);

    }
}