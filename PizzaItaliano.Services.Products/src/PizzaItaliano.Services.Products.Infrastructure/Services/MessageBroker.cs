﻿using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;
using Microsoft.Extensions.Logging;
using PizzaItaliano.Services.Products.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaItaliano.Services.Products.Infrastructure.Services
{
    internal sealed class MessageBroker : IMessageBroker
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IMessageOutbox _messageOutbox;
        private readonly IMessagePropertiesAccessor _messagePropertiesAccessor;
        private readonly ILogger<IMessageBroker> _logger;

        public MessageBroker(IBusPublisher busPublisher, IMessageOutbox messageOutbox,
                IMessagePropertiesAccessor messagePropertiesAccessor, ILogger<IMessageBroker> logger)
        {
            _busPublisher = busPublisher;
            _messageOutbox = messageOutbox;
            _messagePropertiesAccessor = messagePropertiesAccessor;
            _logger = logger;
        }

        public Task PublishAsync(params IEvent[] events)
        {
            return PublishAsync(events?.AsEnumerable());
        }

        public async Task PublishAsync(IEnumerable<IEvent> events)
        {
            if (events is null)
            {
                return;
            }

            var messageProperties = _messagePropertiesAccessor.MessageProperties;
            var originatedMessageId = messageProperties?.MessageId;

            foreach (var @event in events)
            {
                if (@event is null)
                {
                    continue;
                }

                var type = @event.GetType();
                var messageId = Guid.NewGuid().ToString("N");
                _logger.LogTrace($"Publishing integration event: {type.Name} [id: '{messageId}'].");

                if (_messageOutbox.Enabled)
                {
                    await _messageOutbox.SendAsync(Convert.ChangeType(@event, type), originatedMessageId, messageId); // nie moze serializowac interfejs dlatego konwersja w runtime na klase implementujaca
                    continue;
                }

                await _busPublisher.PublishAsync(@event, messageId);
            }
        }
    }
}
