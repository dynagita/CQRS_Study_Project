using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Integration.Model;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Interface;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Impl
{
    public class ReceiveHandler<T> : IReceiveHandler where T : EntityBase
    {
        IRepositoryBase<T> _repository;
        ILogger<T> _logger;
        public ReceiveHandler(IRepositoryBase<T> repository, ILogger<T> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public virtual async Task UpdateDBAsync(Message message)
        {
            var entity = DeserializeEntity(message.Envelop);
            entity.WritableRelation = entity.Id.ToLong();
            entity.Id = string.Empty;

            QueueMethod method = (QueueMethod)message.Method;

            switch (method)
            {
                case QueueMethod.Insert:
                    Normalize(entity);
                    await _repository.InsertAsync(entity);
                    break;
                case QueueMethod.Update:
                    Normalize(entity);
                    await _repository.UpdateAsync(entity);
                    break;
                case QueueMethod.Delete:
                    await _repository.DeleteAsync(entity);
                    break;
                default:
                    throw new NotImplementedException($"Method '{method.ToString()}' not immplemented.");
            }
        }

        public T DeserializeEntity(object data)
        {
            return JsonConvert.DeserializeObject<T>(data.ToString());
        }

        public async Task HandleAsync(Message message)
        {
            try
            {
                await UpdateDBAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(T)}-{nameof(HandleAsync)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");
                //Send to ErroQueue
                throw ex;
            }
            
        }

        protected virtual T Normalize(T entity)
        {
            return entity;
        }
    }
}
