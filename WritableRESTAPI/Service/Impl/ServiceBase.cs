using AutoMapper;
using Microsoft.Extensions.Logging;
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
        IRepositoryBase<Entity> _repository;
        IMapper _mapper;
        IQueueSender _queryIntegration;
        ILogger<ViewModel> _logger;

        public ServiceBase(IRepositoryBase<Entity> repository, IMapper mapper, IQueueSender queue, ILogger<ViewModel> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _queryIntegration = queue;
            _logger = logger;
        }

        public virtual async Task<ViewModel> Delete(int id)
        {
            try
            {
                var entity = await _repository.Delete(id);

                await TriggerIntegration(entity, QueueMethod.Delete);

                return _mapper.Map<ViewModel>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Entity)}-{nameof(Delete)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");
                //Send to ErroQueue
                throw ex;
            }

        }

        public virtual async Task<ViewModel> Insert(ViewModel viewModel)
        {
            try
            {
                var entity = _mapper.Map<Entity>(viewModel);

                Validate(entity);

                entity = await _repository.Insert(entity);

                viewModel = _mapper.Map<ViewModel>(entity);

                await TriggerIntegration(entity, QueueMethod.Insert);

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Entity)}-{nameof(Insert)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");
                //Send to ErroQueue
                throw ex;
            }
        }

        public virtual async Task<ViewModel> Update(int id, ViewModel viewModel)
        {
            try
            {
                var entity = _mapper.Map<Entity>(viewModel);

                Validate(entity);

                entity = await _repository.Update(id, entity);

                viewModel = _mapper.Map<ViewModel>(entity);

                await TriggerIntegration(entity, QueueMethod.Update);

                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Entity)}-{nameof(Update)}: An error has ocurred sending data to QueryDataBase.{Environment.NewLine}Ex: {ex.AllMessages()}{Environment.NewLine}{ex.StackTrace}");
                //Send to ErroQueue
                throw ex;
            }
        }

        protected async Task TriggerIntegration(Entity entity, QueueMethod method)
        {
            await _queryIntegration.Send(entity, method);
        }

        protected virtual void Validate(Entity entity)
        { 
        }
    }
}
