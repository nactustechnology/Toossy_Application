using System;
using System.Collections.Generic;
using System.Text;
using ToossyApp.Models;
using Xamarin.Forms.Maps;

namespace ToossyApp.Controls
{
    public class MessageMap : Map
    {
        public List<MessagePin> MessagePins { get; set; }

        public MessageMap() : base()
        {
            MessagePins = new List<MessagePin>();

        }
    }
}
