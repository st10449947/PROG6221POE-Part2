using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbot
{
    internal class ChatBot
    {
        private KeywordResponder _keywordResponder;
        private SentimentDetector _sentimentDetector;
        private MemoryStore _memoryStore;

        private bool _awaitingName = true;
        private string _lastTopic = "";
        private Random _random = new Random();

        private List<string> _fallbackResponses = new List<string>()
        {
            "I'm not sure I understood that, but I can help with cybersecurity topics.",
            "Try asking about passwords, phishing, scams, or privacy.",
            "I didn't catch that clearly — ask me about online safety topics.",
            "Could you rephrase that? I can help with cybersecurity advice."
        };

        //main method to process user input and generate response
        public ChatBot()
        {
            _keywordResponder = new KeywordResponder();
            _sentimentDetector = new SentimentDetector();
            _memoryStore = new MemoryStore();
        }
        public string ProcessInput(string input)
        {
            string originalInput = input;
            input = input.ToLower().Trim();

            if (_awaitingName)
            {
                _awaitingName = false;

                _memoryStore.Username = originalInput;

                return $"Welcome {originalInput}! I'm your Cybersecurity Assistant. How can I help you today?";

            }
            //questions
            if (input.Contains("tell me more") ||
                input.Contains("explain more") ||
                input.Contains("more details"))
            {
                if (!string.IsNullOrEmpty(_lastTopic))
                {
                    string followUp = _keywordResponder.GetFollowUpResponse();
                    return $"{followUp}\n\nWould you like more details about {_lastTopic}, {_memoryStore.Username}?";
                }

                return "Can you tell me what topic you'd like more information about?";
            }

            //sentiment direction
            Sentiment sentiment =_sentimentDetector.Detect(input);

            string sentimentIntro = "";

            if (sentiment != Sentiment.Neutral)
            {
                sentimentIntro =
                    _sentimentDetector.GetSentimentResponse(sentiment) + "\n";
            }


            // keyword responds
            string keywordResponse = _keywordResponder.GetResponse(input);

            // try to capture topic for memory
            foreach (var keyword in _keywordResponder.GetAllKeywords())
            {
                if (input.Contains(keyword))
                {
                    _lastTopic = keyword;

                    // store in memory system
                    _memoryStore.Store("last topic", keyword);
                    break;
                }
            }

            //special phrases
            if (input.Contains("how are you"))
            {
                return "I'm doing great! Cybersecurity awareness is always important.";
            }

            if (input.Contains("what can you do") ||
                input.Contains("help") ||
                input.Contains("purpose"))
            {
                return "I can help you learn about cybersecurity topics like passwords, phishing, scams, malware, VPNs, and safe browsing.";
            }
            //fallback response if no keywords matched
            bool noKeywordMatch = keywordResponse.Contains("not sure");

            if (noKeywordMatch)
            {
                string fallback =
                    _fallbackResponses[_random.Next(_fallbackResponses.Count)];

                return fallback;
            }

            // full response
            string memoryIntro =
            _memoryStore.GetPersonalisedOpener();

            return
                memoryIntro +
                sentimentIntro +
                keywordResponse +
                $"\n\nFeel free to ask another question, {_memoryStore.Username}!";



        }
    }
}
