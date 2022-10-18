using AutoMapper;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.Integration.Interface;
using WritableRESTAPI.Integration.Model;
using WritableRESTAPI.Repository.Interface;
using WritableRESTAPI.Service.Interface;
using WritableRESTAPI.Util.Extensions;

namespace WritableRESTAPI.Service.Impl
{
    public class ServiceBase<ViewModel, Entity> : IServiceBase<ViewModel> where Entity : EntityBase
    {
        private readonly IRepositoryBase<Entity> _repository;
        private readonly IMapper _mapper;
        private readonly IQueueSender _queryIntegration;
        private readonly ILogger<ViewModel> _logger;
        private readonly AsyncRetryPolicy _retry;
        public ServiceBase(IRepositoryBase<Entity> repository, IMapper mapper, IQueueSender queue, ILogger<ViewModel> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _queryIntegration = queue;
            _logger = logger;
            _retry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, attemptRetry => TimeSpan.FromSeconds(Math.Pow(3, attemptRetry)));
        }

        public virtual async Task<ViewModel> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.DeleteAsync(id);

                await TriggerIntegrationAsync(entity, QueueMethod.Delete);

                return _mapper.Map<ViewModel>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Entity)}-{nameof(DeleteAsync)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");
                //Send to ErroQueue
                throw ex;
            }

        }

        public virtual async Task<ViewModel> InsertAsync(ViewModel viewModel)
        {
            try
            {
                var entity = _mapper.Map<Entity>(viewModel);

                Validate(entity);

                entity = await _repository.InsertAsync(entity);

                viewModel = _mapper.Map<ViewModel>(entity);

                await TriggerIntegrationAsync(entity, QueueMethod.Insert);

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Entity)}-{nameof(InsertAsync)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");
                //Send to ErroQueue
                throw ex;
            }
        }

        public virtual async Task<ViewModel> UpdateAsync(int id, ViewModel viewModel)
        {
            try
            {
                var entity = _mapper.Map<Entity>(viewModel);

                Validate(entity);

                entity = await _repository.UpdateAsync(id, entity);

                viewModel = _mapper.Map<ViewModel>(entity);

                await TriggerIntegrationAsync(entity, QueueMethod.Update);

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Entity)}-{nameof(UpdateAsync)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");
                //Send to ErroQueue
                throw ex;
            }
        }

        protected async Task TriggerIntegrationAsync(Entity entity, QueueMethod method)
        {
            await _retry.ExecuteAsync(async () => await _queryIntegration.SendAsync(entity, method));
        }

        protected virtual void Validate(Entity entity)
        { 
        }
    }
}
