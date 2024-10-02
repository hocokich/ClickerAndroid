using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Shapes;

using Microsoft.Maui.Controls.Shapes;


namespace MauiApp1
{
    internal class CController //класс управляющий собираемыми объектами
    {
        public List<CObject> objects; //список собираемых объектов
        double spawnRate; //время, между созданием собираемых объектов
        double time; //время с момента создания последнего собираемого объекта
        Random rng;
        //минимальное и максимальное время жизни собираемых объектов
        double minLifetime;
        double maxLifetime;
        //минимальный и максимальный размер собираемых объектов
        float minSpriteSize;
        float maxSpriteSize;
        public int points; //набранные очки

        public int count;//число объектов

        public CController(double startTime)
        {
            rng = new Random();
            objects = new List<CObject>();
            time = startTime;
            points = 0;
            minLifetime = 1;
            maxLifetime = 10;

            minSpriteSize = 20;
            maxSpriteSize = 50;
        }

        public List<CObject> spawnObjects()
        {
            objects.Add(new CObject(new Point(rng.Next(350), rng.Next(550)), minSpriteSize, maxLifetime));

            return objects;
        }
        public void destroyObject(CObject obj)
        {
            objects.Remove(obj);
        }
        public void update(double delta)
        {
            for (var i = 0; i < objects.Count; i++)
            {
                if (objects[i].updateLifetime(delta))
                {
                    destroyObject(objects[i]);
                    count = objects.Count;
                }
                else
                {
                    objects[i].size.Height += delta;
                    objects[i].size.Width += delta;

                    count = objects.Count;
                }
            }
        }
        public void mouseClick(Point mousePosition)
        {
            for (var i = 0; i < objects.Count; i++)
            {
                if (objects[i].isMouseOnObject(mousePosition))
                {
                    destroyObject(objects[i]);

                    points += 100;
                    return;
                }
            }
            points += -100;
        }

        public List<Ellipse> drawEllipses()
        {
            List<Ellipse> el = new List<Ellipse>();

            for (var i = 0; i < objects.Count; i++)
                el.Add(objects[i].getSprite());

            return el;
        }
    }
}
