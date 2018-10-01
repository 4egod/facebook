﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace Facebook.Tests
{
    using Messaging;
    using Messaging.Buttons;
    using Messaging.Templates;

    using static Consts;

    [TestClass]
    public class MessagingTests
    {
        private MessagingClient client;

        [TestInitialize]
        public void Intitialize()
        {
            client = new MessagingClient(PageToken);
        }

        [TestMethod]
        public void ApiUriTest()
        {
            string result = client.ApiUri;
            Trace.WriteLine($"ApiUri: {result}");
        }

        [TestMethod]
        public void SendMessageTest()
        {
            string result = client.SendMessageAsync(UserId, "Test message").Result;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void SendMessageWithQuickReplyTest()
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                Title = "title 1",
                Payload = "callback#1",
                QuickReplyType = QuickReplyTypes.Text
            });

            qrl.Add(new QuickReply()
            {
                Title = "title 2",
                Payload = "callback#2",
                QuickReplyType = QuickReplyTypes.Text
            });

            string result = client.SendMessageAsync(1703132266465872, "some text", qrl).Result;
            Trace.WriteLine($"Message Id: {result}");
        }
        
        [TestMethod]
        public void RequestUserLocationTest()
        {
            string result = client.RequestUserLocationAsync(UserId, "Your location:").Result;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void RequestUserPhoneTest()
        {
            string result = client.RequestUserPhoneNumberAsync(UserId, "Your Phone Number:").Result;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void RequestUserEmailTest()
        {
            string result = client.RequestUserEmailAsync(UserId, "Your Email:").Result;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void GenericTemplateTest()
        {
            var buttons = new List<IButton>();
            buttons.Add(new PostbackButton()
            {
                Title = "Button 1",
                Payload = "webhook_value_1"
            });

            buttons.Add(new PostbackButton()
            {
                Title = "Button 2",
                Payload = "webhook_value_2"
            });

            var defaultAction = new GenericTemplate.Element.Action()
            {
                Url = "https://m.me/",            
            };

            var elements = new List<GenericTemplate.Element>();
            elements.Add(new GenericTemplate.Element()
            {
                Title = "Title 1",
                Subtitle = "Subtitle 1",
                ImageUrl = "",
                DefaultAction = defaultAction,
                Buttons = buttons
            });

            elements.Add(new GenericTemplate.Element()
            {
                Title = "Title 2",
                Subtitle = "Subtitle 2",
                ImageUrl = "",
                Buttons = buttons
            });

            var attachment = new Attachment<GenericTemplate>()
            {
                Payload = new GenericTemplate()
                {
                    Elements = elements
                }
            };

            string result = client.SendAttachment<Attachment<GenericTemplate>>(UserId, attachment).Result;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void ButtonTemplateTest()
        {
            var buttons = new List<IButton>();
            buttons.Add(new UrlButton()
            {
                Title = "Url Button",
                Url = "http://m.me"
            });

            buttons.Add(new PostbackButton()
            {
                Title = "Post Back Button",
                Payload = "webhook_value"
            });

            buttons.Add(new CallButton()
            {
                Title = "Call button",
                Payload = "+123456789"
            });

            var attachment = new Attachment<ButtonTemplate>()
            {
                Payload = new ButtonTemplate()
                {
                    Text = "Button Template Test",
                    Buttons = buttons
                }
            };

            string result = client.SendAttachment<Attachment<ButtonTemplate>>(UserId, attachment).Result;
            Trace.WriteLine($"Message Id: {result}");
        }


    }
}