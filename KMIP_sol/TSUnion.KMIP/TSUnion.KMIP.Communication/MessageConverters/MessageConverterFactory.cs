using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSUnion.KMIP.Core;

namespace TSUnion.KMIP.Communication.MessageConverters
{
    public class MessageConverterFactory
    {
        public static IMessageConverter CreateMessageConverter(ConverterType type) 
        {
            if (type == ConverterType.Native) 
            {
                return new NativeMessageConverter();
            }

            if (type == ConverterType.Simple)
            {
                return new SimpleMessageConverter();
            }

            return null;
        }
    }
}
