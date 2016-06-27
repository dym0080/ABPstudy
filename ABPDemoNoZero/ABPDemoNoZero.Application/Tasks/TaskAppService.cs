using Abp.Application.Services;
using Abp.Domain.Repositories;
using ABPDemoNoZero.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABPDemoNoZero.Tasks.Dtos;
using AutoMapper;

namespace ABPDemoNoZero.Tasks
{
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<Person> _personRepository;

        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public TaskAppService(ITaskRepository taskRepository, IRepository<Person> personRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
        }

        public GetTasksOutput GetTasks(GetTasksInput input)
        {
            //调用Task仓储的特定方法GetAllWithPeople
            var tasks = _taskRepository.GetAllWithPeople(input.AssignedPersonId, input.State);

            //用AutoMapper自动将List<Task>转换成List<TaskDto>
            return new GetTasksOutput
            {
                Tasks = Mapper.Map<List<TaskDto>>(tasks)
            };
        }

        public void UpdateTask(UpdateTaskInput input)
        {
            //可以直接Logger,它在ApplicationService基类中定义的
            Logger.Info("Updating a task for input: " + input);

            //通过仓储基类的通用方法Get，获取指定Id的Task实体对象
            var task = _taskRepository.Get(input.TaskId);

            //修改task实体的属性值
            if (input.State.HasValue)
            {
                task.State = input.State.Value;
            }

            if (input.AssignedPersonId.HasValue)
            {
                task.AssignedPerson = _personRepository.Load(input.AssignedPersonId.Value);
            }

            //我们都不需要调用Update方法
            //因为应用服务层的方法默认开启了工作单元模式（Unit of Work）
            //ABP框架会工作单元完成时自动保存对实体的所有更改，除非有异常抛出。有异常时会自动回滚，因为工作单元默认开启数据库事务。
        }

        public void CreateTask(CreateTaskInput input)
        {
            Logger.Info("Creating a task for input: " + input);

            //通过输入参数，创建一个新的Task实体
            var task = new Task { Description = input.Description };

            if (input.AssignedPersonId.HasValue)
            {
                task.AssignedPersonId = input.AssignedPersonId.Value;
            }

            //调用仓储基类的Insert方法把实体保存到数据库中
            _taskRepository.Insert(task);
        }
    }
}
