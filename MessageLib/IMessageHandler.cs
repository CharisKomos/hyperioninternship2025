﻿namespace MessageLib
{
    public interface IMessageHandler
    {
        void Handle(Message message);
    }
}
