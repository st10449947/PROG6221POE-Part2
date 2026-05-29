

using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        // OBJECTS 
        private KeywordResponder responder = new KeywordResponder();
        private SentimentDetector sentimentDetector = new SentimentDetector();
        private MemoryStore memoryStore = new MemoryStore();
        private ChatBot chatBot = new ChatBot();

        private string userName = "";

        // CONSTRUCTOR 
        public MainWindow()
        {
            InitializeComponent();

            PlayGreeting();
            StartChatbot();
        }

        //  START CHATBOT 
        private void StartChatbot()
        {
            AppendBotMessage("Welcome to the Cybersecurity Bot!");
            AppendBotMessage("Please enter your name:");
        }

        //PLAY GREETING AUDIO
        private void PlayGreeting()
        {
            try
            {
                string audioPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "greeting.wav"
                );

                SoundPlayer player = new SoundPlayer(audioPath);
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing audio: " + ex.Message);
            }
        }

        // SEND MESSAGE
        private void SendMessage()
        {
            string message = UserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
                return;

            AppendUserMessage(message);

            ProcessUserInput(message);

            UserInput.Clear();

            ChatScroller.ScrollToEnd();
        }

        //PROCESS USER INPUT 
        private void ProcessUserInput(string input)
        {
            string originalInput = input;
            input = input.ToLower();

            //  USER NAME
            if (string.IsNullOrEmpty(userName))
            {
                userName = originalInput;

                AppendBotMessage($"Hey {userName}! How are you today?");
                AppendBotMessage(
                    "You can ask me about passwords, phishing, scams, malware, VPNs, privacy, and safe browsing.");

                return;
            }

            // GREETING 
            if (input.Contains("how are you") ||
                input.Contains("good") ||
                input.Contains("fine"))
            {
                AppendBotMessage(
                    "I'm doing great, thanks for asking!");

                AppendBotMessage(
                    "Cybersecurity awareness is important for staying safe online.");

                return;
            }

            // PURPOSE 
            if (input.Contains("purpose"))
            {
                AppendBotMessage(
                    "My purpose is to educate users about cybersecurity and help users stay safe online.");

                return;
            }

            // HELP
            if (input.Contains("help") ||
                input.Contains("topics") ||
                input.Contains("what can i ask"))
            {
                string topics = "You can ask me about:\n";

                foreach (string keyword in responder.GetAllKeywords())
                {
                    topics += "• " + keyword + "\n";
                }

                AppendBotMessage(topics);

                return;
            }

            //  FOLLOW-UP
            if (input.Contains("tell me more") ||
                input.Contains("another tip") ||
                input.Contains("explain more") ||
                input.Contains("more details") ||
                input.Contains("confused"))
            {
                string followUp = responder.GetFollowUpResponse();

                AppendBotMessage(followUp);

                return;
            }

            // SENTIMENT
            Sentiment sentiment =
                sentimentDetector.Detect(input);

            string emotionResponse =
                sentimentDetector.GetSentimentResponse(sentiment);

            string botResponse =
                responder.GetResponse(input);

            AppendBotMessage(
                emotionResponse + "\n\n" +
                botResponse + "\n\n" +
                $"Feel free to ask another cybersecurity question, {userName}!"
            );
        }

        //  USER MESSAGE
        private void AppendUserMessage(string message)
        {
            Border bubble = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 300
            };

            TextBlock text = new TextBlock
            {
                Text = message,
                Foreground = Brushes.Black,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14
            };

            bubble.Child = text;

            ChatPanel.Children.Add(bubble);

            ChatScroller.ScrollToEnd();
        }

        // BOT MESSAGE 
        private void AppendBotMessage(string message)
        {
            Border bubble = new Border
            {
                Background = new SolidColorBrush(
                    Color.FromRgb(37, 211, 102)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 350
            };

            TextBlock text = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                FontFamily = new FontFamily("Consolas")
            };

            bubble.Child = text;

            ChatPanel.Children.Add(bubble);

            ChatScroller.ScrollToEnd();
        }

        //  ENTER KEY
        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        //SEND BUTTON 
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }
    }
}


