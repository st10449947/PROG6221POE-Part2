using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CybersecurityChatbot
{
   
        internal class KeywordResponder
        {
            // MAIN RESPONSES
            private Dictionary<string, List<string>> _responses;

            // FOLLOW-UP RESPONSES
            private Dictionary<string, List<string>> _followUps;

            // RANDOM OBJECT
            private Random _random = new Random();

            // CURRENT TOPIC MEMORY
            public string CurrentTopic { get; private set; }

            // CONSTRUCTOR
            public KeywordResponder()
            {
                //  MAIN RESPONSES 
                _responses = new Dictionary<string, List<string>>()
            {
                // PASSWORDS
                {
                    "password",
                    new List<string>()
                    {
                        "Strong passwords help protect your accounts from hackers.",
                        "Use different passwords for every account.",
                        "Enable multi-factor authentication for extra security.",
                        "Avoid using names or birthdays in passwords."
                    }
                },

                // PHISHING
                {
                    "phishing",
                    new List<string>()
                    {
                        "Phishing attacks try to steal sensitive information.",
                        "Always verify suspicious emails before clicking links.",
                        "Phishing scams often pretend to be trusted companies.",
                        "Never enter passwords into suspicious websites."
                    }
                },

                // PRIVACY
                {
                    "privacy",
                    new List<string>()
                    {
                        "Protecting your online privacy is very important.",
                        "Avoid sharing personal information publicly.",
                        "Review app permissions carefully.",
                        "Use privacy settings on social media."
                    }
                },

                // SCAMS
                {
                    "scam",
                    new List<string>()
                    {
                        "Scammers often pressure victims into acting quickly.",
                        "Be cautious of offers that seem too good to be true.",
                        "Never send money to unknown people online.",
                        "Verify websites before making payments."
                    }
                },

                // MALWARE
                {
                    "malware",
                    new List<string>()
                    {
                        "Malware is harmful software designed to damage systems.",
                        "Avoid downloading unknown files.",
                        "Keep your antivirus software updated.",
                        "Malware can steal personal information."
                    }
                },

                // VPN
                {
                    "vpn",
                    new List<string>()
                    {
                        "VPNs help protect your online privacy.",
                        "A VPN encrypts your internet connection.",
                        "VPNs are useful on public Wi-Fi.",
                        "VPNs help hide your IP address."
                    }
                },

                // SAFE BROWSING
                {
                    "safe browsing",
                    new List<string>()
                    {
                        "Always browse websites using HTTPS.",
                        "Avoid clicking suspicious advertisements.",
                        "Keep your browser updated regularly.",
                        "Avoid downloading software from unknown websites."
                    }
                }
            };

                //  FOLLOW-UP RESPONSES 
                _followUps = new Dictionary<string, List<string>>()
            {
                // PASSWORDS
                {
                    "password",
                    new List<string>()
                    {
                        "A strong password should include symbols, numbers, and uppercase letters.",
                        "Password managers can generate secure passwords.",
                        "Changing passwords regularly improves security.",
                        "Avoid reusing passwords across multiple websites."
                    }
                },

                // PHISHING
                {
                    "phishing",
                    new List<string>()
                    {
                        "Hover over links before clicking them.",
                        "Phishing can happen through SMS messages too.",
                        "Fake banking emails are common phishing attacks.",
                        "Urgent language is a common phishing tactic."
                    }
                },

                // PRIVACY
                {
                    "privacy",
                    new List<string>()
                    {
                        "Public Wi-Fi can expose your personal data.",
                        "Limit how much personal information you share online.",
                        "Always log out from shared computers.",
                        "Use strong privacy settings on your accounts."
                    }
                },

                // SCAMS
                {
                    "scam",
                    new List<string>()
                    {
                        "Online shopping scams are becoming more common.",
                        "Scammers sometimes impersonate technical support.",
                        "Always double-check website URLs.",
                        "Do not trust random investment opportunities."
                    }
                },

                // MALWARE
                {
                    "malware",
                    new List<string>()
                    {
                        "Ransomware is a dangerous form of malware.",
                        "Malware spreads through infected email attachments.",
                        "Software updates help reduce malware risks.",
                        "USB devices can also spread malware."
                    }
                },

                // VPN
                {
                    "vpn",
                    new List<string>()
                    {
                        "Businesses use VPNs for secure remote work.",
                        "VPNs improve privacy on public networks.",
                        "Free VPNs are not always secure.",
                        "VPNs protect your browsing activity."
                    }
                },

                // SAFE BROWSING
                {
                    "safe browsing",
                    new List<string>()
                    {
                        "Fake websites can steal login information.",
                        "Never ignore browser security warnings.",
                        "Ad blockers can improve browsing safety.",
                        "HTTPS websites encrypt your information."
                    }
                }
            };
            }

            //  MAIN RESPONSE
            public string GetResponse(string input)
            {
                input = input.ToLower();

                // SEARCH FOR KEYWORDS
                foreach (var keyword in _responses.Keys)
                {
                    if (input.Contains(keyword))
                    {
                        // SAVE CURRENT TOPIC
                        CurrentTopic = keyword;

                        List<string> responses = _responses[keyword];

                        int index = _random.Next(responses.Count);

                        return responses[index];
                    }
                }

                return "I am not sure about that. Try asking about passwords, phishing, scams, malware, privacy, VPNs, or safe browsing.";
            }

            // FOLLOW-UP RESPONSE 
            public string GetFollowUpResponse()
            {
                if (string.IsNullOrEmpty(CurrentTopic))
                {
                    return "Please ask about a cybersecurity topic first.";
                }

                List<string> details = _followUps[CurrentTopic];

                int index = _random.Next(details.Count);

                return details[index];
            }

            //  GET ALL TOPICS 
            public List<string> GetAllKeywords()
            {
                return _responses.Keys.ToList();
            }
        }
    }
