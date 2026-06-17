using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

//unused asset, scrapped idea
namespace separate_animate
{
    public static class BasicA
    {
        public static void CreateAnimatedLetters(string text, Panel container)
        {
            
            for (int i = 0; i < text.Length; i++)
            {
                TextBlock letter = new TextBlock
                {
                    Text = text[i].ToString(),
                    FontSize = 36,
                    Margin = new System.Windows.Thickness(1),
                    RenderTransform = new TranslateTransform()
                };
                //gets each letter and adds it to the container
                container.Children.Add(letter);
                //starts the animation for each letter
                DoubleAnimation bounce = new DoubleAnimation
                {
                    From = 0,
                    To = -15,
                    Duration = TimeSpan.FromMilliseconds(300),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever,
                    BeginTime = TimeSpan.FromMilliseconds(i * 100)
                };
                
                ((TranslateTransform)letter.RenderTransform)
                    .BeginAnimation(
                        TranslateTransform.YProperty,
                        bounce);
            }
        }
    }
}