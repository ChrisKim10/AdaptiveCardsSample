using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdaptiveCards;
using AdaptiveCards.Rendering;
using AdaptiveCards.Rendering.Wpf;

namespace AdaptiveCardSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AdaptiveCardRenderer renderer;
        AdaptiveSchemaVersion schemaVersion;

        public MainWindow()
        {
            InitializeComponent();
            initAdaptiveCardRenderer();
        }

        private void btGenerate_Click(object sender, RoutedEventArgs e)
        {
            RenderAdaptiveCard();
        }

        public void initAdaptiveCardRenderer()
        {
            renderer = new AdaptiveCardRenderer();
            schemaVersion = renderer.SupportedSchemaVersion;
        }

        public void RenderAdaptiveCard()
        {
            cardGrid.Children.Clear();

            try
            {
                AdaptiveCardParseResult parseResult = AdaptiveCard.FromJson(tbInput.Text);

                AdaptiveCard card = parseResult.Card;

                RenderedAdaptiveCard renderedCard = renderer.RenderCard(card);
                FrameworkElement element = renderedCard.FrameworkElement;
                IList<AdaptiveWarning> warnings = renderedCard.Warnings;

                renderedCard.OnAction += RenderedCard_OnAction;
                cardGrid.Children.Add(renderedCard.FrameworkElement);
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }

        private void RenderedCard_OnAction(RenderedAdaptiveCard sender, AdaptiveActionEventArgs e)
        {
            if (e.Action is AdaptiveOpenUrlAction openUrlAction)
            {
                Process.Start("www.microsoft.com");
            }
        }
    }
}
